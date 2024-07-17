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
using System.Text.RegularExpressions;

namespace Presentacion_IU
{
    public partial class FormEmpleados : Form
    {
        public FormEmpleados()
        {
            InitializeComponent();
            oBEEmpleado = new BEEmpleado();
            oBEUsuario = new BEUsuario();
            oBLLEmpleado = new BLLEmpleado();
            oBLLUsuario = new BLLUsuario();
        }

        BLLEmpleado oBLLEmpleado;
        BEEmpleado oBEEmpleado;
        BLLUsuario oBLLUsuario;
        BEUsuario oBEUsuario;

        private void btnGuardad_Click(object sender, EventArgs e)
        {
            try
            {
                if (asignarDatosEmpleado() == true)
                {
                    if (oBLLEmpleado.Guardar(oBEEmpleado) == true)
                    {
                        MessageBox.Show("Empleado cargado!");
                        cargarGrillaEmpleados();
                        limpiarTxt();
                    }
                    else
                    {
                        throw new Exception("No se pudo cargar el emplado");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        bool asignarDatosEmpleado()
        {
            try
            {
                oBEEmpleado.Nombre = this.textBoxNombre.Text;
                oBEEmpleado.Apellido = this.textBoxApellido.Text;

                // se verifica el fromato de el DNI ingresado, caso contrario se pide volver a ingresar uno correcto
                if (Regex.IsMatch(textBoxDNI.Text, "^([0-9]{8})$") == true)
                {
                    if (oBLLEmpleado.ListarTodo().Exists(x => x.Dni == textBoxDNI.Text) == false)
                    {
                        oBEEmpleado.Dni = this.textBoxDNI.Text;
                    }
                    else { throw new Exception("ya existe un empleado con el DNI ingresado"); }
                }
                else
                {
                    throw new Exception("El DNI no cumple el formato definido");
                }
                oBEEmpleado.FechaIngreso = DateTime.Now;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        void limpiarTxt()
        {
            oBEEmpleado.Codigo = 0;
            this.textBoxNombre.Text = string.Empty;
            this.textBoxApellido.Text = string.Empty;
            this.textBoxDNI.Text = string.Empty;
        }

        private void FormEmpleados_Load(object sender, EventArgs e)
        {
            cargarGrillaEmpleados();
        }

        void cargarGrillaEmpleados()
        {
            this.dataGridViewEmpleados.DataSource = null;
            this.dataGridViewEmpleados.DataSource = oBLLEmpleado.ListarTodo();
            this.dataGridViewEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }


        private void dataGridViewEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                oBEEmpleado = (BEEmpleado)this.dataGridViewEmpleados.SelectedRows[0].DataBoundItem;
                this.textBoxNombre.Text = oBEEmpleado.Nombre;
                this.textBoxApellido.Text = oBEEmpleado.Apellido;
                this.textBoxDNI.Text = oBEEmpleado.Dni;
            }
            catch (Exception) { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Se borrarán todos los datos del empleado seleccionado ¿desea continuar?", "ALERTA", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                oBEEmpleado.Nombre = this.textBoxNombre.Text;
                oBEEmpleado.Apellido = this.textBoxApellido.Text;
                oBEEmpleado.Dni = this.textBoxDNI.Text;
                oBEUsuario.Empleado = oBEEmpleado;

                /* 
                 * antes de borrar un empleado veo si tiene ventas,
                 * si no tiene borro primero su usuario y luego el empleado para evitar errores
                */
                if (oBLLEmpleado.obtenerTotalVentas(oBEEmpleado) == false)
                {
                    oBLLUsuario.Borrar(oBEUsuario);
                    if (oBLLEmpleado.Borrar(oBEEmpleado) == true)
                    {

                        MessageBox.Show("Los datos del empleado se han eliminado");
                        cargarGrillaEmpleados();
                        limpiarTxt();
                    }
                }
                else
                {
                    MessageBox.Show("Error al eliminar el empleado, verifique que el mismo no tenga ventas");
                }

            }
        }
    }
}
