
--Creacion de tablas

create table Datos_Clientes
(
	dni			numeric(18,0) primary key,	
	nombre		nvarchar(255),
	apellido	nvarchar(255),
	direccion	nvarchar(255),
	telefono	numeric(18,0),
	mail		nvarchar(255),
	fecha_nac   datetime,
	ciudad      nvarchar(255)

)

create table Cupones
(
	codigo 			 nvarchar(50) primary key,
	precio			 numeric(18,2),
	precio_fict 	 numeric(18,2),
	cantidad	     numeric(18,0),
	descripcion	     nvarchar(255)		
)


create table Cupones_Transacciones
(
	codigo 			 nvarchar(50),
	fecha 			 datetime,
	fecha_venc 		 datetime,
	fecha_compra	 datetime,
	fecha_devolucion datetime,
	fecha_entregado  datetime,

	foreign key (codigo) references dbo.Cupones(codigo)

)


create table Giftcard
(
	fecha 		datetime,
	monto		numeric(18,2),
	cliente_origen numeric(18,0) constraint dni1 foreign key  references dbo.Datos_Clientes(dni),
	cliente_destino numeric(18,0) constraint dni2 foreign key  references dbo.Datos_Clientes(dni)

)

create table cargas
(
	dni numeric(18,0),
	carga_credito numeric(18,2),
	carga_fecha datetime,

	foreign key (dni) references dbo.Datos_Clientes(dni)
)

create table Datos_Proveedores (

	provee_cuit nvarchar(20),
	provee_rs nvarchar(100),
	provee_dom nvarchar(100),
	provee_ciudad nvarchar(255),
	provee_telefono numeric(18,0),
	provee_rubro nvarchar(100),
	primary key(provee_cuit,provee_rs)
)

create table Factura (

	factura_nro numeric(18,0) PRIMARY KEY,
	factura_fecha datetime,
	fecha_inicio_periodo datetime,
	fecha_fin_periodo datetime,
	provee_cuit nvarchar(20),
	provee_rs nvarchar(100),
	FOREIGN KEY (provee_cuit,provee_rs) references Datos_Proveedores(provee_cuit,provee_rs)
)

create table Renglon_Factura (

	factura_nro numeric(18,0),
	cupon nvarchar(50),
	FOREIGN KEY (factura_nro) references Factura(factura_nro),
	FOREIGN KEY (cupon) references Cupones(codigo),
	cantidad numeric(18,0)

)

create table Credito (

	cli_dni numeric(18,0) PRIMARY KEY,
	username nvarchar(100),
	valor_credito numeric(18,2),
	fecha_ultima_cargada datetime

)


create table Login (

	id_usuario numeric(18,0) PRIMARY KEY,
	username nvarchar(100),
	user_password nvarchar(64),
	lastlogin datetime,
	ip nvarchar(15),
	rol_asignado numeric(18,0),
	intentos_fallidos numeric(18,0),
	inhabilitado bit,

)
--- Termina la creacion de tablas, arranca el relleno de datos




insert into Datos_Clientes (dni, nombre, apellido, direccion, telefono, mail, fecha_nac, ciudad)
	
	select distinct master.Cli_Dni, master.Cli_Nombre, master.Cli_Apellido, 
		   master.Cli_Direccion, master.Cli_Telefono, master.Cli_Mail, master.Cli_Fecha_Nac, master.Cli_Ciudad
		from gd_esquema.Maestra master 

		where master.Cli_Dni is not null

