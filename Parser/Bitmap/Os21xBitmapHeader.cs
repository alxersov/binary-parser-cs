using System;
using Parser.Attributes;

namespace Parser.Bitmap
{
    public class Os21xBitmapHeader: DataStructure
    {
        [FieldOrder(0)]
        public UInt32 HeaderSize { get; set; }

        [FieldOrder(1)]
        public UInt16 Width { get; set; }

        [FieldOrder(2)]
        public UInt16 Height { get; set; }

        [FieldOrder(3)]
        public UInt16 Planes { get; set; }

        [FieldOrder(4)]
        public UInt16 BitsPerPixel { get; set; }
    }
}
