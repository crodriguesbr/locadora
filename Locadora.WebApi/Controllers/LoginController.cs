using Locadora.Dominio;
using Locadora.WebApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public ActionResult Autenticar(LoginDto loginDto)
        {
            var usuario = new Usuario() { Email = "cristianonr@gmail.com", Senha = "abc,123", Nome = "Cristiano" };

            if (usuario.Email == loginDto.Email && usuario.Senha == loginDto.Senha)
            {
                var token = GerarToken(usuario);

                return Ok(new TokenDto() { Email = usuario.Email, Token = token });
            }
            else
            {
                return NotFound("Usuário e/ou senha inválida!");
            }
        }

        private static string GerarToken(Usuario usuario)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes("kiekd939kkfikfiejf93jf939jfcms833");
            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, "Administrador")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), 
                                                                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtSecurityTokenHandler.CreateToken(descriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
