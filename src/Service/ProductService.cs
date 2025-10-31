using Censudex_Product_Service.Repository;
using Grpc.Core;
using ProductProto;

namespace Censudex_Product_Service.Service;

public class ProductService(IProductRepository productRepository,
    IProductRepository repository) : ProductProto.ProductService.ProductServiceBase
{

    public ProductResponse? Get(ProductRequest request, ServerCallContext context)
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

    public ProductResponse? Store(CreationProduct creationProduct, ServerCallContext context)
    {
        
    }
    
    
        
}