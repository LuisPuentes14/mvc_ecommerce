using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
	//[IdVenta] [int] IDENTITY(1,1) NOT NULL,
	//[IdCliente] [int] NULL,
	//[TotalProducto] [int] NULL,
	//[MontoTotal] [decimal] (10, 2) NULL,
	//[Contacto] [varchar] (50) NULL,
	//[IdDistrito] [varchar] (10) NULL,
	//[Telefono] [varchar] (50) NULL,
	//[Direccion] [varchar] (500) NULL,
	//[IdTransaccion] [varchar] (50) NULL,
	//[FechaRegistro] [datetime] NULL,

    public class Venta
    {
        public int IdVenta { get; set; }
		public int IdCliente { get; set; }
		public int TotalProducto { get; set; }
		public decimal MontoTotal { get; set; }
		public string Contacto { get; set; }
		public string IdDistrito { get; set; }
		public string Telefono { get; set; }
		public string Direccion { get; set; }
		public string IdTransaccion { get; set; }


		public List<DetalleVenta> oDetalleVenta { get; set; }
	}
}
