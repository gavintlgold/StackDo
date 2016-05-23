using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StackDo.Interface;

namespace StackDo
{
    class TodoContainer : ITodoContainer
    {
        private ISet<ITodoContainer> _children;
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

        public ITodo Todo
        {
            get;
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
            _children = new HashSet<ITodoContainer>();
        }

        public bool AddChild(ITodoContainer container)
        {
            container.Parent = this;
            return _children.Add(container);
        }

        public bool RemoveChild(ITodoContainer container)
        {
            // TODO: handle orphans
            container.Parent = null;
            return _children.Remove(container);
        }
    }
}
