﻿using Locadora.Dados;
using Locadora.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.WebApi.Handlers
{
    public class CadastrarClienteHandler
    {
        private readonly LocadoraContext _locadoraContext;
        private readonly IRepositorioCliente _repositorioCliente;
        public CadastrarClienteHandler(LocadoraContext locadoraContext, 
            IRepositorioCliente repositorioCliente)
        {
            _locadoraContext = locadoraContext;
            _repositorioCliente = repositorioCliente;
        }

        public void Criar(string nome, string cpf, DateTime dataNascimento)
        {
            var cliente = new Cliente
            {
                Nome = nome,
                DataNascimento = dataNascimento,
                Cpf = cpf
            };

            using (var transacao = _locadoraContext.Database.BeginTransaction())
            {
                _repositorioCliente.Salvar(cliente);
                _locadoraContext.SaveChanges();
                transacao.Commit();
            }
        }
    }
}