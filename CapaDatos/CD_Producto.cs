using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Producto
    {

        public List<Producto> Listar()
        {

            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT");
                    sb.AppendLine("P.IdProducto, P.Nombre,P.Descripcion");
                    sb.AppendLine("M.IdMarca, M.Descripcion AS DesMarca");
                    sb.AppendLine("C.IdCategoria, C.Descripcion AS DesCategoria");
                    sb.AppendLine("P.Precio,P.Stock,P.RutaImagenes,P.NombreImagenes,P.Activo");
                    sb.AppendLine("FROM PRODUCTO P");
                    sb.AppendLine("INNER JOIN MARCA M ON M.IdMarca = P.IdMarca");
                    sb.AppendLine("INNER JOIN CATEGORIA C ON C.IdCategoria = P.IdCategoria");


                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                oMarca = new Marca()
                                {
                                    IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                    Descripcion = dr["DesMarca"].ToString()
                                },
                                oCategoria = new Categoria()
                                {
                                    IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                    Descripcion = dr["DesCategoria"].ToString()
                                },
                                Stock = Convert.ToInt32(dr["Stock"]),
                                Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-CO")),
                                RutaImagenes = dr["RutaImagenes"].ToString(),
                                NombreImagenes = dr["NombreImagenes"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])

                            });

                        }

                    }

                }

            }
            catch (Exception)
            {
                lista = new List<Producto>();
            }

            return lista;
        }


        public int Registrar(Producto obj, out string Mensaje)
        {

            int idautogenerado = 0;

            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {


                    SqlCommand cmd = new SqlCommand("sp_RegistrarProducto", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;

                    oconexion.Open();
                    cmd.ExecuteReader();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }

            return idautogenerado;
        }

        public bool Editar(Producto obj, out string Mensaje)
        {

            bool idautogenerado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {


                    SqlCommand cmd = new SqlCommand("sp_EditarProducto", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;

                    oconexion.Open();
                    cmd.ExecuteReader();

                    idautogenerado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                idautogenerado = false;
                Mensaje = ex.Message;
            }

            return idautogenerado;
        }

        public bool GuardarDatosImagen(Producto oProducto, out string Mensjase) {
            bool resultado = false;
            Mensjase = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "UPDATE PRODUCTO SET RutaImagen = @rutaimagen, NombreImagen = @nombreimagen WHERE IdProducto = @idprodcuto";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@rutaimagen", oProducto.RutaImagenes);
                    cmd.Parameters.AddWithValue("@nombreimagen", oProducto.NombreImagenes);
                    cmd.Parameters.AddWithValue("@idprodcuto", oProducto.IdProducto);

                    oconexion.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {

                        resultado = true;

                    }
                    else {

                        Mensjase = "No se pudo actualizar imagen ";
                    }


                   
                }

            }
            catch (Exception ex)
            {
                resultado=false;
                Mensjase=ex.Message;
            }

            return resultado;

        }

        public bool Eliminar(int id, out string Mensaje)
        {

            bool resultado = false;

            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {


                    SqlCommand cmd = new SqlCommand("sp_EliminarProducto", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IdProducto", id);
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;

                    oconexion.Open();
                    cmd.ExecuteReader();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;
        }

    }      
    
}
