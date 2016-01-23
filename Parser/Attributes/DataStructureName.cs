using System;

namespace Parser.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DataStructureName: Attribute
    {
        public DataStructureName(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
