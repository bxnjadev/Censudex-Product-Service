using Censudex_Product_Service.Repository;
using Censudex_Product_Service.Util;
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

        GrpcEntityValidations.ThrowIfIsNull(user);

        return new ProductResponse
        {
            Id = user.Id.ToString(),
            Name = user.Name,
            Category = user.Category,
            Date = user.Date,
            Price = user.Price,
            Url = user.Url
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

        Console.WriteLine("Checking product name " + creationProduct.Name);
        if (await repository.FindByName(creationProduct.Name) != null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "name.already.exits"));
        }

        await repository.Store(product);
        return new ProductResponse
        {
            Id = product.Id.ToString(),
            Name = product.Name,
            Category = product.Category,
            Price = product.Price,
            Date = product.Date,
            Url = product.Url,
            Status = product.Status
        };
    }

    /**
     * This method edit a product, the field for edit are name, description, price and category
     * Retrieve the product edited
     */
    public async override Task<ProductResponse?> Edit(EditProduct editProduct, ServerCallContext serverCallContext)
    {
        var id = editProduct.Id;
        var searchedProduct = await productRepository.Find(id);


        GrpcEntityValidations.ThrowIfIsNull(searchedProduct);

        if (searchedProduct.Name != editProduct.Name)
        {
            if (await productRepository.FindByName(editProduct.Name) != null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "name.already.exits"));
            }
        }

     
        if (editProduct.Name != "")
        {
            searchedProduct.Name = editProduct.Name;
        }

        if (editProduct.Category != "")
        {
            searchedProduct.Category = editProduct.Category;
        }

        if (editProduct.Price != 0)
        {
            searchedProduct.Price = editProduct.Price;
        }

        if (editProduct.Description != "")
        {
            searchedProduct.Description = editProduct.Description;
        }

        if (editProduct.ImageId != "")
        {
            searchedProduct.ImageId = editProduct.ImageId;
            searchedProduct.Url = editProduct.Url;
        }
       
        
        var product = await productRepository.Edit(id, searchedProduct);
        
        GrpcEntityValidations.ThrowIfIsNull(editProduct);

        return new ProductResponse
        {
            Id = product.Id.ToString(),
            Name = product.Name,
            Category = product.Category,
            Price = product.Price,
            Date = product.Date,
            Url = product.Url,
            Status = product.Status
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

        GrpcEntityValidations.ThrowIfIsNull(deletedProduct);

        return new ProductResponse
        {
            Id = deletedProduct.Id.ToString(),
            Name = deletedProduct.Name,
            Category = deletedProduct.Category,
            Price = deletedProduct.Price,
            Date = deletedProduct.Date,
            Url = deletedProduct.Url,
            Status = deletedProduct.Status
        };
    }

    /**
     * List all products and retrieve
     */
    public async override Task<ProductResponseList> All(Empty empty, ServerCallContext serverCallContext)
    {
        var responseList = new ProductResponseList();
        var allElements = await productRepository.All();

        foreach (var element in allElements)
        {
            var products = responseList.Products;
            products.Add(new ProductResponse
            {
                Id = element.Id.ToString(),
                Name = element.Name,
                Category = element.Category,
                Status = element.Status,
                Price = element.Price,
                Date = element.Date,
                Url = element.Url
            });
        }

        return responseList;
    }
}