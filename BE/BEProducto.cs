using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEProducto : Entidad
    {
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
        public BEProveedor Proveedor { get; set; }

        public BEProducto() { }
        public BEProducto(int codigo, string nombre, string marca, string descripcion, string tipo, double precio, int stock, BEProveedor proveedor)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Marca = marca;
            this.Descripcion = descripcion;
            this.Tipo = tipo;
            this.Precio = precio;
            this.Stock = stock;
            this.Proveedor = proveedor;
        }
    }
}
