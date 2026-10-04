using Xunit;
using Moq;

namespace TiendaProductos.Tests
{
    public class TiendaTests : IClassFixture<TiendaFixture>
    {
        // [Fact]
        // public void AgregarProducto_ProductoDebeSerAgregadoAlInventario()
        // {
        //     // Arrange
        //     var tienda = new Tienda();
        //     var producto = new Producto("Laptop", 1000.00m, "Electrónica");

        //     // Act
        //     tienda.AgregarProducto(producto);

        //     // Assert
        //     Assert.Contains(producto, tienda.Inventario);  // Verifica que el producto esté en el inventario
        // }

        // [Fact]
        // public void BuscarProducto_ProductoExistenteDebeSerEncontrado()
        // {
        //     // Arrange
        //     var tienda = new Tienda();
        //     var producto = new Producto("Laptop", 1000.00m, "Electrónica");
        //     tienda.AgregarProducto(producto);

        //     // Act
        //     var resultado = tienda.BuscarProducto("Laptop");

        //     // Assert
        //     Assert.NotNull(resultado);  // Verifica que se encontró el producto
        //     Assert.Equal("Laptop", resultado.Nombre);
        // }

        // [Fact]
        // public void BuscarProducto_ProductoNoExistenteDebeRetornarNull()
        // {
        //     // Arrange
        //     var tienda = new Tienda();

        //     // Act
        //     var resultado = tienda.BuscarProducto("Celular");

        //     // Assert
        //     Assert.Null(resultado);  // Verifica que el producto no fue encontrado
        // }

        [Fact]
        public void EliminarProducto_ProductoExistenteDebeSerEliminado()
        {
            // Arrange
            var tienda = new Tienda();
            var producto = new Producto("Laptop", 1000.00m, "Electrónica");
            tienda.AgregarProducto(producto);

            // Act
            var resultado = tienda.EliminarProducto("Laptop");

            // Assert
            Assert.True(resultado);  // Verifica que el producto fue eliminado
            var exception = Assert.Throws<Exception>(() => tienda.BuscarProducto("Laptop"));
            Assert.Equal("Producto 'Laptop' no existe en el inventario.", exception.Message);  // Verifica que el producto ya no está en el inventario
        }

        // [Fact]
        // public void EliminarProducto_ProductoNoExistenteDebeRetornarFalse()
        // {
        //     // Arrange
        //     var tienda = new Tienda();

        //     // Act
        //     var resultado = tienda.EliminarProducto("Celular");

        //     // Assert
        //     Assert.False(resultado);  // Verifica que no se eliminó nada porque el producto no existía
        // }

        [Fact]
        public void BuscarProducto_ProductoNoExistenteDebeLanzarExcepcion()
        {
            // Arrange
            var tienda = new Tienda();

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => tienda.BuscarProducto("Celular"));
            Assert.Equal("Producto 'Celular' no existe en el inventario.", exception.Message);
        }

        [Fact]
        public void EliminarProducto_ProductoNoExistenteDebeLanzarExcepcion()
        {
            // Arrange
            var tienda = new Tienda();

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => tienda.EliminarProducto("Celular"));
            Assert.Equal("Producto 'Celular' no existe en el inventario.", exception.Message);
        }

        [Fact]
        public void ActualizarPrecio_PrecioNegativoDebeLanzarExcepcion()
        {
            // Arrange
            var producto = new Producto("Laptop", 1000.00m, "Electrónica");

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => producto.ActualizarPrecio(-500.00m));
            Assert.Equal("El precio no puede ser negativo.", exception.Message);
        }


        // Uso de Mocks
        [Fact]
        public void AplicarDescuento_DebeLlamarActualizarPrecio()
        {
            // Arrange
            var tienda = new Tienda();

            // Crear un mock de Producto
            var mockProducto = new Mock<Producto>("Laptop", 1000.00m, "Electrónica");

            // Simula el comportamiento del método ActualizarPrecio
            mockProducto.Setup(p => p.ActualizarPrecio(It.IsAny<decimal>()));

            // Añadir el mock del producto a la tienda
            tienda.AgregarProducto(mockProducto.Object);

            // Act
            tienda.AplicarDescuento("Laptop", 10);  // Aplica un 10% de descuento

            // Assert
            // Verificar que el método ActualizarPrecio fue llamado con el nuevo precio correcto
            mockProducto.Verify(p => p.ActualizarPrecio(900.00m), Times.Once);
        }

        private readonly Tienda _tienda;

        public TiendaTests(TiendaFixture fixture)
        {
            _tienda = fixture.Tienda;  // Reutiliza la tienda inicializada por el fixture
        }

        [Fact]
        public void BuscarProducto_ProductoExistenteDebeSerEncontrado()
        {
            // Act
            var producto = _tienda.BuscarProducto("Laptop");

            // Assert
            Assert.NotNull(producto);
            Assert.Equal("Laptop", producto.Nombre);
        }

        [Fact]
        public void AgregarProducto_ProductoDebeSerAgregadoAlInventario()
        {
            // Act
            var nuevoProducto = new Producto("Tablet", 300.00m, "Electrónica");
            _tienda.AgregarProducto(nuevoProducto);

            // Assert
            Assert.Contains(nuevoProducto, _tienda.inventario);
        }

        // Prueba de integracion
        [Fact]
        public void CalcularTotalCarrito_ConDescuentosDebeDarTotalCorrecto()
        {
            // Arrange: Inicializa el carrito con algunos productos
            var carrito = new List<string> { "Laptop", "Celular" };

            // Aplica un 10% de descuento a la Laptop
            _tienda.AplicarDescuento("Laptop", 10);  // El precio de la Laptop debe ser 900.00

            // Act: Calcula el total del carrito
            var total = _tienda.CalcularTotalCarrito(carrito);

            // Assert: Verifica que el total sea correcto
            // Laptop (900.00) + Celular (500.00) = 1400.00
            Assert.Equal(1400.00m, total);
        }

        
    }
}