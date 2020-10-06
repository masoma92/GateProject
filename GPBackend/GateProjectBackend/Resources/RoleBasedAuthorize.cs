using GateProjectBackend.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Resources
{
    public class RoleBasedAuthorize : AuthorizeAttribute, IAuthorizationFilter
    {
        public RoleBasedAuthorize()
        {

        }

        public string AcceptedRoles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Validate if any permissions are passed when using attribute at controller or action level
            if (string.IsNullOrEmpty(AcceptedRoles))
            {
                //Validation cannot take place without any permissions so returning unauthorized
                context.Result = new UnauthorizedResult();
                return;
            }

           
            var email = context.HttpContext.User.Identity.Name;

            //var role = _roleRepository.GetRoleByUserEmail(email).Result;

            //var permissions = AcceptedRoles.Split(',').Select(r => r.Trim()).ToList();
            //foreach (var x in permissions)
            //{
            //    if (role != null && x == role.Name)
            //    {
            //        return;
            //    }
            //}

            context.Result = new UnauthorizedResult();
            return;

        }
    }
}
