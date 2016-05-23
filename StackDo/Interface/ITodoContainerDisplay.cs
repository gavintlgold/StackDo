using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackDo.Interface
{
    interface ITodoContainerDisplay
    {
        /// <summary>
        /// Display the container and its todo.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        string Display(ITodoContainer container);
    }
}
