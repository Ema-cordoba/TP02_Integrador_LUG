using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion_IU
{
    public partial class FormInicio : Form
    {
        public FormInicio()
        {
            InitializeComponent();
        }


        public BEUsuario objBEUsuario = new BEUsuario();

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCliente formCliente = new FormCliente();
            formCliente.MdiParent = this;
            formCliente.Show();
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEmpleados formEmpleados = new FormEmpleados();
            formEmpleados.MdiParent = this;
            formEmpleados.Show();
        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCompras formCompras = new FormCompras();
            formCompras.MdiParent = this;
            formCompras.Show();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormVenta formVenta = new FormVenta();
            formVenta.MdiParent = this;
            formVenta.Show();
        }

        private void reportesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReportes formReportes = new FormReportes();
            formReportes.MdiParent = this;
            formReportes.Show();
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inventario formInventario = new Inventario();
            formInventario.MdiParent = this;
            formInventario.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
