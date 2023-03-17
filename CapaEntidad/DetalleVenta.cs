using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad


{

 //   [IdDetalleVenta] [int] IDENTITY(1,1) NOT NULL,

 //   [IdVenta] [int] NULL,
	//[IdProducto] [int] NULL,
	//[Cantidad] [int] NULL,
	//[Toatal] [decimal] (10, 2) NULL,
    public class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public Producto oProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Toatal { get; set; }
        public string IdTransaccion { get; set; }
    }
}
