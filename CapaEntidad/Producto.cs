using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
	//[IdProducto] [int] IDENTITY(1,1) NOT NULL,
	//[Descripcion] [varchar] (100) NULL,
	//[IdMarca] [int] NULL,
	//[IdCategoria] [int] NULL,
	//[Precio] [decimal] (10, 2) NULL,
	//[Stock] [int] NULL,
	//[RutaImagenes] [varchar] (100) NULL,
	//[NombreImagenes] [varchar] (100) NULL,
	//[Activo] [bit] NULL,
	//[FechaRegistro] [datetime] NULL,

    public class Producto
    {
        public int IdProducto { get; set; }
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		public Marca oMarca { get; set; }		
		public Categoria oCategoria { get; set; }
		public decimal Precio { get; set; }


		public string PrecioTexto { get; set; }
		public int Stock { get; set; }
		public string RutaImagenes { get; set; }
		public string NombreImagenes { get; set; }
		public bool Activo { get; set; }

		public string Base64 { get; set; }
		public string Extension { get; set; }
	}
}
