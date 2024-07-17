using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Presentacion_IU
{
    public partial class Inventario : Form
    {
        public Inventario()
        {
            InitializeComponent();
            oBLLComputadora = new BLLComputadora();
            oBLLCelular = new BLLCelular();
            oBLLProveedor = new BLLProveedor();
            oBECelular = new BECelular();
            oBEComputadora = new BEComputadora();
        }

        BLLComputadora oBLLComputadora;
        BLLCelular oBLLCelular;
        BLLProveedor oBLLProveedor;
        BEComputadora oBEComputadora;
        BECelular oBECelular;


        void cargarComboProveedores()
        {
            this.comboBoxTipo.DataSource = oBLLProveedor.ListarTodo();
            this.comboBoxTipo.ValueMember = "Codigo";
            this.comboBoxTipo.DisplayMember = "RazonSocial";
            this.comboBoxTipo.Refresh();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Se toman los datos y se determina que tipo de producto es
                obtenerDatosTxt();
                if (this.comboBoxTipo.Text == "Computadora")
                {
                    if (this.comboBoxProveedores.SelectedValue != null)
                    {
                        oBEComputadora.Proveedor = (BEProveedor)this.comboBoxProveedores.SelectedItem;
                        if (oBLLComputadora.Guardar(oBEComputadora) == true)
                        {
                            cargarGrillaProductos("Computadora");
                            MessageBox.Show("Producto cargado!");
                            limpiarText();
                        }
                        else { throw new Exception("Error al cargar el producto"); }
                    }
                    else { throw new Exception("Debe seleccionar un proveedor en la grilla!"); }
                }
                else if (this.comboBoxTipo.Text == "Celular")
                {
                    if (this.comboBoxProveedores.SelectedValue != null)
                    {
                        oBECelular.Proveedor = (BEProveedor)this.comboBoxProveedores.SelectedItem;
                        if (oBLLCelular.Guardar(oBECelular) == true)
                        {
                            cargarGrillaProductos("Celular");
                            limpiarText();
                            MessageBox.Show("Producto cargado!");
                        }
                        else { throw new Exception("Error al cargar el producto"); }
                    }
                    else { throw new Exception("Debe seleccionar un proveedor en la grilla!"); }

                }
                else
                {
                    throw new Exception("Para ingresar un producto debe seleccionar un tipo");
                }


            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void dataGridViewInventario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // se toma el dato seleccionado en la grilla dependiendo su categoria
                if (comboBoxTipo.Text == "Computadora") { oBEComputadora = (BEComputadora)this.dataGridViewInventario.SelectedRows[0].DataBoundItem; mostrarDatosTxt(oBEComputadora); }
                if (comboBoxTipo.Text == "Celular") { oBECelular = (BECelular)this.dataGridViewInventario.SelectedRows[0].DataBoundItem; mostrarDatosTxt(oBECelular); }

            }
            catch (Exception) { }
        }

        void mostrarDatosTxt(BEProducto oBEProducto)
        {
            
            this.textBoxNombreProducto.Text = oBEProducto.Nombre;
            this.textBoxDescripcion.Text = oBEProducto.Descripcion;
            this.textBoxMarca.Text = oBEProducto.Marca;
            this.textBoxPrecio.Text = oBEProducto.Precio.ToString();
            this.textBoxProveedores.Text = oBEProducto.Proveedor.ToString();
            this.textBoxStock.Text = oBEProducto.Stock.ToString();
            this.comboBoxProveedores.Text = oBEProducto.Proveedor.RazonSocial;
        }


        void obtenerDatosTxt()
        {
            try
            {
                if (comboBoxTipo.Text == "Computadora")
                {
                    oBEComputadora.Nombre = textBoxNombreProducto.Text;
                    oBEComputadora.Descripcion = textBoxDescripcion.Text;
                    oBEComputadora.Marca = textBoxMarca.Text;
                    oBEComputadora.Precio = double.Parse(textBoxPrecio.Text);
                    oBEComputadora.Stock = int.Parse(textBoxStock.Text);
                    oBEComputadora.Tipo = comboBoxTipo.Text;
                }
                if (comboBoxTipo.Text == "Celular")
                {
                   
                    oBECelular.Nombre = textBoxNombreProducto.Text;
                    oBECelular.Descripcion = textBoxDescripcion.Text;
                    oBECelular.Marca = textBoxMarca.Text;
                    oBECelular.Precio = double.Parse(textBoxPrecio.Text);
                    oBECelular.Stock = int.Parse(textBoxStock.Text);
                    oBECelular.Tipo = comboBoxTipo.Text;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }




        public void cargarGrillaProductos(string Tipo)
        {
            // dependieno el valor del combobox se muestran en la grilla los productos de ese tipo
            if (Tipo == "Computacdora")
            {
                this.dataGridViewInventario.DataSource = null;
                this.dataGridViewInventario.DataSource = oBLLComputadora.ListarTodo();
            }
            if (Tipo == "Celular")
            {
                this.dataGridViewInventario.DataSource = null;
                this.dataGridViewInventario.DataSource = oBLLCelular.ListarTodo();
            }
            this.dataGridViewInventario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void Inventario_Load(object sender, EventArgs e)
        {
            cargarComboProveedores();
        }

        private void comboBoxTipo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (this.comboBoxTipo.Text == "Computadora")
            {
                limpiarText();
                cargarGrillaProductos("Computadora");
            }
            if (this.comboBoxTipo.Text == "Celular")
            {
                limpiarText();
                cargarGrillaProductos("Celular");
            }
        }

        void limpiarText()
        {
            
            this.textBoxNombreProducto.Text = string.Empty;
            this.textBoxDescripcion.Text = string.Empty;
            this.textBoxMarca.Text = string.Empty;
            this.textBoxProveedores.Text = string.Empty;
            this.textBoxPrecio.Text = string.Empty;
            this.textBoxStock.Text = string.Empty;
        }

    }
}
