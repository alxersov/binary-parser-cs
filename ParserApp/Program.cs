using System;
using System.IO;
using Parser.Bitmap;

namespace ParserApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: ParserApp.exe <path to BMP file>");
                return;
            }

            var filePath = Path.GetFullPath(args[0]);
            Console.WriteLine(filePath);
            Console.WriteLine();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                var parser = new BitmapParser(stream);

                var structures = parser.Parse();

                foreach (var structure in structures)
                {
                    Console.WriteLine(structure);
                }
            }
        }
    }
}
