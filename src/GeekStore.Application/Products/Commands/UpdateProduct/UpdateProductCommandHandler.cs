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

        product.Name = request.Name;
        product.Description = request.Description;
        product.Picture = request.Picture;
        product.Type = request.Type;
        product.Brand = request.Brand;
        product.Quantity = request.Quantity;
        product.Price = request.Price;
        product.Width = request.Width;
        product.Height = request.Height;
        product.Length = request.Length;
        product.Weight = request.Weight;

        _unitOfWork.GetRepository<Product>().Update(product);

        var result = await _unitOfWork.CommitAsync(cancellationToken);

        return result ?
            Result.Success(product) :
            Result.Failure<Product>(new Error(
                ResourceErrorMessages.DEFAULT_ERROR, 
                ResourceErrorMessages.ERROR_UPDATE_PRODUCT));
    }
}
