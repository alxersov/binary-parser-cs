using System;
using Parser.Attributes;

namespace Parser.Bitmap
{
    public class BitmapHeader : DataStructure, IOptionalFieldsAtTheEnd
    {
        [FieldOrder(0)]
        public UInt32 HeaderSize { get; set; }

        [FieldOrder(1)]
        public Int32 Width { get; set; }

        [FieldOrder(2)]
        public Int32 Height { get; set; }

        [FieldOrder(3)]
        public UInt16 Planes { get; set; }

        [FieldOrder(4)]
        public UInt16 BitsPerPixel { get; set; }

        [FieldOrder(5)]
        public UInt32 Compression { get; set; }

        [FieldOrder(6)]
        public UInt32 ImageSize { get; set; }

        [FieldOrder(7)]
        public Int32 XPixelsPerMeter { get; set; }

        [FieldOrder(8)]
        public Int32 YPixelsPerMeter { get; set; }

        [FieldOrder(9)]
        public UInt32 ColorsInColorTable { get; set; }

        [FieldOrder(10)]
        public UInt32 ImportantColors { get; set; }

        [FieldOrder(11)]
        public UInt32 RedChannelBitMask { get; set; }

        [FieldOrder(12)]
        public UInt32 GreenChannelBitMask { get; set; }

        [FieldOrder(13)]
        public UInt32 BlueChannelBitMask { get; set; }

        [FieldOrder(14)]
        public UInt32 AlphaChannelBitMask { get; set; }

        [FieldOrder(15)]
        public UInt32 ColorSpaceType { get; set; }

        [FieldOrder(16)]
        [SizeInBytes(36)]
        public byte[] ColorSpaceEndpoints { get; set; }

        [FieldOrder(17)]
        public UInt32 GammaRed { get; set; }

        [FieldOrder(18)]
        public UInt32 GammaGreen { get; set; }

        [FieldOrder(19)]
        public UInt32 GammaBlue { get; set; }

        [FieldOrder(20)]
        public UInt32 Intent { get; set; }

        [FieldOrder(21)]
        public UInt32 IccProfileData { get; set; }

        [FieldOrder(22)]
        public UInt32 IccProfileSize { get; set; }

        [FieldOrder(23)]
        public UInt32 Reserved { get; set; }

        public bool ContainsAtLeastBytes(int byteCount)
        {
            if (HeaderSize == 0)
            {
                return true;
            }
            return byteCount <= HeaderSize;
        }
    }
}
