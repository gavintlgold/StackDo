using StackDo.Interface;

namespace StackDo.Display
{
    class SummaryTodoDisplay : ITodoDisplay
    {
        public string Display(ITodo todo)
        {
            return todo.Name;
        }
    }
}
