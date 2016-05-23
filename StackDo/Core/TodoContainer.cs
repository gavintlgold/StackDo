using StackDo.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace StackDo.Core
{
    /// <summary>
    /// A container for a todo, handling the parent/child relationship with other todos.
    /// </summary>
    [DataContract]
    [KnownType(typeof(Todo))]
    class TodoContainer : ITodoContainer
    {
        [DataMember]
        private List<ITodoContainer> _children;
        /// <summary>
        /// The children in the todo
        /// </summary>
        public IEnumerable<ITodoContainer> Children
        {
            get
            {
                return _children;
            }
        }

        /// <summary>
        /// The todo's parent, if any.
        /// </summary>
        public ITodoContainer Parent
        {
            get;
            set;
        }

        /// <summary>
        /// The todo itself.
        /// </summary>
        [DataMember]
        public ITodo Todo
        {
            get;
            private set;
        }

        /// <summary>
        /// Number of ancestors the todo has.
        /// </summary>
        public int NumAncestors
        {
            get
            {
                int num = 0;
                ITodoContainer cont = this;
                while (cont.Parent != null)
                {
                    num++;
                    cont = cont.Parent;
                }

                return num;
            }
        }

        /// <summary>
        /// Construct a new todocontainer with a todo in it.
        /// </summary>
        /// <param name="todo"></param>
        public TodoContainer(ITodo todo)
            : this()
        {
            Todo = todo;
        }

        /// <summary>
        /// Construct a new empty TodoContainer.
        /// </summary>
        public TodoContainer()
        {
            _children = new List<ITodoContainer>();
        }

        /// <summary>
        /// Add a new child todo container to this container.
        /// </summary>
        /// <param name="container"></param>
        public void AddChild(ITodoContainer container)
        {
            container.Parent = this;
            _children.Add(container);
        }

        /// <summary>
        /// Remove a child container.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public bool RemoveChild(ITodoContainer container)
        {
            container.Parent = null;
            return _children.Remove(container);
        }

        /// <summary>
        /// Remove a child container by index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool RemoveChild(int index)
        {
            ITodoContainer container = _children.ElementAt(index);
            return RemoveChild(container);
        }

        /// <summary>
        /// Handle custom deserialization (to hook up parents properly).
        /// </summary>
        /// <param name="ctx"></param>
        [OnDeserialized]
        void OnDeserialized(StreamingContext ctx)
        {
            foreach (ITodoContainer child in _children)
            {
                child.Parent = this;
            }
        }
    }
}
