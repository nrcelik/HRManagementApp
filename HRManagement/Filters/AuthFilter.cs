using HRManagementEntities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HRManagement.Filters
{
    public class AuthFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["User"])))
            {
                filterContext.Result = new RedirectResult("/Security/Login");
            }
            else
            {
                List<string> Roles = new List<string>() { "A", "U", "T" };

                bool role = false;

                foreach (var item in Roles)
                {
                    if ((filterContext.HttpContext.Session["User"] as Users).Role ==item)
                    {
                        role=true;
                    }
                   
                }

                if (!role)
                {
                    filterContext.Result = new RedirectResult("/Security/Login");
                }
            }
            }
          
        }
    }
