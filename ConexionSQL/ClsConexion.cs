using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Configuracion
using System.Configuration;
//Base de datos SQL
using System.Data.SqlClient;

namespace ConexionSQL
{
    internal class ClsConexion
    {
        public SqlConnection DB_CONN;

        public ClsConexion()
        {
            try
            {
                DB_CONN = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString + ";MultipleActiveResultSets=True");
                DB_CONN.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar a la base de datos"+ex.ToString());
            }
        }
    }
}
