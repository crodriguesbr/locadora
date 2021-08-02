using Locadora.Dados;
using Locadora.Dominio;
using Locadora.WebApi.Dtos;
using Locadora.WebApi.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public ClienteController(ILogger<ClienteController> logger,
            IRepositorioCliente repositorioCliente,
            LocadoraContext locadoraContext)
        {
            _logger = logger;
            _locadoraContext = locadoraContext;
            _repositorioCliente = repositorioCliente;
        }
        [HttpPost]
        public IActionResult CriarCliente(ClienteDto clienteDto)
        {
            try
            {
                var cadastrarCliente = new CadastrarClienteHandler(_locadoraContext, _repositorioCliente);
                cadastrarCliente.Criar(clienteDto.Nome, clienteDto.Cpf, clienteDto.DataNascimento);
                return CreatedAtAction(nameof(CriarCliente), Guid.NewGuid());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Erro ao criar cliente");
            }
        }
    }
}
