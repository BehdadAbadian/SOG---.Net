using Grpc.Core;
using Security.API.Protos;
using Serilog;

namespace Security.API.Services
{
    public class PermissionService : Permission.PermissionBase
    {
        public override Task<CheckPermissionRespond> Check(CheckPermissionRequest request, ServerCallContext context)
        {
            if (request.Role == "Admin")
            {
                Log.Information("role is admin and respond send!");
                var respond = new CheckPermissionRespond { Success = true, Message = "Ok" };
                return Task.FromResult(respond);
            }
            else
            {
                Log.Information("role is not admin and respond send!");
                var respond = new CheckPermissionRespond { Success = false, Message = "Not Ok" };
                return Task.FromResult(respond);
            }
        }
    }
}
