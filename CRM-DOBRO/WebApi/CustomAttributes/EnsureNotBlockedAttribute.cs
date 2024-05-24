namespace WebApi.CustomAttributes
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    /// <summary>
    /// Custom attribute for checking for blocked users
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class EnsureNotBlockedAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            var dateOfBanClaim = user.FindFirst("DateOfBan");

            if (dateOfBanClaim != null && !string.IsNullOrWhiteSpace(dateOfBanClaim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }

}
