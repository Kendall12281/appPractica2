using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Infraestructure.Models.ViewModel
{
    public class ViewModelProducto
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        [MinLength(4, ErrorMessage = "Al menos 4 caracteres")]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }
        public int idCategoria 
        {
            get
            {
                return int.Parse(idCategoriaToString);
            }
        }
        [Display(Name ="Categoria")]
        public string idCategoriaToString { get; set; }
        [Required]
        [Display(Name = "Total en Stock")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Debe de ingresar un numero entero")]
        public int totalStock { get; set; }
        [Required]
        [Display(Name = "Cantidad Maxima")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Debe de ingresar un numero entero")]
        public int cantMaxima { get; set; }
        [Required]
        [Display(Name = "Cantidad Minima")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Debe de ingresar un numero entero")]
        public int cantMinima { get; set; }
        [Required]
        [Display(Name = "Costo Unitario")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        public double costoUnitario { get; set; }
        public IEnumerable<proveedor> listaProveedores { get; set; }
        public List<SelectListItem> categoriasList { get; set; }




    }
}
