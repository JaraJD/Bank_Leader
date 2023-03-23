# Bank Leader

Implementación de dapper con Clean Architecture

# Diagrama Entidad Relacion (DER)

![DER banco](https://user-images.githubusercontent.com/93845990/227055715-59b7e8e9-7992-400c-92ce-00655b5a31cc.png)

# Script SQL

```SQL

CREATE DATABASE Bank_Leader;
GO

-- Seleccionar la base de datos
USE Bank_Leader;
GO


CREATE TABLE Sucursal (
  sucursal_id INT PRIMARY KEY IDENTITY(1,1),
  nombre VARCHAR(50),
  direccion VARCHAR(100)
);
GO

CREATE TABLE Empleado (
  empleado_id INT PRIMARY KEY IDENTITY(1,1),
  sucursal_id INT,
  nombre VARCHAR(50),
  apellido VARCHAR(50),
  direccion VARCHAR(100),
  correo VARCHAR(50),
  tarjeta_acceso VARCHAR(50),
  estado VARCHAR(20),
  FOREIGN KEY (sucursal_id) REFERENCES Sucursal(sucursal_id)
);
GO

CREATE TABLE Cliente (
  cliente_id INT PRIMARY KEY IDENTITY(1,1),
  nombre VARCHAR(50),
  apellido VARCHAR(50),
  fecha_nacimiento DATE,
  direccion VARCHAR(100),
  telefono VARCHAR(20),
  correo VARCHAR(50),
  ocupacion VARCHAR(50),
  genero VARCHAR(10)
);
GO

CREATE TABLE Cuenta (
  cuenta_id INT PRIMARY KEY IDENTITY(1,1),
  cliente_id INT,
  tipo_cuenta VARCHAR(50),
  saldo DECIMAL(12,2),
  fecha_apertura DATE,
  fecha_cierre DATE,
  tasa_interes DECIMAL(5,2),
  estado VARCHAR(20),
  FOREIGN KEY (cliente_id) REFERENCES Cliente(cliente_id)
);
GO

CREATE TABLE Tarjeta (
  tarjeta_id INT PRIMARY KEY IDENTITY(1,1),
  cliente_id INT,
  tipo_tarjeta VARCHAR(50),
  fecha_emision DATE,
  fecha_vencimiento DATE,
  limite_credito DECIMAL(12,2),
  estado VARCHAR(20),
  FOREIGN KEY (cliente_id) REFERENCES Cliente(cliente_id)
);
GO

CREATE TABLE Producto (
  producto_id INT PRIMARY KEY IDENTITY(1,1),
  tipo_producto VARCHAR(50),
  descripcion VARCHAR(100),
  plazo INT,
  monto DECIMAL(12,2),
  tasa_interes DECIMAL(5,2),
  estado VARCHAR(20)
);
GO

CREATE TABLE Transaccion (
  transaccion_id INT PRIMARY KEY IDENTITY(1,1),
  cuenta_id INT,
  tarjeta_id INT,
  producto_id INT,
  fecha DATE,
  hora TIME,
  tipo_transaccion VARCHAR(50),
  descripcion VARCHAR(100),
  monto DECIMAL(12,2),
  saldo_final DECIMAL(12,2),
  saldo_anterior DECIMAL(12,2),
  FOREIGN KEY (cuenta_id) REFERENCES Cuenta(cuenta_id),
  FOREIGN KEY (tarjeta_id) REFERENCES Tarjeta(tarjeta_id),
  FOREIGN KEY (producto_id) REFERENCES Producto(producto_id)
);
GO

INSERT INTO Sucursal (nombre, direccion) VALUES 
  ('Sucursal A', 'Av. 1, Calle 1'),
  ('Sucursal B', 'Av. 2, Calle 2');

INSERT INTO Empleado (sucursal_id, nombre, apellido, direccion, correo, tarjeta_acceso, estado) VALUES 
  (1, 'Juan', 'Perez', 'Av. 1, Calle 1', 'jperez@example.com', '123456', 'Activo'),
  (2, 'Ana', 'Gonzalez', 'Av. 2, Calle 2', 'agonzalez@example.com', '654321', 'Activo');

INSERT INTO Cliente (nombre, apellido, fecha_nacimiento, direccion, telefono, correo, ocupacion, genero) VALUES 
  ('Maria', 'Lopez', '1990-05-12', 'Av. 3, Calle 3', '555-5555', 'mlopez@example.com', 'Estudiante', 'Femenino'),
  ('Pedro', 'Garcia', '1985-02-01', 'Av. 4, Calle 4', '555-1234', 'pgarcia@example.com', 'Abogado', 'Masculino');

INSERT INTO Cuenta (cliente_id, tipo_cuenta, saldo, fecha_apertura, fecha_cierre, tasa_interes, estado) VALUES 
  (1, 'Cuenta de Ahorro', 1500.00, '2022-01-01', NULL, 0.05, 'Activo'),
  (2, 'Cuenta Corriente', 5000.00, '2021-06-01', NULL, 0.03, 'Activo');

INSERT INTO Tarjeta (cliente_id, tipo_tarjeta, fecha_emision, fecha_vencimiento, limite_credito, estado) VALUES 
  (1, 'Tarjeta de Débito', '2022-01-01', '2025-01-01', 2000.00, 'Activo'),
  (2, 'Tarjeta de Crédito', '2021-06-01', '2024-06-01', 10000.00, 'Activo');

INSERT INTO Producto (tipo_producto, descripcion, plazo, monto, tasa_interes, estado) VALUES 
  ('Préstamo Personal', 'Préstamo para gastos personales', 24, 5000.00, 0.1, 'Activo'),
  ('Hipoteca', 'Crédito para la compra de vivienda', 360, 100000.00, 0.05, 'Activo');

INSERT INTO Transaccion (cuenta_id, tarjeta_id, producto_id, fecha, hora, tipo_transaccion, descripcion, monto, saldo_final, saldo_anterior) VALUES 
  (1, NULL, NULL, '2022-02-01', '10:00:00', 'Depósito', 'Depósito en efectivo', 1000.00, 2500.00, 1500.00),
  (2, NULL, NULL, '2021-07-01', '15:30:00', 'Transferencia', 'Transferencia a cuenta de terceros', 2000.00, 3000.00, 5000.00);

```
