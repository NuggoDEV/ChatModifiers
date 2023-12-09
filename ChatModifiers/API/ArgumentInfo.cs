using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatModifiers.API
{
    public class ArgumentInfo
    {
        public string Name { get; set; }
        public Type Type { get; set; }

        public ArgumentInfo(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}
