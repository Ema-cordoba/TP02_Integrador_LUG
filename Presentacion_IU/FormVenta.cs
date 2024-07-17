using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Presentacion_IU
{
    public partial class FormVenta : Form
    {
        public FormVenta()
        {
            InitializeComponent();
            oBLLCliente = new BLLCliente();
            oBLLComputadora = new BLLComputadora();
            oBLLCelular = new BLLCelular();
            oBEComputadora = new BEComputadora();
            oBECelular = new BECelular();
            oBEVenta = new BEVenta();
            oBLLVenta = new BLLVenta();
            oBECliente = new BECliente();
            oBEEmpleado = new BEEmpleado();
            oBLLEmpleado = new BLLEmpleado();
        }

        public BEEmpleado oBEEmpleado;
        public List<BEProducto> listaCarrito = new List<BEProducto>();
        BLLCliente oBLLCliente;
        BECliente oBECliente;
        BLLComputadora oBLLComputadora;
        BLLCelular oBLLCelular;
        BEComputadora oBEComputadora;
        BECelular oBECelular;
        BEVenta oBEVenta;
        BLLVenta oBLLVenta;
        BLLEmpleado oBLLEmpleado;


        private void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                // se agregan los productos al carrito
                if (this.dataGridViewProductos.SelectedRows.Count != 0)
                {
                    if (comboBoxSeccion.Text == "Computadora" && numericUpDown1.Value != 0)
                    {
                        oBEComputadora = (BEComputadora)this.dataGridViewProductos.SelectedRows[0].DataBoundItem;

                        int cantidad = int.Parse(numericUpDown1.Value.ToString());

                        BEComputadora objBEProdAdquirido = new BEComputadora(oBEComputadora.Codigo, oBEComputadora.Nombre, oBEComputadora.Marca, oBEComputadora.Descripcion, oBEComputadora.Tipo, oBEComputadora.Precio, cantidad, oBEComputadora.Proveedor);


                        //Si el producto ya habia sido agregado se suma su cantidad
                        if (listaCarrito.Exists(x => x.Codigo == oBEComputadora.Codigo) == false)
                        {
                            // se verifica que el stock a adquirir sea correcto
                            if (oBEComputadora.Stock - cantidad < 0)
                            {
                                throw new Exception("No hay stock disponible del producto");
                            }
                            oBEComputadora.Stock -= cantidad;
                            listaCarrito.Add(objBEProdAdquirido);
                        }
                        else
                        {
                            if (oBEComputadora.Stock - cantidad < 0)
                            {
                                throw new Exception("No hay stock disponible del producto");
                            }
                            oBEComputadora.Stock -= cantidad;
                            listaCarrito.Find(x => x.Codigo == objBEProdAdquirido.Codigo).Stock += cantidad;
                            cargarGrillaCarrito();
                        }

                        this.numericUpDown1.Value = 0;
                        oBLLComputadora.Guardar(oBEComputadora);
                        cargarGrillaProdComputadora();
                        cargarGrillaCarrito();
                    }
                    else if (comboBoxSeccion.Text == "Celular" && numericUpDown1.Value != 0)
                    {
                        oBECelular = (BECelular)this.dataGridViewProductos.SelectedRows[0].DataBoundItem;
                        int cantidad = int.Parse(numericUpDown1.Value.ToString());
                        BECelular objBEProdAdquirido = new BECelular(oBECelular.Codigo, oBECelular.Nombre, oBECelular.Marca, oBECelular.Descripcion, oBECelular.Tipo, oBECelular.Precio, cantidad, oBECelular.Proveedor);
                        if (listaCarrito.Exists(x => x.Codigo == oBECelular.Codigo) == false)
                        {
                            if (oBECelular.Stock - cantidad < 0)
                            {
                                throw new Exception("No hay stock disponible del producto");
                            }
                            oBECelular.Stock -= cantidad;
                            listaCarrito.Add(objBEProdAdquirido);
                        }
                        else
                        {
                            if (oBECelular.Stock - cantidad < 0)
                            {
                                throw new Exception("No hay stock disponible del producto");
                            }
                            oBECelular.Stock -= cantidad;
                            listaCarrito.Find(x => x.Codigo == objBEProdAdquirido.Codigo).Stock += cantidad;
                            cargarGrillaCarrito();
                        }
                        this.numericUpDown1.Value = 0;
                        oBLLCelular.Guardar(oBECelular);
                        cargarGrillaProdCelular();
                        cargarGrillaCarrito();
                    }
                    else
                    {
                        MessageBox.Show("Ingrese una cantidad del producto");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        void cargarGrillaCarrito()
        {
            this.dataGridCarrito.DataSource = null;
            this.dataGridCarrito.DataSource = listaCarrito;

            // se calcula un total estimado del total del carrito (sin descuentos)
            labelTotal.Text = listaCarrito.Sum(x => x.Precio * x.Stock).ToString();
            this.dataGridCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }



        private void FormVenta_Load(object sender, EventArgs e)
        {
            this.dataGridViewClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }


        void cargarGrillaClientes()
        {
            this.dataGridViewClientes.DataSource = null;
            this.dataGridViewClientes.DataSource = oBLLCliente.ListarTodo();
            this.dataGridViewClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }


        private void comboBoxSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSeccion.Text == "Computadora")
            {
                cargarGrillaProdComputadora();
            }
            if (comboBoxSeccion.Text == "Celular")
            {
                cargarGrillaProdCelular();
            }
        }


        void cargarGrillaProdComputadora()
        {
            this.dataGridViewProductos.DataSource = null;
            this.dataGridViewProductos.DataSource = oBLLComputadora.ListarTodo();
            this.dataGridViewProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }


        void cargarGrillaProdCelular()
        {
            this.dataGridViewProductos.DataSource = null;
            this.dataGridViewProductos.DataSource = oBLLCelular.ListarTodo();
            this.dataGridViewProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                BEProducto oBEProducto = (BEProducto)this.dataGridCarrito.SelectedRows[0].DataBoundItem;
                if (listaCarrito.Count > 0)
                {
                    /*
                     * Se elimina un producto del carrito y se vuelve a renovar el stock como se encontraba anteriormente
                     */
                    listaCarrito.RemoveAll(x => x.Codigo == oBEProducto.Codigo);
                    if (comboBoxSeccion.Text == "Computadora")
                    {
                        oBEComputadora = oBLLComputadora.ListarTodo().Find(x => x.Codigo == oBEProducto.Codigo);
                        oBEComputadora.Stock += oBEProducto.Stock;
                        oBLLComputadora.Guardar(oBEComputadora);
                        cargarGrillaProdComputadora();
                        cargarGrillaCarrito();
                    }
                    else
                    {
                        oBECelular = oBLLCelular.ListarTodo().Find(x => x.Codigo == oBEProducto.Codigo);
                        oBECelular.Stock += oBEProducto.Stock;
                        oBLLCelular.Guardar(oBECelular);
                        cargarGrillaProdCelular();
                        cargarGrillaCarrito();
                    }
                }
            }
            catch (Exception) { }
        }

        private void btnAceptarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                if (listaCarrito.Count > 0 && dataGridViewClientes.SelectedRows.Count > 0)
                {
                    string Msg = string.Empty;
                    oBEVenta.Cliente = (BECliente)this.dataGridViewClientes.SelectedRows[0].DataBoundItem;
                    oBEVenta.Empleado = oBEEmpleado;
                    oBEVenta.Fecha = DateTime.Now;
                    oBEVenta.Productos = listaCarrito;
                    oBEVenta.Descuento = 0;

                    // si los productos de computación tienen un total mayor a 60000 hay 15% de descuento
                    double totalComputadora = listaCarrito.FindAll(x => x.Tipo == "Computadora").Sum(y => y.Precio * y.Stock);
                    if (totalComputadora > 60000)
                    {
                        double descuentoComputadora = oBLLComputadora.obtenerDescuento(totalComputadora);
                        oBEVenta.Descuento += descuentoComputadora;
                        Msg = "Descuentos aplicados: " + Environment.NewLine + "    - Descuento por productos de computadora -$" + descuentoComputadora + Environment.NewLine;
                    }
                    // si los productos de computación tienen un total mayor a 100000 hay 25% de descuento
                    double totalCelular = listaCarrito.FindAll(x => x.Tipo == "Celular").Sum(y => y.Precio * y.Stock);
                    if (totalCelular > 95000)
                    {
                        double descuentoCelular = oBLLCelular.obtenerDescuento(totalCelular);
                        oBEVenta.Descuento += descuentoCelular;
                        Msg += "    - Descuento por productos de celular -$" + descuentoCelular + Environment.NewLine + Environment.NewLine;
                    }
                    // se calcula el total con o sin descuentos
                    oBEVenta.Total = listaCarrito.Sum(x => x.Precio * x.Stock) - oBEVenta.Descuento;
                    Msg += Environment.NewLine + "TOTAL: $" + oBEVenta.Total;
                    MessageBox.Show(Msg);
                    DialogResult dialog = MessageBox.Show($"¿Desea registrar la venta al cliente {oBEVenta.Cliente.ToString()}?", "Información", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        if (oBLLVenta.Guardar(oBEVenta) == true)
                        {
                            oBEVenta.Codigo = oBLLVenta.ListarTodo().First().Codigo;
                            if (oBLLVenta.RegistrarDetalle_Venta(oBEVenta) == true)
                            {
                                MessageBox.Show("Venta registrada con exito");
                                listaCarrito.Clear();
                                this.dataGridViewProductos.Refresh();
                                this.dataGridCarrito.DataSource = null;
                                this.labelTotal.Text = "$___";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error al registrar la venta");
                        }
                    }


                }
            }
            catch (Exception) { }
        }
    }
}
