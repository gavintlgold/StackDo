using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StackDo.Interface;

namespace StackDo
{
    public class Todo : ITodo
    {
        public DateTime DateCreated
        {
            get;
        }

        public DateTime DateModified
        {
            get;
            private set;
        }

        public string Name
        {
            get;
        }

        public string Notes
        {
            get;
        }

        public Todo(string name, string notes = null)
        {
            Name = name;
            Notes = notes;
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}
