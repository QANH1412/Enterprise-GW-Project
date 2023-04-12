using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Filter
{
    public class AdminAuthorization : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller
                return;
            }
            // Check for authorization
            if (HttpContext.Current.Session["user"] == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }

            // prevent another role access to admin
            var admin = (WebApplication2.Models.User)HttpContext.Current.Session["user"];
            if (admin == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else 
            {
                if (admin.RoleId != "Admin" && admin.RoleId != "Manager")
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
           

        }
    }
}