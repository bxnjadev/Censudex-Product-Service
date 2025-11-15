using System.Collections.Immutable;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Grpc.Core;
using ImageProto;
using Microsoft.Extensions.Options;

namespace Censudex_Product_Service.Service;

public class CloudinaryImageService(Cloudinary cloudinary) : ImageService.ImageServiceBase
{
    private const int MaxSize = 10 * 1024 * 1024;
    private const int MinSize = 0;
    
    private const int Height = 500;
    private const int Width = 500;
    private const string Crop = "fill";
    private const string Gravity = "face";
    private const string Folder = "censudex";

    public override async Task<UploadedImageResponse> Upload(UploadImage request,
        ServerCallContext serverCallContext)
    {
        using var stream = new MemoryStream(request.Image.ToByteArray());
        var parameters = new ImageUploadParams
        {
            File = new FileDescription("image.jpg", stream),
            Transformation = new Transformation()
                .Width(Width)
                .Height(Height)
                .Crop(Crop)
                .Gravity(Gravity),
            Folder = Folder
        };
        
        var uploadedImage = await cloudinary.UploadAsync(parameters);
        return new UploadedImageResponse
        {
            Id = uploadedImage.PublicId,
            Url = uploadedImage.Url.AbsoluteUri
        };
    }

    public async Task<Empty> Destroy(string id)
    {
            
        var parameters = new DeletionParams(id);
        await cloudinary.DestroyAsync(parameters);
        return new Empty();
    }
    
}