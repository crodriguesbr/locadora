using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Dominio
{
    public interface IRepositorioCliente
    {
        void Salvar(Cliente cliente);
    }
}
