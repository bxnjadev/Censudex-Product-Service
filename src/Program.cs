using System.Security.Authentication;
using Censudex_Product_Service.Repository;
using Censudex_Product_Service.Service;
using CloudinaryDotNet;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IProductRepository, ProductRepository>();

var connectionString = builder.Configuration.GetValue<string>
    ("MongoDbSettings:ConnectionString");

var databaseName = builder.Configuration.GetValue<string>
    ("MongoDbSettings:DatabaseName");

var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
settings.SslSettings = new SslSettings
{
    EnabledSslProtocols = SslProtocols.Tls12
};
var mongoDbClient = new MongoClient(settings);
var mongoDatabase = mongoDbClient.GetDatabase(databaseName);

builder.Services.AddSingleton<IMongoClient>(mongoDbClient);
builder.Services.AddSingleton<IMongoDatabase>(mongoDatabase);

var cloudinaryUrl = builder.Configuration.GetValue<string>("Cloudinary:Url");
var cloudinary = new Cloudinary(cloudinaryUrl);

builder.Services.AddSingleton<Cloudinary>(cloudinary);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGrpcService<ProductService>();
app.MapGrpcService<CloudinaryImageService>();

app.Run();