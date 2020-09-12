using System;
using System.Collections.Generic;
using System.Text;

namespace K1vs.DotChat.FrameworkUtils.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class NameDescriptionAttribute: Attribute
    {
        public NameDescriptionAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }
    }
}
