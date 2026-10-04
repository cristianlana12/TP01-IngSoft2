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
    }
}