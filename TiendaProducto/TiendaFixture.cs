using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProductos
{
    public class TiendaFixture : IDisposable
    {
        public Tienda Tienda { get; private set; }

        public TiendaFixture()
        {
            // Inicializa la tienda con algunos productos predefinidos
            Tienda = new Tienda();
            Tienda.AgregarProducto(new Producto("Laptop", 1000.00m, "Electrónica"));
            Tienda.AgregarProducto(new Producto("Celular", 500.00m, "Electrónica"));
            Tienda.AgregarProducto(new Producto("Tablet", 600.00m, "Electrónica"));
        }

        // Usamos Cleanup si es necesario
        public void Dispose()
        {
            // Aquí podrías limpiar recursos si fuera necesario.
            
        }
    }
}