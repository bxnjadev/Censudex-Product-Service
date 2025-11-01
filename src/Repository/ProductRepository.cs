using Censudex_Product_Service.Model;
using MongoDB.Driver;

namespace Censudex_Product_Service.Repository;
 
public class ProductRepository(
        IMongoDatabase mongoDatabase
    ) : IProductRepository
{

    private readonly IMongoCollection<Product> _products =
        mongoDatabase.GetCollection<Product>("products");
    
    public Product Store(Product product)
    {
        _products.InsertOne(product);
        return product;
    }

    public Product? Find(string uuid)
    {

        var realUuid = new Guid(uuid);
        var filter = Builders<Product>.Filter
            .Eq(p => p.Id, realUuid);

        return _products.Find(filter).
            FirstOrDefault();
    }

    public Product? Edit(string uuid, Product product)
    {
        var realUuid = new Guid(uuid);
        var filter = Builders<Product>.Filter
            .Eq(p => p.Id, realUuid);
        
        var update = Builders<Product>
            .Update
            .Set(p => p.Name, product.Name)
            .Set(p => p.Category, product.Category)
            .Set(p => p.Price, product.Price)
            .Set(p => p.Description, product.Description);

        _products.UpdateOne(filter, update);
        return product;
    }

    public Product? Delete(string uuid)
    {
        var user = Find(uuid);
        if (user == null)
        {
            return null;
        }
        
        var realUuid = new Guid(uuid);
        
        var filter = Builders<Product>.Filter
            .Eq(p => p.Id, realUuid);
        
        var update = Builders<Product>
            .Update
            .Set(p => p.Status, !user.Status);

        _products.UpdateOne(filter, update);
        user.Status = !user.Status;
        return user;
    }
    
}