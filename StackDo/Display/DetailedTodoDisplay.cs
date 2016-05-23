using StackDo.Interface;
using System.Text;

namespace StackDo.Display
{
    class DetailedTodoDisplay : ITodoDisplay
    {
        public string Display(ITodo todo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}\n\n", todo.Name);
            if (todo.Notes != null)
            {
                sb.AppendLine("Notes:");
                sb.AppendFormat("{0}\n", todo.Notes);
            }
            sb.AppendFormat(" Created: {0}\n", todo.DateCreated);
            sb.AppendFormat("Modified: {0}\n", todo.DateModified);

            return sb.ToString();
        }
    }
}
