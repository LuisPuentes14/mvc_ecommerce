using CapaEntidad;
using CapaNegocio;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionAdmin.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult Categoria()
        {
            return View();
        }

        public ActionResult Marca()
        {
            return View();
        }

        public ActionResult Producto()
        {
            return View();
        }

        #region CATEGORIA
        public JsonResult ListarCategorias()
        {

            List<Categoria> oLista = new List<Categoria>();
            oLista = new CN_Categoria().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdCategoria == 0)
            {
                resultado = new CN_Categoria().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Categoria().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Categoria().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);



        }
        #endregion

        #region MARCA
        public JsonResult ListarMarcas()
        {

            List<Marca> oLista = new List<Marca>();
            oLista = new CN_Marca().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarMarcas(Marca objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdMarca == 0)
            {
                resultado = new CN_Marca().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Marca().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EliminarMarcas(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Marca().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);



        }

        #endregion

        #region PRODUCTO

        public JsonResult ListarProdcuto()
        {

            List<Producto> oLista = new List<Producto>();
            oLista = new CN_Producto().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarProducto(string objeto, HttpPostedFileBase archivoImagen)
        {

            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;

            Producto oProdcuto = new Producto();
            oProdcuto = JsonConvert.DeserializeObject<Producto>(objeto);

            decimal precio;

            if (decimal.TryParse(oProdcuto.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-CO"), out precio))
            {
                oProdcuto.Precio = precio;

            }
            else
            {

                return Json(new { operacionExitosa = false, mensaje = "El formato del precio debe ser ##.##" }, JsonRequestBehavior.AllowGet);

            }




            if (oProdcuto.IdProducto == 0)
            {
                int idProductoGenerado = new CN_Producto().Registrar(oProdcuto, out mensaje);

                if (idProductoGenerado != 0)
                {
                    oProdcuto.IdProducto = idProductoGenerado;
                }
                else
                {
                    operacion_exitosa = false;

                }


            }
            else
            {
                operacion_exitosa = new CN_Producto().Editar(oProdcuto, out mensaje);
            }

            if (operacion_exitosa)
            {
                if (archivoImagen != null) {

                    string ruta_guardar = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extencion = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oProdcuto.IdProducto.ToString(), extencion);

                    try
                    {

                        archivoImagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));

                    }
                    catch (Exception ex)
                    {

                        string msg = ex.Message;
                        guardar_imagen_exito = false;
                    }

                    if (guardar_imagen_exito)
                    {

                        oProdcuto.RutaImagenes = ruta_guardar;
                        oProdcuto.NombreImagenes = nombre_imagen;
                        bool rspta = new CN_Producto().GuardarDatosImagen(oProdcuto, out mensaje);
                    }
                    else {
                        mensaje = "Se guardo el producto pero hubo problemas con la imagen";
                    }
                    

                
                }

            }


            return Json(new { resultado = operacion_exitosa, idGenerado=oProdcuto.IdProducto, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }


        #endregion

    }

}