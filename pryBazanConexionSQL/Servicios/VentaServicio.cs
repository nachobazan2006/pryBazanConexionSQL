using System;
using System.Data;
using System.Data.SqlClient;
using pryBazanConexionSQL.AccesoDatos;

namespace pryBazanConexionSQL.Servicios
{
    public class VentaServicio
    {
        private readonly ConexionBD conexionBD;

        public VentaServicio()
        {
            conexionBD = new ConexionBD();
        }

        public DataTable ObtenerVentas(int? idVendedor, int? idProducto, int? idGrupo, DateTime? desde, DateTime? hasta, int cantidadMaxima)
        {
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
            {
                string consulta = @"SELECT TOP (@CantidadMaxima)
                                           v.[Cod Vendedor] AS CodVendedor,
                                           ven.NombreVendedor,
                                           v.[Cod Producto] AS CodProducto,
                                           p.NomProducto,
                                           g.NombreGrupo,
                                           v.Fecha,
                                           v.Kilos,
                                           p.Precio,
                                           v.Kilos * p.Precio AS Total
                                    FROM Ventas v
                                    INNER JOIN Vendedores ven ON v.[Cod Vendedor] = ven.IdVendedor
                                    INNER JOIN Productos p ON v.[Cod Producto] = p.IdProducto
                                    INNER JOIN Grupos g ON p.IdGrupo = g.IdGrupo
                                    WHERE (@IdVendedor IS NULL OR v.[Cod Vendedor] = @IdVendedor)
                                      AND (@IdProducto IS NULL OR v.[Cod Producto] = @IdProducto)
                                      AND (@IdGrupo IS NULL OR p.IdGrupo = @IdGrupo)
                                      AND (@Desde IS NULL OR v.Fecha >= @Desde)
                                      AND (@Hasta IS NULL OR v.Fecha <= @Hasta)
                                    ORDER BY v.Fecha DESC";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdVendedor", (object)idVendedor ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@IdProducto", (object)idProducto ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@IdGrupo", (object)idGrupo ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Desde", (object)desde ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Hasta", (object)hasta ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@CantidadMaxima", cantidadMaxima);

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }

        public void Agregar(int idVendedor, int idProducto, DateTime fecha, double kilos)
        {
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = @"INSERT INTO Ventas ([Cod Vendedor], [Cod Producto], Fecha, Kilos)
                                    VALUES (@IdVendedor, @IdProducto, @Fecha, @Kilos)";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdVendedor", idVendedor);
                    comando.Parameters.AddWithValue("@IdProducto", idProducto);
                    comando.Parameters.AddWithValue("@Fecha", fecha);
                    comando.Parameters.AddWithValue("@Kilos", kilos);
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
