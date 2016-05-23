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
            string filename = "serialized.json";
            ITodoContainer rootContainer = LoadRoot(filename, serializer);
            ITodoContainer currentContainer = rootContainer;

            ITodoDisplay detailedDisplay = new DetailedTodoDisplay();
            ITodoDisplay summaryDisplay = new SummaryTodoDisplay();
            ITodoContainerDisplay detailedContainerDisplay = new DetailedTodoContainerDisplay(detailedDisplay, summaryDisplay, summaryDisplay);

            while (HandleInput(detailedContainerDisplay, ref currentContainer))
            {
            }

            SaveRoot(filename, serializer, rootContainer);
        }

        /// <summary>
        /// Load a root todo container with the given serializer.
        /// </summary>
        /// <param name="serializer"></param>
        /// <returns></returns>
        static ITodoContainer LoadRoot(string filename, DataContractJsonSerializer serializer)
        {
            Console.WriteLine(string.Format("Loading from {0}", filename));
            if (!File.Exists(filename))
            {
                return new TodoContainer();
            }

            using (Stream stream = File.OpenRead(filename))
            {
                return (TodoContainer)serializer.ReadObject(stream);
            }
        }

        /// <summary>
        /// Save the given root todo container with the given serializer.
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="rootContainer"></param>
        static void SaveRoot(string filename, DataContractJsonSerializer serializer, ITodoContainer rootContainer)
        {
            Console.WriteLine(string.Format("Saving to {0}", filename));
            using (Stream stream = File.Open(filename, FileMode.Create))
            {
                serializer.WriteObject(stream, rootContainer);
            }
        }

        /// <summary>
        /// Display the todo container, and handle user input.
        /// </summary>
        /// <param name="containerDisplay"></param>
        /// <param name="currentContainer"></param>
        /// <returns></returns>
        static bool HandleInput(ITodoContainerDisplay containerDisplay, ref ITodoContainer currentContainer)
        {
            Console.WriteLine(containerDisplay.Display(currentContainer));

            string input = Console.ReadLine().Trim();
            Console.Clear();

            if (string.IsNullOrEmpty(input))
            {
                return true;
            }

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
                else
                {
                    Console.WriteLine("No parent.");
                }
                return true;
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
                }
                else
                {
                    Console.WriteLine(string.Format("No child found matching index '{0}'", numStr));
                }
                return true;
            }

            if (input.Equals("r", StringComparison.OrdinalIgnoreCase))
            {
                if (currentContainer.Parent != null)
                {
                    ITodoContainer containerToRemove = currentContainer;
                    currentContainer = currentContainer.Parent;
                    currentContainer.RemoveChild(containerToRemove);
                }
                else
                {
                    Console.WriteLine("Can't remove root node.");
                }
                return true;
            }

            int num;
            if (int.TryParse(input, out num))
            {
                ITodoContainer newContainer = currentContainer.Children.ElementAtOrDefault(num);
                if (newContainer != null)
                {
                    currentContainer = newContainer;
                }
                else
                {
                    Console.WriteLine(string.Format("Can't find child index {0}", num));
                }
                return true;
            }

            Console.WriteLine(string.Format("Couldn't understand command '{0}'", input));

            return true;
        }

        /// <summary>
        /// Add a new todo item to the container
        /// </summary>
        /// <param name="container"></param>
        /// <param name="input"></param>
        static void AddToContainer(ITodoContainer container, string input)
        {
            string strippedInput = input.Substring(2);
            ITodoContainer newContainer = NewTodo(strippedInput);

            container.AddChild(newContainer);
        }

        /// <summary>
        /// Create a new todo item from the command args.
        /// </summary>
        /// <param name="commandArgs"></param>
        /// <returns></returns>
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
