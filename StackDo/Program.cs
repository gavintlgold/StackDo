using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StackDo.Interface;
using StackDo.Display;

namespace StackDo
{
    class Program
    {
        static void Main(string[] args)
        {
            ITodoContainer rootContainer = LoadRoot();
            ITodoContainer currentContainer = rootContainer;

            ITodoDisplay detailedDisplay = new DetailedTodoDisplay();
            ITodoDisplay summaryDisplay = new SummaryTodoDisplay();
            ITodoContainerDisplay detailedContainerDisplay = new DetailedTodoContainerDisplay(detailedDisplay, summaryDisplay, summaryDisplay);

            while (HandleInput(detailedContainerDisplay, ref currentContainer))
            {
            }
        }

        static ITodoContainer LoadRoot()
        {
            ITodoContainer rootContainer = new TodoContainer();

            return rootContainer;
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
