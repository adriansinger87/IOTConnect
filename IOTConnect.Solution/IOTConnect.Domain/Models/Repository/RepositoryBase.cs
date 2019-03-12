using System;
using System.Collections.Generic;
using System.Text;

namespace IOTConnect.Domain.Models.Repository
{
    /// <summary>
    /// Base class to represent items in a list that a client code can use and add some properties related to that list
    /// </summary>
    public abstract class RepositoryBase<T>
    {

        // -- constructor

        /// <summary>
        /// Base constructor that instanciates the items list.
        /// </summary>
        public RepositoryBase()
        {
            Items = new List<T>();
        }

        // -- methods

        /// <summary>
        /// overwritten method
        /// </summary>
        /// <returns>Returns the name property and the count of items</returns>
        public override string ToString()
        {
            return $"{Name}: {Items.Count} {(Items.Count == 1 ? "item" : "items")}";
        }

        // -- properties

        /// <summary>
        /// Gets or sets the name of the repository
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the items of the repository and allows it for derived classes to set the items list
        /// </summary>
        public List<T> Items { get; protected set; }

        /// <summary>
        /// Gets the count of the items list as a shortcut property 
        /// </summary>
        public int Count => Items.Count;
    }
}
