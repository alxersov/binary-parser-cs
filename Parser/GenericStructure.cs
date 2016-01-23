namespace Parser
{
    class GenericStructure : DataStructure
    {
        public GenericStructure(string name)
        {
            StructureName = name;
        }

        public override string GetStructureName()
        {
            return StructureName;
        }

        protected string StructureName;
    }
}
