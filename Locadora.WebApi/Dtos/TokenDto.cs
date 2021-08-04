using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.WebApi.Dtos
{
    public class TokenDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
