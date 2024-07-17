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
using System.Text.RegularExpressions;

namespace Presentacion_IU
{
    public partial class FormCliente : Form
    {
        public FormCliente()
        {
            InitializeComponent();
            oBECliente = new BECliente();
            oBLLCliente = new BLLCliente();

        }

        BECliente oBECliente;
        BLLCliente oBLLCliente;

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        void cargarGrillaClientes()
        {
            this.dataGridViewCliente.DataSource = null;
            this.dataGridViewCliente.DataSource = oBLLCliente.ListarTodo();
            this.dataGridViewCliente.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
           
        }

        void limpiarDatos()
        {
            this.txtBoxNombre.Text = string.Empty;
            this.txtBoxApellido.Text = string.Empty;
            this.txtBoxDNI.Text = string.Empty;
            this.txtBoxTelefono.Text = string.Empty;
            this.txtBoxEmail.Text = string.Empty;
            this.labelCod.Text = "0";
        }

        private void FormCliente_Load(object sender, EventArgs e)
        {
            cargarGrillaClientes();
            this.reportViewer1.RefreshReport();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                oBECliente = (BECliente)this.dataGridViewCliente.CurrentRow.DataBoundItem;
                this.txtBoxNombre.Text = oBECliente.Nombre;
                this.txtBoxApellido.Text = oBECliente.Apellido;
                this.txtBoxDNI.Text = oBECliente.DNI.ToString();
                this.txtBoxTelefono.Text = oBECliente.Telefono;
                this.txtBoxEmail.Text = oBECliente.Email;
                this.labelCod.Text = oBECliente.Codigo.ToString();
            }
            catch (Exception) { }
        }

        private void btnGuardad_Click(object sender, EventArgs e)
        {
            try
            {
                // guarda los datos del cliente
                oBECliente.Codigo = Convert.ToInt32(labelCodigo.Text);
                oBECliente.Nombre = textBoxNombre.Text;
                oBECliente.Apellido = textBoxApellido.Text;
                oBECliente.Telefono = textBoxTelefono.Text;

                // se verifica mediante expresión regular si cumple ciertos formatos como DNI o Email
                if (Regex.IsMatch(textBoxDNI.Text, "^([0-9]{8})$") == true)
                {
                    if (oBLLCliente.ListarTodo().Exists(x => x.DNI == textBoxDNI.Text) == false)
                    {
                        oBECliente.DNI = textBoxDNI.Text;
                    }
                    else
                    {
                        throw new Exception("El DNI ingresado ya existe");
                    }
                }
                else { throw new Exception("El formato del DNI ingresado es incorrecto"); }
                if (Regex.IsMatch(textBoxEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") == true)
                {
                    if (oBLLCliente.ListarTodo().Exists(x => x.Email.Trim() == textBoxEmail.Text.Trim()) == false)
                    {
                        oBECliente.Email = textBoxEmail.Text;
                    }
                    else
                    {
                        throw new Exception("El email ingresado ya existe");
                    }
                }
                else { throw new Exception("El email ingresado es incorrecto"); }
                oBLLCliente.Guardar(oBECliente);
                cargarGrillaClientes();
                limpiarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                oBECliente = (BECliente)this.dataGridViewCliente.CurrentRow.DataBoundItem;
                DialogResult Dialogo = MessageBox.Show("¿Seguro desea eliminar el cliente seleccionado?", "Eliminación", MessageBoxButtons.YesNo);
                if (Dialogo == DialogResult.Yes)
                {
                    if (oBLLCliente.Borrar(oBECliente) == true)
                    {
                        MessageBox.Show("Cliente eliminado!");
                        cargarGrillaClientes();
                    }
                    else
                    {
                        MessageBox.Show("El cliente no puede ser eliminado ya que tiene ventas realizadas");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
