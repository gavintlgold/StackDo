using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using StackDo.Interface;

namespace StackDo.Core
{
    [DataContract]
    [KnownType(typeof(Todo))]
    class TodoContainer : ITodoContainer
    {
        [DataMember]
        private List<ITodoContainer> _children;
        public IEnumerable<ITodoContainer> Children
        {
            get
            {
                return _children;
            }
        }

        public ITodoContainer Parent
        {
            get;
            set;
        }

        [DataMember]
        public ITodo Todo
        {
            get;
            private set;
        }

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

        public void AddChild(ITodoContainer container)
        {
            container.Parent = this;
            _children.Add(container);
        }

        public bool RemoveChild(ITodoContainer container)
        {
            // TODO: handle orphans
            container.Parent = null;
            return _children.Remove(container);
        }

        public bool RemoveChild(int index)
        {
            ITodoContainer container = _children.ElementAt(index);
            return RemoveChild(container);
        }

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
