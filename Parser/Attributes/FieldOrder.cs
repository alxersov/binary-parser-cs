using System;

namespace Parser.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldOrder: Attribute
    {
        public FieldOrder(int order)
        {
            Order = order;
        }

        public int Order { get; set; }
    }
}
