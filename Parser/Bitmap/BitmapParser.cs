using System;
using System.Collections.Generic;
using System.IO;

namespace Parser.Bitmap
{
    public class BitmapParser: Parser
    {
        public BitmapParser(Stream stream) : base(stream)
        {
        }

        public override void ParseImpl(IList<DataStructure> structures)
        {
            var fileHeader = Reader.ReadDataStructure<FileHeader>();

            structures.Add(fileHeader);

            var headerSize = Reader.ReadUInt16();
            Stream.Seek(-2, SeekOrigin.Current);

            bool isOs2Header = headerSize == 12;
            Int32 bitsPerPixel;
            UInt32 colorsInColorTable = 0;
            UInt32 imageSize = 0;
            Int32 width;
            Int32 height;
            if (isOs2Header)
            {
                var os2Header = Reader.ReadDataStructure<Os21xBitmapHeader>();
                structures.Add(os2Header);

                bitsPerPixel = os2Header.BitsPerPixel;
                width = os2Header.Width;
                height = os2Header.Height;
            }
            else
            {
                var bitmapHeader = Reader.ReadDataStructure<BitmapHeader>();
                structures.Add(bitmapHeader);

                bitsPerPixel = bitmapHeader.BitsPerPixel;
                colorsInColorTable = bitmapHeader.ColorsInColorTable;
                imageSize = bitmapHeader.ImageSize;
                width = bitmapHeader.Width;
                height = bitmapHeader.Height;
            }

            if (bitsPerPixel <= 8 && 0 == colorsInColorTable)
            {
                colorsInColorTable = (uint) 1 << bitsPerPixel;
            }

            if (0 < colorsInColorTable)
            {
                var entrySize = isOs2Header ? 3 : 4;
                var tableSize = entrySize * colorsInColorTable;

                structures.Add(new GenericStructure("Color Table")
                {
                    Offset = Stream.Position,
                    Size = tableSize
                });

                Stream.Seek(tableSize, SeekOrigin.Current);
            }


            if (Stream.Position < fileHeader.BitmapBitsOffset)
            {
                var gap = fileHeader.BitmapBitsOffset - Stream.Position;

                structures.Add(new GenericStructure("Gap")
                {
                    Offset = Stream.Position,
                    Size = gap
                });

                Stream.Seek(gap, SeekOrigin.Current);
            }

            if (imageSize == 0)
            {
                var rowSize = ((bitsPerPixel*width + 31) >> 5) << 2;
                imageSize = (uint)(Math.Abs(height)*rowSize);
            }

            structures.Add(new GenericStructure("Image Data")
            {
                Offset = Stream.Position,
                Size = imageSize
            });

            Stream.Seek(imageSize, SeekOrigin.Current);

            if (Stream.Position < Stream.Length)
            {
                var size = Stream.Length - Stream.Position;

                structures.Add(new GenericStructure("Unknown")
                {
                    Offset = Stream.Position,
                    Size = size
                });

                Stream.Seek(size, SeekOrigin.Current);
            }
        }
    }
}
