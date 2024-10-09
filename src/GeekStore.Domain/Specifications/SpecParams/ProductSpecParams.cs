namespace GeekStore.Domain.Specifications.SpecParams
{
    public class ProductSpecParams : PaginationParams
    {
        private List<string> _brands = new();
        private List<string> _types = new();
        private string? _search;
        private decimal? _maxPrice;
        private decimal? _minPrice;

        public List<string> Brands 
        {
            get => _brands;
            set => _brands = value.SelectMany(b => 
                b.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
        }

        public List<string> Types
        {
            get => _types;
            set => _types = value.SelectMany(t => 
                t.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
        }

        public string Search 
        {
            get => _search ?? string.Empty;
            set => _search = value.ToLower();
        }

        public string? Sort { get; set; }

        public decimal? MaxPrice 
        {
            get => _maxPrice ?? null;
            set => _maxPrice = value;
        }

        public decimal? MinPrice
        {
            get => _minPrice ?? null;
            set => _minPrice = value;
        }
    }
}