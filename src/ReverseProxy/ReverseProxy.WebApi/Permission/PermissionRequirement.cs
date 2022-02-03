using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ReverseProxy.WebApi.Permission
{
    //public class PermissionRequirement : IAuthorizationRequirement
    //{
    //    public string _permissionName { get; }
    //    public List<string> Permisss { get; }
    //    public string ClaimType { get; }

    //    public PermissionRequirement(
    //        string permissionName,
    //        List<string> permisss,
    //        string claimType
    //        )
    //    {
    //        _permissionName = permissionName;
    //        Permisss = permisss;
    //        ClaimType = claimType;

    //    }
    //}


    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string _permissionName { get; }

        public PermissionRequirement(string PermissionName)
        {
            _permissionName = PermissionName;
        }
    }
}
