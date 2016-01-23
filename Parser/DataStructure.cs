using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Parser.Attributes;

namespace Parser
{
    public abstract class DataStructure
    {
        public Int64 Offset { get; set; }
        public Int64 Size { get; set; }

        public int GetSizeByType(PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType;

            if (type == typeof(UInt16))
            {
                return 2;
            }

            if (type == typeof(UInt32))
            {
                return 4;
            }

            if (type == typeof(UInt64))
            {
                return 8;
            }

            if (type == typeof(Int16))
            {
                return 2;
            }

            if (type == typeof(Int32))
            {
                return 4;
            }

            if (type == typeof(Int64))
            {
                return 8;
            }

            if (type == typeof(byte[]))
            {
                var size = propertyInfo.GetCustomAttribute<SizeInBytes>();
                if (size == null)
                {
                    throw new Exception("byte[] property not marked with SizeInBytes attribute");
                }
                return size.Size;
            }

            throw new ArgumentException("Unexpected type");
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var name = GetStructureName();
            sb.AppendFormat("{0:x8} {1} {2}\n", Offset, name, Size);

            int offset = 0;

            var orderedProperties = GetOrderedProperties();
            foreach (var property in orderedProperties)
            {
                var optionalFieldsAtTheEnd = this as IOptionalFieldsAtTheEnd;
                if (optionalFieldsAtTheEnd != null)
                {
                    var propertySize = GetSizeByType(property);
                    if (!optionalFieldsAtTheEnd.ContainsAtLeastBytes(offset + propertySize))
                    {
                        break;
                    }
                    offset += propertySize;
                }

                var value = property.GetValue(this);

                string formattedValue;
                if (value == null)
                {
                    formattedValue = "";
                }
                else if (property.PropertyType == typeof (byte[]))
                {
                    formattedValue = Utils.BytesToHexString((byte[]) value).PadLeft(10);
                }
                else if (property.PropertyType == typeof (string))
                {
                    formattedValue = value.ToString().PadLeft(10);
                }
                else
                {
                    var hex = string.Format("{0:X}", value);
                    formattedValue = string.Format("{0,10} {1,10}", value, hex);
                }

                sb.AppendFormat("    {0,-20} {1}\n", property.Name, formattedValue);
            }

            return sb.ToString();
        }

        public virtual string GetStructureName()
        {
            var type = this.GetType();

            var name = type.Name;
            var nameAttribute = (DataStructureName) Attribute.GetCustomAttribute(type, typeof (DataStructureName));
            if (nameAttribute != null)
            {
                name = nameAttribute.Name;
            }
            return name;
        }

        public IList<PropertyInfo> GetOrderedProperties()
        {
            var orderedProperties = new SortedDictionary<int, PropertyInfo>();

            var properties = this.GetType().GetProperties();
            foreach (var property in properties)
            {
                var order = property.GetCustomAttribute<FieldOrder>(true);
                if (order != null)
                {
                    orderedProperties.Add(order.Order, property);
                }
            }
            return orderedProperties.Select(op => op.Value).ToList();
        }
    }
}
