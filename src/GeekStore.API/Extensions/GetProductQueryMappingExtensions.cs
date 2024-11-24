using GeekStore.Application.Products.Queries.GetProducts;
using GeekStore.Domain.Specifications.SpecParams;

namespace GeekStore.API.Extensions
{
    public static class GetProductQueryMappingExtensions
    {
        public static GetProductsQuery ToProductsQuery(this ProductSpecParams productSpecParams)
        {
            var queryParams = new GetProductsQuery
            {
                Brands = productSpecParams.Brands,
                Types = productSpecParams.Types,
                Search = productSpecParams.Search,
                Sort = productSpecParams.Sort,
                MaxPrice = productSpecParams.MaxPrice,
                MinPrice = productSpecParams.MinPrice
            };

            return queryParams;
        }
    }
}
