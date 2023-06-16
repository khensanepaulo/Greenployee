using Greenployee.MODELS.Authentication;

namespace Greenployee.API.Authentication
{
    public class CurrentUser : ICurrentUser
    {
        public CurrentUser(IHttpContextAccessor httpContextAccessor) 
        { 
            var httpContext = httpContextAccessor.HttpContext;
            var claims = httpContext.User.Claims;

            if (claims.Any(x => x.Type == "id")) 
            { 
                var Id = Convert.ToInt32(claims.First(x => x.Type == "id").Value);
                id = Id;
            }

            if (claims.Any(x => x.Type == "dsLogin"))
            {
                dsLogin = claims.First(x => x.Type == "dsLogin").Value;
            }

            if (claims.Any(x => x.Type == "permissions"))
            {
                permissions = claims.First(x => x.Type == "permissions").Value;
            }
        }

        public int id { get; set; }
        public string dsLogin { get; set; }
        public string permissions { get; set; }
    }
}
