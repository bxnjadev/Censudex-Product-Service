using Grpc.Core;

namespace Censudex_Product_Service.Util;

public class GrpcEntityValidations
{
    
    private GrpcEntityValidations() {}

    public static void ThrowIfIsNull(Object? entity)
    {
        if (entity == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "user.not.exists"));
        }
    } 
    
}