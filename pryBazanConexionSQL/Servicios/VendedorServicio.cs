using System;
using System.Data;
using System.Data.SqlClient;
using pryBazanConexionSQL.AccesoDatos;

namespace pryBazanConexionSQL.Servicios
{
    public class VendedorServicio
    {
        private readonly ConexionBD conexionBD;

        public VendedorServicio()
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
                                           IdVendedor, NombreVendedor, FechaAlta, NIF, FechaNac,
                                           Direccion, Poblacion, CodPostal, Telefon, EstalCivil, [Guap@]
                                    FROM Vendedores
                                    WHERE (@Filtro = ''
                                           OR NombreVendedor LIKE @FiltroBusqueda
                                           OR NIF LIKE @FiltroBusqueda
                                           OR Poblacion LIKE @FiltroBusqueda)
                                    ORDER BY NombreVendedor";
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

        public void Agregar(string nombre, string nif, string telefono, string poblacion, string direccion)
        {
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();
                int idVendedor = ObtenerProximoId(conexion);
                string consulta = @"INSERT INTO Vendedores
                                    (IdVendedor, NombreVendedor, FechaAlta, NIF, Direccion, Poblacion, Telefon, [Guap@])
                                    VALUES
                                    (@IdVendedor, @NombreVendedor, @FechaAlta, @NIF, @Direccion, @Poblacion, @Telefon, @Guapo)";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdVendedor", idVendedor);
                    comando.Parameters.AddWithValue("@NombreVendedor", nombre);
                    comando.Parameters.AddWithValue("@FechaAlta", DateTime.Today);
                    comando.Parameters.AddWithValue("@NIF", nif);
                    comando.Parameters.AddWithValue("@Direccion", direccion);
                    comando.Parameters.AddWithValue("@Poblacion", poblacion);
                    comando.Parameters.AddWithValue("@Telefon", telefono);
                    comando.Parameters.AddWithValue("@Guapo", false);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void Modificar(int idVendedor, string nombre, string nif, string telefono, string poblacion, string direccion)
        {
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = @"UPDATE Vendedores
                                    SET NombreVendedor = @NombreVendedor,
                                        NIF = @NIF,
                                        Telefon = @Telefon,
                                        Poblacion = @Poblacion,
                                        Direccion = @Direccion
                                    WHERE IdVendedor = @IdVendedor";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdVendedor", idVendedor);
                    comando.Parameters.AddWithValue("@NombreVendedor", nombre);
                    comando.Parameters.AddWithValue("@NIF", nif);
                    comando.Parameters.AddWithValue("@Telefon", telefono);
                    comando.Parameters.AddWithValue("@Poblacion", poblacion);
                    comando.Parameters.AddWithValue("@Direccion", direccion);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int idVendedor)
        {
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();
                string consulta = "DELETE FROM Vendedores WHERE IdVendedor = @IdVendedor";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdVendedor", idVendedor);
                    comando.ExecuteNonQuery();
                }
            }
        }

        private int ObtenerProximoId(SqlConnection conexion)
        {
            using (SqlCommand comando = new SqlCommand("SELECT ISNULL(MAX(IdVendedor), 0) + 1 FROM Vendedores", conexion))
            {
                return Convert.ToInt32(comando.ExecuteScalar());
            }
        }
    }
}
