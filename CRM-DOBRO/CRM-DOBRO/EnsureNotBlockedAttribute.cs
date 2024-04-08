namespace CRM_DOBRO
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;

    public class EnsureNotBlockedAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            var dateOfBanClaim = user.FindFirst("DateOfBan");

            if (dateOfBanClaim != null && !string.IsNullOrWhiteSpace(dateOfBanClaim.Value))
            {
                context.Result = new StatusCodeResult(403); // Возвращаем 403 Forbidden
            }
        }
    }

}
