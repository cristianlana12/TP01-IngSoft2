using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaProductos;

namespace TiendaProductos
{
    public class Tienda
    {
        public List<Producto> inventario;

        public Tienda()
        {
            inventario = new List<Producto>();
        }

        public void AgregarProducto(Producto producto)
        {
            inventario.Add(producto);
        }

        // Método para buscar un producto por nombre
        public Producto BuscarProducto(string nombre)
        {
            foreach (Producto producto in inventario)
            {
                if (producto.Nombre == nombre)
                {
                    return producto;
                }
            }
            throw new Exception($"Producto '{nombre}' no existe en el inventario.");
        }

        // Método para eliminar un producto por nombre
        public bool EliminarProducto(string nombre)
        {
            Producto productoAEliminar = BuscarProducto(nombre);
            if (productoAEliminar != null)
            {
                inventario.Remove(productoAEliminar);
                return true;
            }
            throw new Exception($"Producto '{nombre}' no existe en el inventario.");
        }

        // Metodo para aplicar descuento a un producto
        public void AplicarDescuento(string nombre, decimal porcentaje)
        {
            var producto = BuscarProducto(nombre);
            var nuevoPrecio = producto.Precio * (1 - porcentaje / 100);
            producto.ActualizarPrecio(nuevoPrecio); // Llama a ActualizarPrecio en Producto
        }

        public decimal CalcularTotalCarrito(List<string> nombresDeProductos)
        {
            decimal total = 0;

            foreach (var nombre in nombresDeProductos)
            {
                var producto = BuscarProducto(nombre);
                total += producto.Precio;
            }

            return total;
        }
    }
}
