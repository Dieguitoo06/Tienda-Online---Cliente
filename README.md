# Tienda Online - Plataforma de Ventas Gratuita

## Introducción

La Tienda Online es una plataforma de comercio electrónico diseñada para permitir a vendedores y clientes realizar ventas y compras sin comisiones. Este proyecto está orientado a ofrecer una solución accesible para pequeñas empresas y emprendedores, permitiendo gestionar clientes, productos y carritos de compra de manera sencilla.

## Funcionalidades

- **Gestión de Clientes**: Registro, autenticación, y almacenamiento de información personal como nombre, apellido, email, nombre de usuario y contraseña.
- **Búsqueda y Filtro de Productos**: Búsqueda por nombre, descripción o categoría y filtrado por rango de precios.
- **Carrito de Compras**: Adición de productos, especificación de cantidades, cálculo de subtotales y total de la compra.
- **Proceso de Compra**: Confirmación de compra que ajusta el inventario y posibilidad de cancelar el carrito.

## Tecnologías Utilizadas

- **ASP.NET Core**: Framework para la construcción de la API web.
- **Entity Framework Core**: ORM para manejar la persistencia en la base de datos.
- **MySQL**: Base de datos relacional para almacenar clientes, productos y órdenes.
- **C#**: Lenguaje de programación para el desarrollo del backend.
- **Arquitectura Vertical Slice**: Estructuración modular del código en función de cada funcionalidad específica.

## Requisitos Previos

1. **.NET SDK** (versión 6.0 o superior) - Para desarrollar y ejecutar el proyecto.
2. **MySQL** - Base de datos donde se almacenará la información.
3. **Herramienta de gestión de base de datos MySQL** (como MySQL Workbench) - Para facilitar la creación y gestión de la base de datos.

## Configuración del Entorno

### Clonar el Repositorio

```bash
git clone https://github.com/tu_usuario/tu_proyecto_tienda_online.git
cd tu_proyecto_tienda_online
