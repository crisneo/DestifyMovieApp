using Microsoft.AspNetCore.Mvc;

namespace Destify.MovieApp.WebApi.Authorization
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute()
            : base(typeof(ApiKeyAuthFilter))
        {
        }
    }
}
