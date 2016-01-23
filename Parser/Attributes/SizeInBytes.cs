using System;

namespace Parser.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SizeInBytes : Attribute
    {
        public SizeInBytes(int size)
        {
            Size = size;
        }

        public int Size { get; set; }
    }
}
