using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Parser
{
    public abstract class Parser
    {
        protected Parser(Stream stream)
        {
            Stream = stream;
            Reader = new BinaryStructReader(stream, Encoding.UTF8, true);
        }

        public IList<DataStructure> Parse()
        {
            var structures = new List<DataStructure>();

            try
            {
                ParseImpl(structures);
            }
            catch (Exception ex)
            {
                structures.Add(new ErrorStructure
                {
                    Offset = Stream.Position,
                    Description = ex.Message
                });
            }

            return structures;
        }

        public abstract void ParseImpl(IList<DataStructure> structures);

        protected Stream Stream { get; set; }
        protected BinaryStructReader Reader { get; set; }
    }
}
