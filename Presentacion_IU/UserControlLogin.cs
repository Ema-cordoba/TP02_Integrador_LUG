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
using Security;

namespace Presentacion_IU
{
    public partial class UserControlLogin : UserControl
    {
        public UserControlLogin()
        {
            InitializeComponent();
            oBLLUsuario = new BLLUsuario();

        }


        BLLUsuario oBLLUsuario;

        public static BEUsuario oBEUsuario;

        private void btnImgresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxUsuario.Text == string.Empty)
                {
                    MessageBox.Show("Ingrese el usuario!");
                }
                if (textBoxContraseña.Text == string.Empty)
                {
                    MessageBox.Show("Ingrese una contraseña!");
                }
                if (textBoxUsuario.Text != string.Empty && textBoxContraseña.Text != string.Empty)
                {
                    oBEUsuario = oBLLUsuario.VerificarUsuario(textBoxUsuario.Text, ClsSecurity.Encriptar(textBoxContraseña.Text));
                    if (oBEUsuario != null)
                    {
                        MessageBox.Show($"Se ha loggeado correctamente!");
                        this.FindForm().Hide();
                        FormInicio formInicial = new FormInicio();
           
                        formInicial.Show();
                    }
                    else
                    {
                        MessageBox.Show("Usuario y/o contraseña incorrectos!");
                        this.textBoxContraseña.Text = string.Empty;
                    }
                }
            }
            catch (Exception) { }
        }
    }
}
