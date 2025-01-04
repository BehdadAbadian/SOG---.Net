using Grpc.Core;
using Security.API.Protos;
using Security.Application.Contracts.Interface;
using Serilog;

namespace Security.API.Services
{
    public class PermissionService : Permission.PermissionBase
    {
        //private readonly IPermissionApplicationService _permissionApplication;

        //public PermissionService(IPermissionApplicationService permissionApplication)
        //{
        //    _permissionApplication = permissionApplication;
        //}

        public override Task<CheckPermissionRespond> Check(CheckPermissionRequest request, ServerCallContext context)
        {
            //var userId = Guid.NewGuid();
            //var permission = "Product-GetAll";
            //var f = _permissionApplication.CheckPermission(userId, permission);
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
