using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Products.Commands.UpdateProduct;
public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<Product>>
{
    private readonly IUnityOfWork _unitOfWork;

    public UpdateProductCommandHandler(IUnityOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.GetRepository<Product>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure<Product>(new Error(
                ResourceErrorMessages.DEFAULT_NOT_FOUND, 
                ResourceErrorMessages.PRODUCT_NOT_FOUND));
        }

        UpdateProductFields(product, request);

        _unitOfWork.GetRepository<Product>().Update(product);

        var result = await _unitOfWork.CommitAsync(cancellationToken);

        return result ?
            Result.Success(product) :
            Result.Failure<Product>(new Error(
                ResourceErrorMessages.DEFAULT_ERROR, 
                ResourceErrorMessages.ERROR_UPDATE_PRODUCT));
    }

    private static void UpdateProductFields(Product product, UpdateProductCommand request)
    {
        product.Name = request.Name ?? product.Name;
        product.Description = request.Description ?? product.Description;
        product.Picture = request.Picture ?? product.Picture;
        product.Type = request.Type ?? product.Type;
        product.Brand = request.Brand ?? product.Brand;
        product.Quantity = (request.Quantity.HasValue && request.Quantity.Value >= 0) ? 
            request.Quantity.Value : product.Quantity;
        product.Price = (request.Price.HasValue  && request.Price.Value >= 0m) ? 
            request.Price.Value : product.Price;
    }
}
