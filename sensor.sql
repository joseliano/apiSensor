USE [Sensor]
GO
/****** Object:  Table [dbo].[Pais]    Script Date: 17/06/2019 15:08:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pais](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NULL,
	[Ativo] [nchar](1) NULL CONSTRAINT [DF_Pais_ativo]  DEFAULT ((1)),
 CONSTRAINT [PK_Pais] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Regiao]    Script Date: 17/06/2019 15:08:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Regiao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NULL,
	[Ativo] [char](1) NULL CONSTRAINT [DF_Regiao_ativo]  DEFAULT ((1)),
 CONSTRAINT [PK_Regiao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sensor]    Script Date: 17/06/2019 15:08:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sensor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nchar](10) NULL,
	[idRegiao] [int] NULL,
	[idPais] [int] NULL,
 CONSTRAINT [PK_Sensor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sensor_valor]    Script Date: 17/06/2019 15:08:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sensor_valor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSensor] [int] NULL,
	[Valor] [numeric](18, 0) NULL,
	[TimesTamp] [numeric](18, 0) NULL,
	[DataCadastrado] [smalldatetime] NULL CONSTRAINT [DF_Sensor_valor_DataCadastrado]  DEFAULT (getdate()),
 CONSTRAINT [PK_Sensor_valor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Sensor]  WITH CHECK ADD  CONSTRAINT [FK_Sensor_Pais] FOREIGN KEY([idPais])
REFERENCES [dbo].[Pais] ([Id])
GO
ALTER TABLE [dbo].[Sensor] CHECK CONSTRAINT [FK_Sensor_Pais]
GO
ALTER TABLE [dbo].[Sensor]  WITH CHECK ADD  CONSTRAINT [FK_Sensor_Regiao] FOREIGN KEY([idRegiao])
REFERENCES [dbo].[Regiao] ([Id])
GO
ALTER TABLE [dbo].[Sensor] CHECK CONSTRAINT [FK_Sensor_Regiao]
GO
ALTER TABLE [dbo].[Sensor_valor]  WITH CHECK ADD  CONSTRAINT [FK_Sensor_valor_Sensor] FOREIGN KEY([IdSensor])
REFERENCES [dbo].[Sensor] ([Id])
GO
ALTER TABLE [dbo].[Sensor_valor] CHECK CONSTRAINT [FK_Sensor_valor_Sensor]
GO
