using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Represents information about a method argument.
/// </summary>
namespace ChatModifiers.API
{
    /// <summary>
    /// Class encapsulating information about a method argument.
    /// </summary>
    public class ArgumentInfo
    {
        /// <summary>
        /// Gets or sets the name of the argument.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the argument.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentInfo"/> class.
        /// </summary>
        /// <param name="name">The name of the argument.</param>
        /// <param name="type">The type of the argument.</param>
        public ArgumentInfo(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}
