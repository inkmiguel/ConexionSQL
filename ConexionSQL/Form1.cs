using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ConexionSQL
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        ClsConexion DB_CONN = new ClsConexion();
        SqlCommand cm = new SqlCommand();
        FrmPrincipal InicioPrincipal;
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.txtUsuario.Text == "" || this.txtContraseña.Text == "")
            {
                MessageBox.Show("Por favor llena los campos faltantes");
            }
            else
            {
                try
                {
                    cm = new SqlCommand("SELECT Login, Password FROM Usuario WHERE Login='" + txtUsuario.Text + "' AND Password='" + txtContraseña.Text + "'", DB_CONN.DB_CONN);
                    cm.CommandType = CommandType.Text;
                    SqlDataReader dr = cm.ExecuteReader();
                    if (dr.Read())
                    {
                        InicioPrincipal = new FrmPrincipal();
                        InicioPrincipal.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Datos incorrectos.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error " + ex.Message);
                }
            }
        }
    }
}
