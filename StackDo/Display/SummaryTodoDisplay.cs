using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
