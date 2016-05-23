using StackDo.Interface;
using System;
using System.Runtime.Serialization;

namespace StackDo.Core
{
    /// <summary>
    /// Generic todo object.
    /// </summary>
    [DataContract]
    public class Todo : ITodo
    {
        /// <summary>
        /// Date todo was created.
        /// </summary>
        [DataMember]
        public DateTime DateCreated
        {
            get;
            private set;
        }

        /// <summary>
        /// Last modified date.
        /// </summary>
        [DataMember]
        public DateTime DateModified
        {
            get;
            private set;
        }

        /// <summary>
        /// Name for the todo.
        /// </summary>
        [DataMember]
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Extra notes for the todo.
        /// </summary>
        [DataMember]
        public string Notes
        {
            get;
            private set;
        }

        /// <summary>
        /// Construct a new todo object.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="notes"></param>
        public Todo(string name, string notes = null)
        {
            Name = name;
            Notes = notes;
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}
