# AngularCRUDTareaClient

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 11.1.2.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Instrucciones de Ambiente
1- Crear base de datos local en MSSQL Server, de nombre => "AngularCRUD" <br>
2.- Ejecutar procedimientos almacenados <br>
3.- Descargar dependencias de angular y ejecutar el siguiente comando en ./Angular.CRUD.Tarea.Client/ <br>

```
npm install
npm run start
```

4.- Levantar api en localhost:5001

## Procedimientos Almacenados

## Crear tablas y seedear datos iniciales

```
CREATE TABLE Categoria(
    id_categoria_articulo int NOT NULL  IDENTITY(1,1),
    glosa_categoria_articulo varchar(200) NOT NULL,
    vigente_categoria_articulo bit NOT NULL,
    id_cliente int,
    usuario_creador int,
    usuario_modificador int,
    fecha_modificacion datetime,
    fecha_creacion datetime	,
	Primary Key(id_categoria_articulo)
	);


CREATE TABLE Articulo(
    id_articulo bigint NOT NULL IDENTITY(1,1),
	codigo_articulo varchar(500),
	marca_articulo varchar(100),
	modelo_articulo varchar(100),
	descripcion_articulo varchar(2000),
	id_tipo_insumo int,
    id_categoria_articulo int,
	Primary Key(id_articulo),
	CONSTRAINT FK_categoria_articulo FOREIGN KEY (id_categoria_articulo)
    REFERENCES Categoria(id_categoria_articulo)
	);


INSERT INTO Categoria(glosa_categoria_articulo, vigente_categoria_articulo, id_cliente, usuario_creador,  usuario_modificador, fecha_modificacion, fecha_creacion)
VALUES ('Electrodomesticos', 1, 1, 1, 1, GETDATE(), GETDATE());

INSERT INTO Categoria(glosa_categoria_articulo, vigente_categoria_articulo, id_cliente, usuario_creador,  usuario_modificador, fecha_modificacion, fecha_creacion)
VALUES ('Computación', 1, 1, 1, 1, GETDATE(), GETDATE());

INSERT INTO Categoria(glosa_categoria_articulo, vigente_categoria_articulo, id_cliente, usuario_creador,  usuario_modificador, fecha_modificacion, fecha_creacion)
VALUES ('Vestuario', 1, 1, 1, 1, GETDATE(), GETDATE());

INSERT INTO Articulo(codigo_articulo, marca_articulo, modelo_articulo, descripcion_articulo, id_tipo_insumo, id_categoria_articulo)
VALUES ('XYZES32', 'Toshiba', 'Satellite X3', 'Un pc tanto para la oficina como para el hogar', 1, 2);

INSERT INTO Articulo(codigo_articulo, marca_articulo, modelo_articulo, descripcion_articulo, id_tipo_insumo, id_categoria_articulo)
VALUES ('ESAE34', 'HP', 'Omen 2022', 'Un pc con la potencia para mover una casa', 1, 2);

INSERT INTO Articulo(codigo_articulo, marca_articulo, modelo_articulo, descripcion_articulo, id_tipo_insumo, id_categoria_articulo)
VALUES ('JFJ4594', 'Swift', 'Chaleco Talla M', 'Un abrigador chaleco', 1, 3);

INSERT INTO Articulo(codigo_articulo, marca_articulo, modelo_articulo, descripcion_articulo, id_tipo_insumo, id_categoria_articulo)
VALUES ('QWRJ487', 'Mademsa', 'Refrigerador', 'Un refrigerador tamaño XL', 1, 1);

INSERT INTO Articulo(codigo_articulo, marca_articulo, modelo_articulo, descripcion_articulo, id_tipo_insumo, id_categoria_articulo)
VALUES ('MTYYUD', 'Monster', 'Cortador de Cesped', 'Para cortar pasto y otros', 1, 1);

```

