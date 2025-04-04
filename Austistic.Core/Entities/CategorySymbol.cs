namespace Austistic.Core.Entities
{
    public class CategorySymbol : BaseEntity
    {
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public IEnumerable<SymbolImage> SymbolImages { get; set; }

    }
}
