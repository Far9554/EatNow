USE [EatNow]
GO
/****** Object:  Table [dbo].[Casilla]    Script Date: 18/12/2023 19:39:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Casilla](
	[IdCasilla] [int] IDENTITY(1,1) NOT NULL,
	[X] [int] NOT NULL,
	[Y] [int] NOT NULL,
	[NumeroMesa] [int] NULL,
	[EsMesa] [bit] NOT NULL,
	[EstaOcupada] [bit] NOT NULL,
	[RIdRestaurante] [int] NOT NULL,
 CONSTRAINT [PK_Casilla] PRIMARY KEY CLUSTERED 
(
	[IdCasilla] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 18/12/2023 19:39:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[IdCliente] [int] IDENTITY(1,1) NOT NULL,
	[CorreoElectronico] [varchar](100) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellidos] [varchar](100) NULL,
	[Telefono] [varchar](11) NOT NULL,
	[URLFoto] [varchar](500) NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleado]    Script Date: 18/12/2023 19:39:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleado](
	[IdEmpleado] [int] IDENTITY(1,1) NOT NULL,
	[DNI] [char](9) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellidos] [varchar](100) NOT NULL,
	[CorreoElectronico] [varchar](100) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[RIdRestaurante] [int] NOT NULL,
 CONSTRAINT [PK_Empleado] PRIMARY KEY CLUSTERED 
(
	[IdEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoReserva]    Script Date: 18/12/2023 19:39:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoReserva](
	[IdEstado] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NOT NULL,
 CONSTRAINT [PK_EstadoReserva] PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Imagen]    Script Date: 18/12/2023 19:39:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Imagen](
	[IdImagen] [int] IDENTITY(1,1) NOT NULL,
	[URL] [varchar](500) NOT NULL,
	[RIdRestaurante] [int] NOT NULL,
 CONSTRAINT [PK_Imagen] PRIMARY KEY CLUSTERED 
(
	[IdImagen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 18/12/2023 19:39:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[IdPedido] [int] NOT NULL,
	[RIdReserva] [int] NOT NULL,
	[RIdPlato] [int] NOT NULL,
 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED 
(
	[IdPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plato]    Script Date: 18/12/2023 19:39:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plato](
	[IdPlato] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Precio] [money] NOT NULL,
	[RIdRestaurante] [int] NOT NULL,
	[URLImagen] [varchar](500) NULL,
 CONSTRAINT [PK_Plato] PRIMARY KEY CLUSTERED 
(
	[IdPlato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reserva]    Script Date: 18/12/2023 19:39:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reserva](
	[IdReserva] [int] IDENTITY(1,1) NOT NULL,
	[Inicio] [datetime] NOT NULL,
	[Fin] [datetime] NOT NULL,
	[RIdCliente] [int] NOT NULL,
	[RIdEstadoReserva] [int] NOT NULL,
	[RIdCasilla] [int] NOT NULL,
 CONSTRAINT [PK_Reserva] PRIMARY KEY CLUSTERED 
(
	[IdReserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Restaurante]    Script Date: 18/12/2023 19:39:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Restaurante](
	[IdRestaurante] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Direccion] [varchar](100) NOT NULL,
	[Telefono] [varchar](11) NOT NULL,
	[Web] [varchar](200) NULL,
	[Descripcion] [varchar](200) NULL,
	[HoraApertura] [time](7) NOT NULL,
	[HoraCierre] [time](7) NOT NULL,
 CONSTRAINT [PK_Restaurante] PRIMARY KEY CLUSTERED 
(
	[IdRestaurante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Casilla]  WITH CHECK ADD  CONSTRAINT [FK_Casilla_Restaurante] FOREIGN KEY([RIdRestaurante])
REFERENCES [dbo].[Restaurante] ([IdRestaurante])
GO
ALTER TABLE [dbo].[Casilla] CHECK CONSTRAINT [FK_Casilla_Restaurante]
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD  CONSTRAINT [FK_Empleado_Restaurante] FOREIGN KEY([RIdRestaurante])
REFERENCES [dbo].[Restaurante] ([IdRestaurante])
GO
ALTER TABLE [dbo].[Empleado] CHECK CONSTRAINT [FK_Empleado_Restaurante]
GO
ALTER TABLE [dbo].[Imagen]  WITH CHECK ADD  CONSTRAINT [FK_Imagen_Restaurante] FOREIGN KEY([RIdRestaurante])
REFERENCES [dbo].[Restaurante] ([IdRestaurante])
GO
ALTER TABLE [dbo].[Imagen] CHECK CONSTRAINT [FK_Imagen_Restaurante]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Plato] FOREIGN KEY([RIdPlato])
REFERENCES [dbo].[Plato] ([IdPlato])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Plato]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Reserva] FOREIGN KEY([RIdReserva])
REFERENCES [dbo].[Reserva] ([IdReserva])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Reserva]
GO
ALTER TABLE [dbo].[Plato]  WITH CHECK ADD  CONSTRAINT [FK_Plato_Restaurante] FOREIGN KEY([RIdRestaurante])
REFERENCES [dbo].[Restaurante] ([IdRestaurante])
GO
ALTER TABLE [dbo].[Plato] CHECK CONSTRAINT [FK_Plato_Restaurante]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Casilla] FOREIGN KEY([RIdCasilla])
REFERENCES [dbo].[Casilla] ([IdCasilla])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Casilla]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Cliente] FOREIGN KEY([RIdCliente])
REFERENCES [dbo].[Cliente] ([IdCliente])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Cliente]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_EstadoReserva] FOREIGN KEY([RIdEstadoReserva])
REFERENCES [dbo].[EstadoReserva] ([IdEstado])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_EstadoReserva]
GO
