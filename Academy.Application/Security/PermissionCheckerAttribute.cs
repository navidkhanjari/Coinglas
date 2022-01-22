using Academy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Security
{
    public class PermissionCheckerAttribute :  AuthorizeAttribute, IAuthorizationFilter
    {
        private long _permissionId;

        public PermissionCheckerAttribute(long permissionName)
        {
            _permissionId = permissionName;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var permissionService = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userName = context.HttpContext.User.Identity.Name;

                if (!permissionService.CheckPermission(userName, _permissionId).Result)
                {
                    context.Result = new RedirectResult("/login");
                }
            }
        }
       
    }
}
