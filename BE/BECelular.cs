using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BECelular : BEProducto
    {
        public BECelular() { }

        public BECelular(int codigo, string nombre, string marca, string descripcion, string tipo, double precio, int stock, BEProveedor proveedor) : base(codigo, nombre, marca, descripcion, tipo, precio, stock, proveedor)
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
