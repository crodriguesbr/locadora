using Locadora.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Dados
{
    public class RepositorioCliente: IRepositorioCliente
    {
        private readonly LocadoraContext _locadoraContext;
        public RepositorioCliente(LocadoraContext locadoraContext)
        {
            _locadoraContext = locadoraContext;
        }

        public void Salvar(Cliente cliente)
        {
            _locadoraContext.Add(cliente);
        }
    }
}
