using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProducto : IServiceProducto
    {
        public void ActualizarProducto(producto producto)
        {
            IRepositoryProducto repositoryProducto = new RepositoryProducto();
            repositoryProducto.ActualizarProducto(producto);
        }

        public List<TipoCategoria> Categorias()
        {
            IRepositoryProducto repositoryProducto = new RepositoryProducto();
            return repositoryProducto.Categorias();
        }

        public IEnumerable<producto> ListadoProducto()
        {
            IRepositoryProducto repositoryProducto = new RepositoryProducto();
            return repositoryProducto.ListadoProducto();
        }

        public producto ObtenerProductoID(int id)
        {
            IRepositoryProducto repositoryProducto = new RepositoryProducto();
            return repositoryProducto.ObtenerProductoID(id);           
        }

        public IEnumerable<proveedor> Proveedores()
        {
            IRepositoryProducto repositoryProducto = new RepositoryProducto();
            return repositoryProducto.Proveedores();
        }
    }
}
