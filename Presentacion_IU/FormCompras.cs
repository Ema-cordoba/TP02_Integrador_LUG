using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using BE;

namespace Presentacion_IU
{
    public partial class FormCompras : Form
    {
        public FormCompras()
        {
            InitializeComponent();
            oBLLCliente = new BLLCliente();
            oBECliente = new BECliente();
            oBEVenta = new BEVenta();
        }

        BLLCliente oBLLCliente;
        BECliente oBECliente;
        BEVenta oBEVenta;


        private void FormCompras_Load(object sender, EventArgs e)
        {
            this.dataGridViewClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewComprasHechas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            cargarGrillaClientes();
        }


        void cargarDGVentasClientes()
        {
            this.dataGridViewComprasHechas.DataSource = null;
            this.dataGridViewComprasHechas.DataSource = oBLLCliente.ListarVentasCliente(oBECliente);
            this.dataGridViewComprasHechas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }


        private void dataGridViewCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // al clickearse una venta se muestran sus detalles en una grilla
                this.labelDetalle.Visible = true;
                this.dataGridViewDetalle.Visible = true;
                oBEVenta = (BEVenta)this.dataGridViewComprasHechas.SelectedRows[0].DataBoundItem;
                cargarDGDetalles();
            }
            catch (Exception) { }
        }


        void cargarDGDetalles()
        {
            this.dataGridViewDetalle.DataSource = null;
            this.dataGridViewDetalle.DataSource = oBEVenta.Productos;
            this.dataGridViewDetalle.Columns.Remove("Proveedor");
            this.dataGridViewDetalle.Columns.Remove("Marca");
            this.dataGridViewDetalle.Columns.Remove("Descripcion");
            this.dataGridViewDetalle.Columns[3].HeaderText = "Cantidad";
            this.dataGridViewDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void dataGridViewClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Si se clickea una venta de la grilla desaparece la grilla de detalles de la venta
                if (dataGridViewDetalle.Visible == true && labelDetalle.Visible == true)
                {
                    this.labelDetalle.Visible = false;
                    this.dataGridViewDetalle.Visible = false;
                }
                oBECliente = (BECliente)this.dataGridViewClientes.SelectedRows[0].DataBoundItem;
                cargarDGVentasClientes();
            }
            catch (Exception) { }
        }


        void cargarGrillaClientes()
        {
            this.dataGridViewClientes.DataSource = null;
            this.dataGridViewClientes.DataSource = oBLLCliente.ListarTodo();
            this.dataGridViewClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
    }
}
