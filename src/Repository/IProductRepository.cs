using Censudex_Product_Service.Model;

namespace Censudex_Product_Service.Repository;

public interface IProductRepository
{

    /**
     * string name,
        string description,
        int price,
        int category
     */
    
    public Product Store(Product product);

    public Product? Find(string uuid);

    public Product? Edit(string uuid,
        Product product);

    public Product? Delete(string uuid);


}