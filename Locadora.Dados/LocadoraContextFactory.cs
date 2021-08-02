using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Dados
{
    public class LocadoraContextFactory : IDesignTimeDbContextFactory<LocadoraContext>
    {
        public LocadoraContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LocadoraContext>();
            optionsBuilder.UseSqlServer("Data Source=192.168.70.111,1433;Initial Catalog=locadoradb;User Id=sa;Password=abc,12345678");

            return new LocadoraContext(optionsBuilder.Options);
        }
    }
}
