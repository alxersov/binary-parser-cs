using Parser.Attributes;

namespace Parser
{
    [DataStructureName("Error")]
    public class ErrorStructure: DataStructure
    {
        [FieldOrder(0)]
        public string Description { get; set; }
    }
}
