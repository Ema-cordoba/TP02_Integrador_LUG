namespace Presentacion_IU
{
    partial class FormLogin
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.userControlLogin1 = new Presentacion_IU.UserControlLogin();
            this.SuspendLayout();
            // 
            // userControlLogin1
            // 
            this.userControlLogin1.BackColor = System.Drawing.Color.Blue;
            this.userControlLogin1.Location = new System.Drawing.Point(-4, 1);
            this.userControlLogin1.Name = "userControlLogin1";
            this.userControlLogin1.Size = new System.Drawing.Size(211, 243);
            this.userControlLogin1.TabIndex = 0;
            this.userControlLogin1.Load += new System.EventHandler(this.userControlLogin1_Load);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(207, 238);
            this.Controls.Add(this.userControlLogin1);
            this.Name = "FormLogin";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControlLogin userControlLogin1;
    }
}

