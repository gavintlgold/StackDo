using System.Collections.Generic;

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

        void AddChild(ITodoContainer container);

        bool RemoveChild(int index);
        bool RemoveChild(ITodoContainer container);
    }
}
