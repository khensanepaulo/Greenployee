using Greenployee.MODELS.Authentication;
using Greenployee.MODELS.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Greenployee.API.Authentication
{
    public class TokenGenerator : ITokenGenerator
    {
        public dynamic Generator(Usuario usuario)
        {
            var permission = string.Join(",", usuario.PermissoesUsuario.Select(x => x.Permissao.nmPermissao));
            var claims = new List<Claim>
            {
                new Claim("id", usuario.id.ToString()),
                new Claim("dsLogin", usuario.dsLogin),
                new Claim("dsSenha", usuario.dsSenha),
                new Claim("permissoes", permission)
            };

            var expires = DateTime.Now.AddHours(1);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
            var tokenData = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                expires: expires,
                claims: claims
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);

            return new { access_token = token, expirations = expires };
        }
    }
}
