using System.Security.Authentication;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();