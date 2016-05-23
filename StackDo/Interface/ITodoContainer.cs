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

        /// <summary>
        /// Returns how many ancestors the todo has
        /// </summary>
        int NumAncestors
        {
            get;
        }

        /// <summary>
        /// Add a container as a child.
        /// </summary>
        /// <param name="container"></param>
        void AddChild(ITodoContainer container);

        /// <summary>
        /// Remove the ith child container from this container.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        bool RemoveChild(int index);
        
        /// <summary>
        /// Remove the specified child container from this container.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        bool RemoveChild(ITodoContainer container);
    }
}
