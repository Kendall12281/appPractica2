using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class ProductoController : Controller
    {
        // GET: oProducto
        public ActionResult Index()
        {
            try
            {
                IServiceProducto _ServiceProducto = new ServiceProducto();
                return View(_ServiceProducto.ListadoProducto());
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Producto";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: oProducto/Details/5
        public ActionResult Details(int? id)
        {
            ServiceProducto _ServiceProducto = new ServiceProducto();
            producto oProducto = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                oProducto = _ServiceProducto.ObtenerProductoID(Convert.ToInt32(id));
                if (oProducto == null)
                {
                    TempData["Message"] = "No existe el Producto solicitado";
                    TempData["Redirect"] = "Producto";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(oProducto);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Producto";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: oProducto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: oProducto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private MultiSelectList listaProveedores(ICollection<proveedor> proveedores = null)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            IEnumerable<proveedor> lista = _ServiceProducto.Proveedores();
            //Seleccionar categorias
            int[] listaProveedoressSelect = null;
            if (proveedores != null)
            {
                listaProveedoressSelect = proveedores.Select(c => c.id).ToArray();
            }

            return new MultiSelectList(lista, "id", "nombreEmpresa", listaProveedoressSelect);
        }
        // GET: oProducto/Edit/5
        public ActionResult Edit(int id)
        {
            ServiceProducto service = new ServiceProducto();
            producto oProducto = service.ObtenerProductoID(id);
            List<SelectListItem> categorias = new List<SelectListItem>();
            foreach (var item in service.Categorias())
            {
                if(oProducto.idCategoria == item.id)
                {
                    categorias.Add(new SelectListItem
                    {
                        Value = item.id.ToString(),
                        Text = item.Descripcion,
                        Selected = true
                    });
                }
                else
                {
                    categorias.Add(new SelectListItem
                    {
                        Value = item.id.ToString(),
                        Text = item.Descripcion
                    });
                }
               
            }
            ViewBag.Categorias = categorias;
            ViewModelProducto model = new ViewModelProducto()
            {
                id = oProducto.id,
                nombre = oProducto.nombre,
                descripcion = oProducto.descripcion,
                totalStock = oProducto.totalStock,
                cantMaxima = oProducto.cantMaxima,
                cantMinima = oProducto.cantMinima,
                costoUnitario = oProducto.costoUnitario,
                listaProveedores = oProducto.proveedor.ToList(),
                categoriasList = categorias
            };
            ViewBag.Proveedores = listaProveedores(oProducto.proveedor);
            return View(model);
        }

        // POST: oProducto/Edit/5
        [HttpPost]
        public ActionResult Edit(ViewModelProducto oProducto, string[] proveedoresSeleccionados)
        {
            try
            {
                // TODO: Add update logic here
                if (!(ModelState.IsValid))
                {
                    return View(oProducto);
                }
                ServiceProducto serviceProducto = new ServiceProducto();

                List<proveedor> listaProveedores = new List<proveedor>();
                
                foreach (var item in serviceProducto.Proveedores())
                {
                    if(proveedoresSeleccionados.Contains(item.id.ToString()))
                    {
                        listaProveedores.Add(item);
                    }
                }

                producto product = new producto()
                {
                    id = oProducto.id,
                    nombre = oProducto.nombre,
                    descripcion = oProducto.descripcion,
                    totalStock = oProducto.totalStock,
                    cantMaxima = oProducto.cantMaxima,
                    cantMinima = oProducto.cantMinima,
                    costoUnitario = oProducto.costoUnitario,
                    proveedor = listaProveedores,
                    idCategoria = oProducto.idCategoria

                };

                serviceProducto.ActualizarProducto(product);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: oProducto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: oProducto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
