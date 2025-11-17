using Censudex_Product_Service.Model;
using MongoDB.Driver;

namespace Censudex_Product_Service.Repository;
 
public class ProductRepository(
        IMongoDatabase mongoDatabase
    ) : IProductRepository
{

    private readonly IMongoCollection<Product> _products =
        mongoDatabase.GetCollection<Product>("products");
    
    public async Task<Product> Store(Product product)
    {
        await _products.InsertOneAsync(product);
        return product;
    }

    public async Task<Product?> Find(string uuid)
    {

        var realUuid = new Guid(uuid);
        var filter = Builders<Product>.Filter
            .Eq(p => p.Id, realUuid);


        return await _products.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Product?> FindByName(string name)
    {
        var filter = Builders<Product>.Filter
            .Eq(p => p.Name, name);

        return await _products.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Product?> Edit(string uuid, Product product)
    {
        var realUuid = new Guid(uuid);
        var filter = Builders<Product>.Filter
            .Eq(p => p.Id, realUuid);

        var update = Builders<Product>
            .Update
            .Set(p => p.Name, product.Name)
            .Set(p => p.Category, product.Category)
            .Set(p => p.Price, product.Price)
            .Set(p => p.Description, product.Description)
            .Set(p => p.ImageId, product.ImageId)
            .Set(p => p.Url, product.Url);

        await _products.UpdateOneAsync(filter, update);
        return await Find(uuid);
    }

    public async Task<Product?> Delete(string uuid)
    {
        var user = await Find(uuid);
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

        await _products.UpdateOneAsync(filter, update);
        user.Status = !user.Status;
        return user;
    }

    public async Task<ICollection<Product>> All()
    {
        return await _products.Find(_ => true)
            .ToListAsync();
    }
    
}