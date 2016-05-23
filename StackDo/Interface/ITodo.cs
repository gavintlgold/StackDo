using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackDo.Interface
{
    /// <summary>
    /// Unit of work to do
    /// </summary>
    interface ITodo
    {
        /// <summary>
        /// The name for the todo.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Additional notes for the todo.
        /// </summary>
        string Notes
        {
            get;
        }

        /// <summary>
        /// Date and time the todo was created.
        /// </summary>
        DateTime DateCreated
        {
            get;
        }

        /// <summary>
        /// Last modified time of todo.
        /// </summary>
        DateTime DateModified
        {
            get;
        }
    }
}
