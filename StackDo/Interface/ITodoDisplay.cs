using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackDo.Interface
{
    /// <summary>
    /// Interface for displaying ITodo objects.
    /// </summary>
    interface ITodoDisplay
    {
        string Display(ITodo todo);
    }
}