# Crear procedimiento almacenado CREATE Categorias
```
CREATE PROCEDURE [DBO].[SP_GUARDAR_CATEGORIA]
(
	@glosa_categoria_articulo varchar(200),
	@vigente_categoria_articulo bit,
	@id_cliente int,
	@usuario_creador int,
	@usuario_modificador int,
	@fecha_modificacion datetime,
    @fecha_creacion datetime
)
AS
BEGIN TRY

INSERT INTO Categoria(glosa_categoria_articulo, vigente_categoria_articulo, id_cliente, usuario_creador, usuario_modificador, fecha_modificacion, fecha_creacion) 
OUTPUT INSERTED.id_categoria_articulo
VALUES(@glosa_categoria_articulo, @vigente_categoria_articulo, @id_cliente, @usuario_creador, @usuario_modificador, @fecha_modificacion, @fecha_creacion);
SELECT CAST(SCOPE_IDENTITY() as int)
END TRY

BEGIN CATCH
SELECT ERROR_MESSAGE() as RESPONSE;
END CATCH
```


## Crear procedimiento UPDATE Categoria
```
CREATE PROCEDURE[DBO].[SP_ACTUALIZAR_CATEGORIA]
        @id_categoria_articulo int,
		@glosa_categoria_articulo varchar(200),
		@vigente_categoria_articulo bit,
		@id_cliente int,
		@usuario_creador int,
		@usuario_modificador int,
		@fecha_modificacion datetime,
		@fecha_creacion datetime
  AS
BEGIN
     UPDATE Categoria 
  SET 
	 glosa_categoria_articulo = @glosa_categoria_articulo,
	 vigente_categoria_articulo = @vigente_categoria_articulo,
	 id_cliente = @id_cliente,
	 usuario_creador = @usuario_creador,
	 usuario_modificador = @usuario_modificador,
	 fecha_modificacion  = @fecha_modificacion,
	 fecha_creacion = @fecha_creacion
 WHERE id_categoria_articulo  = @id_categoria_articulo 
 END
```

## Crear procedimiento GET Categoria

```
CREATE PROCEDURE [DBO].[SP_CONSULTAR_CATEGORIA]
AS
SELECT * FROM Categoria
GO;

@id_categoria_articulo int
AS
SELECT * FROM Categoria WHERE id_categoria_articulo = @id_categoria_articulo
GO
```

## Crear procedimiento DELETE Categoria

```
CREATE PROCEDURE [DBO].[SP_ELIMINAR_CATEGORIA]
(
        @id_categoria_articulo int
)
AS
BEGIN
 DELETE FROM Categoria
 WHERE id_categoria_articulo  = @id_categoria_articulo 
END
```

## Crear procedimiento UPDATE Articulo

```
CREATE PROCEDURE[DBO].[SP_ACTUALIZAR_ARTICULO]
    @id_articulo int,
    @codigo_articulo varchar(500),
	@marca_articulo varchar(100),
	@modelo_articulo varchar(100),
	@descripcion_articulo varchar(2000),
	@id_tipo_insumo int,
	@id_categoria_articulo int
  AS
BEGIN
     UPDATE Articulo 
  SET 
    codigo_articulo = @codigo_articulo,
	marca_articulo = @marca_articulo,
	modelo_articulo = @modelo_articulo,
	descripcion_articulo = @descripcion_articulo,
	id_tipo_insumo = @id_tipo_insumo,
	id_categoria_articulo = @id_categoria_articulo
 WHERE id_articulo  = @id_articulo
 END
```

## Crear procedimiento CREATE Articulo

```
CREATE PROCEDURE [DBO].[SP_GUARDAR_ARTICULO]
(

	@codigo_articulo varchar(500),
	@marca_articulo varchar(100),
	@modelo_articulo varchar(100),
	@descripcion_articulo varchar(2000),
	@id_tipo_insumo int,
        @id_categoria_articulo int
)
AS
BEGIN TRY

INSERT INTO Articulo(codigo_articulo, marca_articulo, modelo_articulo, descripcion_articulo, id_tipo_insumo, id_categoria_articulo) 
OUTPUT INSERTED.id_articulo
VALUES(@codigo_articulo, @marca_articulo, @modelo_articulo, @descripcion_articulo, @id_tipo_insumo, @id_categoria_articulo);
SELECT CAST(SCOPE_IDENTITY() as int)
END TRY

BEGIN CATCH
SELECT ERROR_MESSAGE() as RESPONSE;
END CATCH
```


