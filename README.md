# Bank Leader

Implementación de dapper con Clean Architecture

# Diagrama Entidad Relacion (DER)

![DER Bank_Leader](https://user-images.githubusercontent.com/93845990/227270651-b4d18b4d-64a6-4c78-830a-837138857b20.png)


# Script SQL

```SQL

CREATE DATABASE Bank_Leader;
GO

-- Seleccionar la base de datos
USE Bank_Leader;
GO

CREATE TABLE Cliente (
  cliente_id INT PRIMARY KEY IDENTITY(1,1),
  nombre VARCHAR(50),
  apellido VARCHAR(50),
  fecha_nacimiento DATE,
  telefono VARCHAR(20),
  correo VARCHAR(50),
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
  cliente_id INT,
  tipo_producto VARCHAR(50),
  descripcion VARCHAR(100),
  plazo INT,
  monto DECIMAL(12,2),
  tasa_interes DECIMAL(5,2),
  estado VARCHAR(20),
  FOREIGN KEY (cliente_id) REFERENCES Cliente(cliente_id)
);
GO

CREATE TABLE Transaccion (
  transaccion_id INT PRIMARY KEY IDENTITY(1,1),
  cuenta_id INT,
  tarjeta_id INT,
  producto_id INT,
  fecha DATE,
  tipo_transaccion VARCHAR(50),
  descripcion VARCHAR(100),
  monto DECIMAL(12,2),
  FOREIGN KEY (cuenta_id) REFERENCES Cuenta(cuenta_id),
  FOREIGN KEY (tarjeta_id) REFERENCES Tarjeta(tarjeta_id),
  FOREIGN KEY (producto_id) REFERENCES Producto(producto_id)
);
GO

INSERT INTO Cliente (cliente_id, nombre, apellido, fecha_nacimiento, telefono, correo, genero)
VALUES (1, 'Juan', 'Perez', '1990-05-12', '555-1234', 'juan.perez@mail.com', 'M'),
       (2, 'Maria', 'Garcia', '1985-11-23', '555-5678', 'maria.garcia@mail.com', 'F'),
       (3, 'Pedro', 'Rodriguez', '1995-02-15', '555-2468', 'pedro.rodriguez@mail.com', 'M'),
       (4, 'Ana', 'Lopez', '1980-07-30', '555-3698', 'ana.lopez@mail.com', 'F');

INSERT INTO Cuenta (cuenta_id, cliente_id, tipo_cuenta, saldo, fecha_apertura, fecha_cierre, tasa_interes, estado)
VALUES (1, 1, 'Corriente', 10000.00, '2022-01-01', NULL, 2.50, 'Activa'),
       (2, 1, 'Ahorros', 5000.00, '2021-12-01', NULL, 1.50, 'Activa'),
       (3, 2, 'Corriente', 15000.00, '2021-11-15', NULL, 2.50, 'Activa'),
       (4, 3, 'Ahorros', 1000.00, '2022-02-10', NULL, 1.75, 'Activa');

INSERT INTO Tarjeta (tarjeta_id, cliente_id, tipo_tarjeta, fecha_emision, fecha_vencimiento, limite_credito, estado)
VALUES (1, 1, 'Credito', '2021-12-01', '2022-12-01', 5000.00, 'Activa'),
       (2, 2, 'Debito', '2022-01-15', '2023-01-15', 2000.00, 'Activa'),
       (3, 3, 'Credito', '2021-10-01', '2022-10-01', 10000.00, 'Activa');

INSERT INTO Producto (producto_id, cliente_id, tipo_producto, descripcion, plazo, monto, tasa_interes, estado)
VALUES (1, 2, 'Prestamo', 'Prestamo personal', 12, 20000.00, 3.75, 'Activo'),
       (2, 4, 'Prestamo', 'Prestamo de auto', 36, 50000.00, 4.50, 'Activo');

```
