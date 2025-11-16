using Censudex_Product_Service.Repository;
using Grpc.Core;
using ProductProto;
using Product = Censudex_Product_Service.Model.Product;

namespace Censudex_Product_Service.Service;

public class ProductService(
    IProductRepository productRepository,
    IProductRepository repository) : ProductProto.ProductService.ProductServiceBase
{
    
    /**
     * This is a gRPC method for find a user from her uuid
     * retrieve object ProductResponse found
     */

    public async override Task<ProductResponse?> Get(ProductRequest request, ServerCallContext context)
    {
        var uuid = request.Id;
        var user = await productRepository.Find(uuid);
        
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
    
    /**
    * This method store a new product in the datastore
    * Retrieve the response 
    */

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
            ImageId = creationProduct.ImageId
        };

        await repository.Store(product);
        return new ProductResponse
        {
            Id = product.Id.ToString(),
            Name = product.Name,
            Category = product.Category,
            Description = product.Description,
            Price = product.Price,
            Date = product.Date,
            Url = product.Url
        };
    }
    
    /**
     * This method edit a product, the field for edit are name, description, price and category
     * Retrieve the product edited
     */
    
    public async override Task<ProductResponse?> Edit(EditProduct editProduct, ServerCallContext serverCallContext)
    {
        var id = editProduct.Id;
        var editedProduct = await productRepository.Edit(id, new Product
        {
            Name = editProduct.Name,
            Description = editProduct.Description,
            Price = editProduct.Price,
            Category = editProduct.Category
        });

        if (editedProduct == null)
        {
            return null;
        }
        
        return new ProductResponse
        {
            Id = editedProduct.Id.ToString(),
            Name = editedProduct.Name,
            Category = editedProduct.Category,
            Description = editedProduct.Description,
            Price = editedProduct.Price,
            Date = editedProduct.Date,
            Url = editedProduct.Url
        };
    }
    
    /**
     * Delete a product from her uuid
     * Retrieve the product id
     */

    public async override Task<ProductResponse?> Delete(ProductRequest request, ServerCallContext serverCallContext)
    {
        var id = request.Id;
        var deletedProduct = await productRepository.Delete(id);

        if (deletedProduct == null)
        {
            return null;
        }
        
        return new ProductResponse
        {
            Id = deletedProduct.Id.ToString(),
            Name = deletedProduct.Name,
            Category = deletedProduct.Category,
            Description = deletedProduct.Description,
            Price = deletedProduct.Price,
            Date = deletedProduct.Date,
            Url = deletedProduct.Url
        };
    }
    
    /**
     * List all products and retrieve
     */

    public async override Task<ProductResponseList> All(Empty empty, ServerCallContext serverCallContext)
    {
        var responseList = new ProductResponseList();
        ICollection<ProductResponse> responses = new List<ProductResponse>();
        var allElements = await productRepository.All();
        
        foreach (var element in allElements)
        {
           var products =  responseList.Products;
           responses.Add(new ProductResponse
           {
               Id = element.Id.ToString(),
               Name = element.Name,
               Category = element.Category,
               Description = element.Description,
               Price = element.Price,
               Date = element.Date,
               Url = element.Url
           });
        }

        return responseList;
    }

}