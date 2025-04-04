namespace Austistic.Core.Entities
{
    public class SymbolImage : BaseEntity
    {
        public CategorySymbol CategorySymbol { get; set; }
        public string CategorySymbolId { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string SymbolIdentifier { get; set; }
        public string ContentType { get; set; }
        public byte[] ImageData { get; set; }
    }
}
