using GeekStore.Application.Products.Commands.CreateProduct;
using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Products.Commands
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Product>>
    {
        private readonly IUnityOfWork _unityOfWork;
        public CreateProductCommandHandler(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        public async Task<Result<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product 
            {
                Name = request.Name,
                Description = request.Description,
                Picture = request.Picture,
                Type = request.Type,
                Brand = request.Brand,
                Quantity = request.Quantity,
                Price = request.Price,
                Width = request.Width,
                Height = request.Height,
                Length = request.Length,
                Weight = request.Weight
            };

            _unityOfWork.GetRepository<Product>().Add(product);

            var result = await _unityOfWork.CommitAsync(cancellationToken);

            if (!result)
            {
                return Result.Failure<Product>(
                    new Error("Error", "Something went wrong while creating the product!"));
            }

            return Result.Success(product);
        }
    }
}