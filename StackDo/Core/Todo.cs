using StackDo.Interface;
using System;
using System.Runtime.Serialization;

namespace StackDo.Core
{
    [DataContract]
    public class Todo : ITodo
    {
        [DataMember]
        public DateTime DateCreated
        {
            get;
            private set;
        }

        [DataMember]
        public DateTime DateModified
        {
            get;
            private set;
        }

        [DataMember]
        public string Name
        {
            get;
            private set;
        }

        [DataMember]
        public string Notes
        {
            get;
            private set;
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
