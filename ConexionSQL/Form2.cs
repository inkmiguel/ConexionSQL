﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionSQL
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        //Declarar cada formulario
        FrmCliente Cliente;

        private void btnClientes_Click(object sender, EventArgs e)
        {
            //Llamar cada formulario
            Cliente = new FrmCliente();
            Cliente.Show();
            this.Hide();
        }
    }
}
