using System;
using System.Data;
using System.Data.SqlClient;
using pryBazanConexionSQL.AccesoDatos;

namespace pryBazanConexionSQL.Servicios
{
    public class ProductoServicio
    {
        private readonly ConexionBD conexionBD;

        public ProductoServicio()
        {
            conexionBD = new ConexionBD();
        }

        public DataTable ObtenerTodos()
        {
            return ObtenerTodos(0, string.Empty);
        }

        public DataTable ObtenerTodos(int cantidadMaxima, string filtro)
        {
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
            {
                string consulta = @"SELECT TOP (CASE WHEN @CantidadMaxima <= 0 THEN 2147483647 ELSE @CantidadMaxima END)
                                           p.IdProducto, p.NomProducto, p.IdGrupo, g.NombreGrupo, p.Precio
                                    FROM Productos p
                                    INNER JOIN Grupos g ON p.IdGrupo = g.IdGrupo
                                    WHERE (@Filtro = '' OR p.NomProducto LIKE @FiltroBusqueda OR g.NombreGrupo LIKE @FiltroBusqueda)
                                    ORDER BY p.NomProducto";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@CantidadMaxima", cantidadMaxima);
                    comando.Parameters.AddWithValue("@Filtro", filtro.Trim());
                    comando.Parameters.AddWithValue("@FiltroBusqueda", "%" + filtro.Trim() + "%");

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }

        public void Agregar(string nombre, int idGrupo, decimal precio)
        {
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();
                int idProducto = ObtenerProximoId(conexion);
                string consulta = @"INSERT INTO Productos (IdProducto, NomProducto, IdGrupo, Precio)
                                    VALUES (@IdProducto, @NomProducto, @IdGrupo, @Precio)";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdProducto", idProducto);
                    comando.Parameters.AddWithValue("@NomProducto", nombre);
                    comando.Parameters.AddWithValue("@IdGrupo", idGrupo);
                    comando.Parameters.AddWithValue("@Precio", precio);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void Modificar(int idProducto, string nombre, int idGrupo, decimal precio)
        {
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = @"UPDATE Productos
                                    SET NomProducto = @NomProducto, IdGrupo = @IdGrupo, Precio = @Precio
                                    WHERE IdProducto = @IdProducto";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdProducto", idProducto);
                    comando.Parameters.AddWithValue("@NomProducto", nombre);
                    comando.Parameters.AddWithValue("@IdGrupo", idGrupo);
                    comando.Parameters.AddWithValue("@Precio", precio);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int idProducto)
        {
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = "DELETE FROM Productos WHERE IdProducto = @IdProducto";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdProducto", idProducto);
                    comando.ExecuteNonQuery();
                }
            }
        }

        private int ObtenerProximoId(SqlConnection conexion)
        {
            using (SqlCommand comando = new SqlCommand("SELECT ISNULL(MAX(IdProducto), 0) + 1 FROM Productos", conexion))
            {
                return Convert.ToInt32(comando.ExecuteScalar());
            }
        }
    }
}
