using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackDo.Interface
{
    /// <summary>
    /// Container for a todo. Handles how the todo relates to other todos.
    /// </summary>
    interface ITodoContainer
    {
        /// <summary>
        /// This todo.
        /// </summary>
        ITodo Todo
        {
            get;
        }

        /// <summary>
        /// This todo container's parent container.
        /// </summary>
        ITodoContainer Parent
        {
            get;
            set;
        }

        /// <summary>
        /// The list of children this todo contains.
        /// </summary>
        IEnumerable<ITodoContainer> Children
        {
            get;
        }

        int NumAncestors
        {
            get;
        }

        bool AddChild(ITodoContainer container);

        bool RemoveChild(ITodoContainer container);
    }
}
