using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Censudex_Product_Service.Model;

public class Product
{
    
    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }
    
    /**
     * The name product
     */
    
    public string Name { get; set; }
    
    /**
     * The category name
     */
    
    public string Category { get; set; }
    
    /**
     * The description of the product
     */
    
    public string Description { get; set; }
    
    /**
     * The price of the product 
     */
    
    public int Price { get; set; }
    
    /**
     * The url of the product
     */
    public string Url { get; set; }
    
    /**
     * The image id of the product
     */
    
    public string ImageId { get; set; }
    
    /**
     * The status of the product
     */
    
    public bool Status { get; set; }
    
    /**
     * The date creation product
     */
    
    public string Date { get; set; }
    
}

