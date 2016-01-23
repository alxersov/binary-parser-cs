using System;
using Parser.Attributes;


namespace Parser.Bitmap
{
    [DataStructureName("BITMAPFILEHEADER")]
    public class FileHeader: DataStructure
    {
        [FieldOrder(0)]
        [SizeInBytes(2)]
        public byte[] Signature { get; set; }

        [FieldOrder(1)]
        public UInt32 FileSize { get; set; }

        [FieldOrder(2)]
        public UInt16 Reserved1 { get; set; }

        [FieldOrder(3)]
        public UInt16 Reserved2 { get; set; }

        [FieldOrder(4)]
        public UInt32 BitmapBitsOffset { get; set; }
    }
}
