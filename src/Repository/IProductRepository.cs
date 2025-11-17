using Censudex_Product_Service.Model;

namespace Censudex_Product_Service.Repository;

public interface IProductRepository
{

    /**
     * Store a new product in the datastore
     * retrieve the product stored
     */
    
    public Task<Product> Store(Product product);

    /**
     * Find a product from her uuid
     * retrieve the product searched
     */
    
    public Task<Product?> Find(string uuid);

    /**
     * Find a product from her name
     * retrieve name product searched
     */
    public Task<Product?> FindByName(string name);
    
    /**
     * Edit a product from her uuid
     * retrieve the product searched 
     */
    
    public Task<Product?> Edit(string uuid,
        Product product);

    /**
     * Delete a product her uuid
     * retrieve the product deleted
     */
    
    public Task<Product?> Delete(string uuid);

    /**
     * List all products and retrieve from the datastore
     */
    
    public Task<ICollection<Product>> All();


}