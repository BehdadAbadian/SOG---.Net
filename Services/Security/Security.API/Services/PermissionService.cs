using Grpc.Core;
using Security.API.Protos;

namespace Security.API.Services
{
    public class PermissionService : Permission.PermissionBase
    {
        public override Task<CheckPermissionRespond> Check(CheckPermissionRequest request, ServerCallContext context)
        {
            if (request.Role == "Admin")
            {
                var respond = new CheckPermissionRespond { Success = true, Message = "Ok" };
                return Task.FromResult(respond);
            }
            else
            {
                var respond = new CheckPermissionRespond { Success = false, Message = "Not Ok" };
                return Task.FromResult(respond);
            }
        }
    }
}
