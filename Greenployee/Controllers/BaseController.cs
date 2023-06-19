using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Greenployee.API.Controllers
{
    
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        [Authorize]
        public bool ValidatePermission(List<string> permissionUser, List<string> permissionNeeded)
        {
            return permissionNeeded.Any(x => permissionUser.Contains(x));
        }

        [NonAction]
        [Authorize]
        public ActionResult Forbidden()
        {
            var obj = new
            {
                code = "permissao_negada",
                message = "Usuario não tem permissão para acessar este recurso"
            };

            return new ObjectResult(obj) { StatusCode = 403 };
        }
    }
}
