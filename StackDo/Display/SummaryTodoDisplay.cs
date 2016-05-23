using StackDo.Interface;

namespace StackDo.Display
{
    /// <summary>
    /// A short todo display.
    /// </summary>
    class SummaryTodoDisplay : ITodoDisplay
    {
        /// <summary>
        /// Display the todo.
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public string Display(ITodo todo)
        {
            return todo.Name;
        }
    }
}
