using System.Web.Http;
using Microsoft.AspNetCore.Authorization;

namespace MoviesDB
{
    public class BasicAuthorizationAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public BasicAuthorizationAttribute()
        {
            Policy = "BasicAuthentication";
        }
    }
}