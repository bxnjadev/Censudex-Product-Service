using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Censudex_Product_Service.Model;

public class Product
{
    
    [BsonId]
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Category { get; set; }
    
    public string Description { get; set; }
    
    public int Price { get; set; }
    
    public string Url { get; set; }
    
    public string ImageId { get; set; }
    
    public bool Status { get; set; }
    
    public string Date { get; set; }
    
}

