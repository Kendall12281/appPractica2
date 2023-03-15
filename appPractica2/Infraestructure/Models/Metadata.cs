using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Xml.Linq;

namespace Infraestructure.Models
{
   
    internal partial class ProductoMetadata
    {
       
        public int id { get; set; }

        [Display(Name = "Categoría")]
        public int idCategoria { get; set; }


        [Display(Name = "Nombre del producto")]
        public string nombre { get; set; }

        [Display(Name = "Total de existencia")]
        public int totalStock { get; set; }

        [Display(Name = "Cantidad máxima")]
        public int cantMaxima { get; set; }

        [Display(Name = "Cantidad mínima")]
        public int cantMinima { get; set; }

        [Display(Name = "Costo")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double costoUnitario { get; set; }

        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        [Display(Name = "Imagen")]
        public byte[] imagen { get; set; }

        [Display(Name = "Categoría")]
        public virtual TipoCategoria TipoCategoria { get; set; }

        [Display(Name = "Estante")]
        public virtual ICollection<productoEstante> productoEstante { get; set; }

        [Display(Name = "Proveedor")]
        public virtual ICollection<proveedor> proveedor { get; set; }
    }
}
