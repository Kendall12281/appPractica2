using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryProducto : IRepositoryProducto
    {
        public void ActualizarProducto(producto producto)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    List<proveedor> collectionPlan = new List<proveedor>();
                    foreach (proveedor item in producto.proveedor)
                    {
                        collectionPlan.Add(item);
                    }
                    producto.proveedor.Clear();
                    ctx.producto.Add(producto);
                    ctx.Entry(producto).State = EntityState.Modified;
                    ctx.SaveChanges();

                    string[] selected = new string[collectionPlan.Count()];

                    for (int i = 0; i < collectionPlan.Count(); i++)
                    {
                        selected[i] = collectionPlan.ElementAt(i).id.ToString();
                    }


                    var selectedCollection = new HashSet<string>(selected);
                    ctx.Entry(producto).Collection(p => p.proveedor).Load();
                    var newCollection = ctx.proveedor
                        .Where(x => selectedCollection.Contains(x.id.ToString())).ToList();
                    producto.proveedor = newCollection;

                    ctx.Entry(producto).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TipoCategoria> Categorias()
        {
            using (MyContext ctx = new MyContext())
            {
                return ctx.TipoCategoria.ToList();
            }
        }

        public IEnumerable<producto> ListadoProducto()
        {
            IEnumerable<producto> lista = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                try
                {
                    lista = ctx.producto.Include(x=>x.TipoCategoria).Where(x => x.totalStock > 0).ToList();
                    return lista;

                }
                catch (Exception ex)
                {
                    string mensaje = "";
                    Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                    throw;
                }
            }
        }
        public producto ObtenerProductoID(int id)
        {
            producto producto = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                producto = ctx.producto.Include(x => x.TipoCategoria).Include(x => x.proveedor)
                    .Where(x => x.id == id).FirstOrDefault();
            }
            return producto;
        }

        public IEnumerable<proveedor> Proveedores()
        {
            using (MyContext ctx = new MyContext())
            {
                return ctx.proveedor.ToList();
            }
        }
    }
}
