using Locadora.Dados;
using Locadora.Dominio;
using Locadora.WebApi.Dtos;
using Locadora.WebApi.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.WebApi.Controllers
{
    [ApiController]
    
    [Route("[controller]")]   
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly LocadoraContext _locadoraContext;
        private readonly IConnection _rabbitConnection;

        public ClienteController(ILogger<ClienteController> logger,
            IRepositorioCliente repositorioCliente,
            LocadoraContext locadoraContext,
            IConnection rabbitConnection)
        {
            _logger = logger;
            _locadoraContext = locadoraContext;
            _repositorioCliente = repositorioCliente;
            _rabbitConnection = rabbitConnection;
        }
        [HttpPost]
        [Authorize(Roles="Gerente,Administrador")]
        public IActionResult CriarCliente(ClienteDto clienteDto)
        {
            try
            {
                var cadastrarCliente = new CadastrarClienteHandler(_locadoraContext, _repositorioCliente, _rabbitConnection);
                var nome = User.Identity.Name;
                cadastrarCliente.Criar(clienteDto);
                return CreatedAtAction(nameof(CriarCliente), Guid.NewGuid());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao criar cliente");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Versao()
        {
            return Ok("1.0.0.0");
        }
    }
}
