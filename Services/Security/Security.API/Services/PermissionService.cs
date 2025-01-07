using Grpc.Core;
using Security.API.Protos;
using Security.Application.Contracts.Interface;
using Serilog;

namespace Security.API.Services
{
    public class PermissionService : Permission.PermissionBase
    {
        private readonly IPermissionApplicationService _permissionApplication;

        public PermissionService(IPermissionApplicationService permissionApplication)
        {
            _permissionApplication = permissionApplication;
        }

        public override Task<CheckPermissionRespond> Check(CheckPermissionRequest request, ServerCallContext context)
        {
            var userId = Guid.Parse(request.Userid);
            
            var pCheck = _permissionApplication.CheckPermission(userId, request.Permissionname);
            if (pCheck)
            {
                Log.Information("have access");
                var respond = new CheckPermissionRespond { Success = true, Message = "Ok" };
                return Task.FromResult(respond);
            }
            else
            {
                Log.Information("not access!");
                var respond = new CheckPermissionRespond { Success = false, Message = "Not Ok" };
                return Task.FromResult(respond);
            }
        }
    }
}
