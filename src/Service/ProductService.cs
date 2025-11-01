using Censudex_Product_Service.Repository;
using Grpc.Core;
using ProductProto;
using Product = Censudex_Product_Service.Model.Product;

namespace Censudex_Product_Service.Service;

public class ProductService(
    IProductRepository productRepository,
    IProductRepository repository) : ProductProto.ProductService.ProductServiceBase
{

    public async override Task<ProductResponse> Get(ProductRequest request, ServerCallContext context)
    {
        
        var uuid = request.Id;
        var user = productRepository.Find(uuid);

        if (user == null)
        {
            return null;
        }

        return new ProductResponse
        {
            Id = user.Id.ToString(),
            Name = user.Name,
            Category = user.Category,
            Date = user.Date,
            Price = user.Price
        };
    }

    public async override Task<ProductResponse> Store(CreationProduct creationProduct, ServerCallContext context)
    {

        var product = new Product
        {
            Name = creationProduct.Name,
            Category = creationProduct.Category,
            Description = creationProduct.Description,
            Date = new DateTime().ToString(),
            Price = creationProduct.Price,
            Status = true,
            Url = creationProduct.Url,
        };

        repository.Store(product);
        return new ProductResponse
        {
            Id = product.Id.ToString(),
            Category = product.Category,
            Description = product.Description,
            Price = product.Price,
            Date = product.Date,
            Url = product.Url,
        };
    }

}