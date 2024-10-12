-- Active: 1727132803198@@127.0.0.1@3306@tienda_online_cliente
CREATE TABLE IF NOT EXISTS Cliente (
    IdCliente INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100),
    Apellido VARCHAR(100),
    Email VARCHAR(100) UNIQUE,
    Usuario VARCHAR(50),
    Contrasenia VARCHAR(255)
);
CREATE TABLE IF NOT EXISTS CategoriaProducto (
    IdCategoria INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100),
    Descripcion TEXT
);
CREATE TABLE IF NOT EXISTS Producto (
    IdProducto INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100),
    Descripcion TEXT,
    PrecioUnitario DECIMAL(10, 2),
    Stock INT,
    IdCategoria INT,
    FOREIGN KEY (IdCategoria) REFERENCES CategoriaProducto(IdCategoria)
);
CREATE TABLE IF NOT EXISTS Carrito (
    IdCarrito INT AUTO_INCREMENT PRIMARY KEY,
    NroCarrito INT,
    Total DECIMAL(10, 2),
    IdCliente INT,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente)
);
CREATE TABLE IF NOT EXISTS ItemCarrito (
    IdItemCarrito INT AUTO_INCREMENT PRIMARY KEY,
    Subtotal DECIMAL(10, 2),
    Cantidad INT,
    IdProducto INT,
    IdCarrito INT,
    FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto),
    FOREIGN KEY (IdCarrito) REFERENCES Carrito(IdCarrito)
);
