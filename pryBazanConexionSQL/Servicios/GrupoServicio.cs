using System.Data;
using System.Data.SqlClient;
using pryBazanConexionSQL.AccesoDatos;

namespace pryBazanConexionSQL.Servicios
{
    public class GrupoServicio
    {
        private readonly ConexionBD conexionBD;

        public GrupoServicio()
        {
            conexionBD = new ConexionBD();
        }

        public DataTable ObtenerTodos()
        {
            using (SqlConnection conexion = conexionBD.ObtenerConexion())
            {
                string consulta = "SELECT IdGrupo, NombreGrupo FROM Grupos ORDER BY NombreGrupo";
                using (SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion))
                {
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    return tabla;
                }
            }
        }
    }
}
