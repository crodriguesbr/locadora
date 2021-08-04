using Locadora.Dados;
using Locadora.Dominio;
using Locadora.WebApi.Dtos;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Locadora.WebApi.Handlers
{
    public class CadastrarClienteHandler
    {
        private readonly LocadoraContext _locadoraContext;
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly IConnection _rabbitConnection;
        public CadastrarClienteHandler(LocadoraContext locadoraContext, 
            IRepositorioCliente repositorioCliente,
            IConnection rabbitConnection)
        {
            _locadoraContext = locadoraContext;
            _repositorioCliente = repositorioCliente;
            _rabbitConnection = rabbitConnection;
        }

        public void Criar(ClienteDto clienteDto)
        {
            var cliente = new Cliente
            {
                Nome = clienteDto.Nome,
                DataNascimento = clienteDto.DataNascimento,
                Cpf = clienteDto.Cpf
            };

            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                _repositorioCliente.Salvar(cliente);
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }

            //Enfileirar a mensagem
            using (var canal = _rabbitConnection.CreateModel())
            {
                canal.QueueDeclare(queue: "qu.solicitacao.aluquel",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

               
                string mensagem = JsonSerializer.Serialize(clienteDto);
                var corpo = Encoding.UTF8.GetBytes(mensagem);
                canal.BasicPublish(exchange: "",
                                    routingKey: "qu.solicitacao.aluquel",
                                    basicProperties: null,
                                    body: corpo);
            }
        }
    }
}
