using System;
using System.IO;
using System.Text;

namespace Parser
{
    public class BinaryStructReader: BinaryReader
    {
        public BinaryStructReader(Stream input) : base(input)
        {
        }

        public BinaryStructReader(Stream input, Encoding encoding) : base(input, encoding)
        {
        }

        public BinaryStructReader(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen)
        {
        }

        public object ReadByType(Type type)
        {
            if (type == typeof(UInt16))
            {
                return ReadUInt16();
            }

            if (type == typeof(UInt32))
            {
                return ReadUInt32();
            }

            if (type == typeof(UInt64))
            {
                return ReadUInt64();
            }

            if (type == typeof(Int16))
            {
                return ReadInt16();
            }

            if (type == typeof(Int32))
            {
                return ReadInt32();
            }

            if (type == typeof(Int64))
            {
                return ReadInt64();
            }

            throw new ArgumentException("Unexpected type");
        }

        public T ReadDataStructure<T>() where T:DataStructure
        {
            return (T)ReadDataStructure(typeof(T));
        }

        public DataStructure ReadDataStructure(Type t)
        {
            var s = (DataStructure)Activator.CreateInstance(t);

            s.Offset = BaseStream.Position;

            var properties = s.GetOrderedProperties();
            foreach (var property in properties)
            {
                var propertySize = s.GetSizeByType(property);

                if (CanReadNextField(s, propertySize))
                {
                    var propertyType = property.PropertyType;
                    var isByteArray = propertyType == typeof (byte[]);
                    var value = isByteArray ? ReadBytes(propertySize) : ReadByType(propertyType);
                    property.SetValue(s, value);
                }
                else
                {
                    break;
                }
            }

            s.Size = BaseStream.Position - s.Offset;

            return s;
        }

        private bool CanReadNextField(DataStructure s, int size)
        {
            var optionalFieldsAtTheEnd = s as IOptionalFieldsAtTheEnd;
            if (optionalFieldsAtTheEnd != null)
            {
                var currentlyRead = BaseStream.Position - s.Offset;
                return optionalFieldsAtTheEnd.ContainsAtLeastBytes((int)(currentlyRead + size));
            }

            return true;
        }
    }
}
