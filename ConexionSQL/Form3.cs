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
    public partial class FrmCliente : Form
    {
        public FrmCliente()
        {
            InitializeComponent();
        }
        //Declarar variables de conexion y el comand que usaremos en el formulario
        ClsConexion DB_CONN = new ClsConexion();
        SqlCommand cm = new SqlCommand();
        //Declaramos la pantalla principal
        FrmPrincipal InicioPrincipal;

        private void label1_Click(object sender, EventArgs e)
        {

        }
        DataTable dt;
        public DataTable GetData(string consulta)
        {
            cm = new SqlCommand(consulta, DB_CONN.DB_CONN);
            SqlDataAdapter adp = new SqlDataAdapter(cm);
            dt = new DataTable();
            adp.Fill(dt);
            return dt;
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            //Cargar la informacion de todos los clientes registrados cuando se abre la pantalla
            this.dataGridView1.DataSource = GetData("GetAllCustomersOrdered");
        }

        //El boton cancelar nos regresara a la pantalla principal
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicioPrincipal = new FrmPrincipal();
            InicioPrincipal.Show();
            this.Hide();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (this.txtIdCliente.Text == "")
            {
                MessageBox.Show("Por favor llena los campos faltantes: Id Cliente");
            }
            else
            {
                try
                {
                    //cm = new SqlCommand("SELECT CompanyName, ContactName, City, Phone FROM Customers WHERE CustomersID='" + this.txtIdCliente.Text + "'", DB_CONN.DB_CONN);
                    //cm.CommandType = CommandType.Text;
                    cm = new SqlCommand("GetAllCustomersOrdered", DB_CONN.DB_CONN);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("@IdCliente", SqlDbType.VarChar).Value = this.txtIdCliente.Text;
                    SqlDataReader dr = cm.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        this.txtIdCliente.Text = dr.GetString(0);
                        this.txtCompañia.Text = dr.GetString(1);
                        this.txtContacto.Text = dr.GetString(2);
                        this.txtCiudad.Text = dr.GetString(3);
                        this.txtTelefono.Text = dr.GetString(4);
                        dr.Close();
                        this.txtIdCliente.Enabled = false;
                        this.btnAgregar.Enabled = true;
                        this.btnEditar.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("El cliente no Existe.");
                        this.txtIdCliente.Enabled = true;
                        this.btnAgregar.Enabled = true;
                        this.btnEditar.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error" + ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.txtIdCliente.Text == "" || this.txtCompañia.Text == "")
            {
                MessageBox.Show("Por favor llena los campos faltantes: Id Cliente, Nombre Compañia");
            }
            else
            {
                try
                {
                    String st = "UPDATE Customers SET CompanyName = '" + this.txtCompañia.Text + "',ContactName ='" + this.txtContacto.Text + "'," +
                        "City = '" + this.txtCiudad.Text + "', Phone = '" + this.txtTelefono.Text + "' WHERE CustomersID = '" + this.txtIdCliente.Text + "'";
                    cm = new SqlCommand(st, DB_CONN.DB_CONN);
                    cm.ExecuteNonQuery();
                    MessageBox.Show("El cliente Actalizado");

                    this.btnCancelar.PerformClick();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ha ocurrido un error" + ex.Message);
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (this.txtIdCliente.Text == "" || this.txtCompañia.Text == "")
            {
                MessageBox.Show("Por favor llena los campos faltantes: Id Cliente, Nombre Compañia");
            }
            else
            {
                try
                {
                    String st = "INSERT INTO Customers (CustomerID, CompanyName, ContactName, City, Phone)VALUES" +
                        "('" + this.txtIdCliente.Text + "','" + this.txtCompañia.Text + "','" + this.txtContacto.Text +
                        "','" + this.txtCiudad.Text + "','" + this.txtTelefono.Text + "')";
                    cm = new SqlCommand(st, DB_CONN.DB_CONN);
                    cm.ExecuteNonQuery();
                    MessageBox.Show("Cliente Guardado");

                    this.btnCancelar.PerformClick();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ha ocurrido un error" + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnPrincipal_Click(object sender, EventArgs e)
        {
            InicioPrincipal = new FrmPrincipal();
            InicioPrincipal.Show();
            this.Hide();
        }
    }
}
