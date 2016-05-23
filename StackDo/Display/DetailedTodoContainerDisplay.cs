using StackDo.Interface;
using System.Linq;
using System.Text;

namespace StackDo.Display
{
    /// <summary>
    /// A detailed display for the todo container.
    /// </summary>
    class DetailedTodoContainerDisplay : ITodoContainerDisplay
    {
        private ITodoDisplay _todoDisplay;
        private ITodoDisplay _parentDisplay;
        private ITodoDisplay _childDisplay;

        /// <summary>
        /// Construct a new detailed container display, with provided sub-display objects.
        /// </summary>
        /// <param name="todoDisplay"></param>
        /// <param name="parentDisplay"></param>
        /// <param name="childDisplay"></param>
        public DetailedTodoContainerDisplay(ITodoDisplay todoDisplay, ITodoDisplay parentDisplay, ITodoDisplay childDisplay)
        {
            _todoDisplay = todoDisplay;
            _parentDisplay = parentDisplay;
            _childDisplay = childDisplay;
        }

        /// <summary>
        /// Display the todo container.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public string Display(ITodoContainer container)
        {
            StringBuilder sb = new StringBuilder();

            if (_parentDisplay != null)
            {
                if (container.Parent != null)
                {
                    ITodoContainer parent = container.Parent;
                    while (parent != null)
                    {
                        if (parent.Todo != null)
                        {
                            sb.Insert(0, string.Format("{0} > ", _parentDisplay.Display(parent.Todo)));
                        }
                        else
                        {
                            sb.Insert(0, "root > ");
                        }

                        parent = parent.Parent;
                    }
                    sb.AppendLine("\n");
                }
                else
                {
                    sb.AppendLine("<root>");
                }
            }

            if (_todoDisplay != null && container.Todo != null)
            {
                sb.AppendLine(_todoDisplay.Display(container.Todo));
            }

            if (container.Children.Any())
            {
                sb.AppendFormat("{0} Children:\n", container.Children.Count());

                if (_childDisplay != null)
                {
                    int i = 0;
                    foreach (ITodo child in container.Children.Select(c => c.Todo))
                    {
                        sb.AppendFormat("  {0} {1}\n", i, _childDisplay.Display(child));
                        i++;
                    }
                }
            }
            else
            {
                sb.AppendLine("No TODOs.");
            }

            return sb.ToString();
        }
    }
}
