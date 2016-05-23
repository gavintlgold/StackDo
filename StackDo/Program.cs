using StackDo.Core;
using StackDo.Display;
using StackDo.Interface;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace StackDo
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(TodoContainer));
            ITodoContainer rootContainer = LoadRoot(serializer);
            ITodoContainer currentContainer = rootContainer;

            ITodoDisplay detailedDisplay = new DetailedTodoDisplay();
            ITodoDisplay summaryDisplay = new SummaryTodoDisplay();
            ITodoContainerDisplay detailedContainerDisplay = new DetailedTodoContainerDisplay(detailedDisplay, summaryDisplay, summaryDisplay);

            while (HandleInput(detailedContainerDisplay, ref currentContainer))
            {
            }

            SaveRoot(serializer, rootContainer);
        }

        static ITodoContainer LoadRoot(DataContractJsonSerializer serializer)
        {
            if (!File.Exists("serialized.json"))
            {
                return new TodoContainer();
            }

            using (Stream stream = File.OpenRead("serialized.json"))
            {
                return (TodoContainer)serializer.ReadObject(stream);
            }
        }

        static void SaveRoot(DataContractJsonSerializer serializer, ITodoContainer rootContainer)
        {
            using (Stream stream = File.Open("serialized.json", FileMode.Create))
            {
                serializer.WriteObject(stream, rootContainer);
            }
        }

        static bool HandleInput(ITodoContainerDisplay containerDisplay, ref ITodoContainer currentContainer)
        {
            Console.Clear();
            Console.WriteLine(containerDisplay.Display(currentContainer));

            string input = Console.ReadLine().Trim();
            if (input.Equals("q", StringComparison.OrdinalIgnoreCase) ||
                input.Equals("quit", StringComparison.OrdinalIgnoreCase) ||
                input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (input.Equals("p", StringComparison.OrdinalIgnoreCase))
            {
                if (currentContainer.Parent != null)
                {
                    currentContainer = currentContainer.Parent;
                }
            }

            if (input.StartsWith("a ", StringComparison.OrdinalIgnoreCase))
            {
                AddToContainer(currentContainer, input);
                return true;
            }

            if (input.StartsWith("r ", StringComparison.OrdinalIgnoreCase))
            {
                string numStr = input.Substring(2);
                int index;
                if (int.TryParse(numStr, out index))
                {
                    currentContainer.RemoveChild(index);
                    return true;
                }
            }

            if (input.Equals("r", StringComparison.OrdinalIgnoreCase))
            {
                if (currentContainer.Parent != null)
                {
                    ITodoContainer containerToRemove = currentContainer;
                    currentContainer = currentContainer.Parent;
                    currentContainer.RemoveChild(containerToRemove);
                    return true;
                }
            }

            int num;
            if (int.TryParse(input, out num))
            {
                ITodoContainer newContainer = currentContainer.Children.ElementAtOrDefault(num);
                if (newContainer != null)
                {
                    currentContainer = newContainer;
                }
                return true;
            }

            return true;
        }

        static void AddToContainer(ITodoContainer container, string input)
        {
            string strippedInput = input.Substring(2);
            ITodoContainer newContainer = NewTodo(strippedInput);

            container.AddChild(newContainer);
        }

        static ITodoContainer NewTodo(string commandArgs)
        {
            // TODO: add in notes parsing
            string name = commandArgs;
            ITodo todo = new Todo(name);
            ITodoContainer container = new TodoContainer(todo);

            return container;
        }
    }
}
