using System.Configuration;
using System.Data.SqlClient;

namespace pryBazanConexionSQL.AccesoDatos
{
    public class ConexionBD
    {
        private readonly string cadenaConexion;

        public ConexionBD()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionVerduleros"].ConnectionString;
        }

        public string CadenaConexion
        {
            get { return cadenaConexion; }
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
