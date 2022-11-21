USE [master]
GO
/****** Object:  Database [PurchaseOrderSystem]    Script Date: 21-11-2022 01:03:34 PM ******/
CREATE DATABASE [PurchaseOrderSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PurchaseOrderSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\PurchaseOrderSystem.mdf' , SIZE = 139264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PurchaseOrderSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\PurchaseOrderSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PurchaseOrderSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PurchaseOrderSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PurchaseOrderSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PurchaseOrderSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PurchaseOrderSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PurchaseOrderSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PurchaseOrderSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [PurchaseOrderSystem] SET  MULTI_USER 
GO
ALTER DATABASE [PurchaseOrderSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PurchaseOrderSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PurchaseOrderSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PurchaseOrderSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PurchaseOrderSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PurchaseOrderSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PurchaseOrderSystem', N'ON'
GO
ALTER DATABASE [PurchaseOrderSystem] SET QUERY_STORE = OFF
GO
USE [PurchaseOrderSystem]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 21-11-2022 01:03:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](200) NULL,
	[Password] [varchar](200) NULL,
	[IsDeleted] [bit] NULL,
	[LoggedOn] [datetime] NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 21-11-2022 01:03:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PurchaseOrderID] [int] NULL,
	[ProductID] [int] NULL,
	[ProductPrice] [float] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](max) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](max) NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderMaster]    Script Date: 21-11-2022 01:03:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [varchar](200) NOT NULL,
	[Status] [int] NULL,
	[Amount] [float] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](max) NULL,
 CONSTRAINT [PK_OrderMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductMaster]    Script Date: 21-11-2022 01:03:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductMaster](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](500) NULL,
	[ProductPrice] [float] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](max) NULL,
 CONSTRAINT [PK_ItemMaster] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Login] ON 
GO
INSERT [dbo].[Login] ([ID], [UserName], [Password], [IsDeleted], [LoggedOn]) VALUES (1, N'Admin', N'CCT', 0, CAST(N'2022-11-18T16:29:17.207' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Login] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetail] ON 
GO
INSERT [dbo].[OrderDetail] ([ID], [PurchaseOrderID], [ProductID], [ProductPrice], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, 1, 2, 50, CAST(N'2022-11-21T12:59:40.777' AS DateTime), N'Admin', CAST(N'2022-11-21T12:59:51.250' AS DateTime), N'Admin')
GO
INSERT [dbo].[OrderDetail] ([ID], [PurchaseOrderID], [ProductID], [ProductPrice], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2, 1, 3, 75, CAST(N'2022-11-21T12:59:40.813' AS DateTime), N'Admin', CAST(N'2022-11-21T12:59:51.253' AS DateTime), N'Admin')
GO
INSERT [dbo].[OrderDetail] ([ID], [PurchaseOrderID], [ProductID], [ProductPrice], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (3, 1, 4, 20, CAST(N'2022-11-21T12:59:40.837' AS DateTime), N'Admin', CAST(N'2022-11-21T12:59:51.257' AS DateTime), N'Admin')
GO
SET IDENTITY_INSERT [dbo].[OrderDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderMaster] ON 
GO
INSERT [dbo].[OrderMaster] ([ID], [OrderNumber], [Status], [Amount], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (1, N'21-11-2022-1', 2, NULL, CAST(N'2022-11-21T12:59:40.607' AS DateTime), N'Admin', CAST(N'2022-11-21T12:59:51.237' AS DateTime), N'Admin')
GO
SET IDENTITY_INSERT [dbo].[OrderMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductMaster] ON 
GO
INSERT [dbo].[ProductMaster] ([ProductID], [ProductName], [ProductPrice], [IsDeleted], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (2, N'Green Tea', 50, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ProductMaster] ([ProductID], [ProductName], [ProductPrice], [IsDeleted], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (3, N'Sugar', 75, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ProductMaster] ([ProductID], [ProductName], [ProductPrice], [IsDeleted], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (4, N'Salt', 20, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ProductMaster] ([ProductID], [ProductName], [ProductPrice], [IsDeleted], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (5, N'Edible Oil', 1000, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ProductMaster] ([ProductID], [ProductName], [ProductPrice], [IsDeleted], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (6, N'Noodles', 500, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ProductMaster] ([ProductID], [ProductName], [ProductPrice], [IsDeleted], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (7, N'vegetables', 100, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ProductMaster] ([ProductID], [ProductName], [ProductPrice], [IsDeleted], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (8, N'Fruits', 150, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ProductMaster] ([ProductID], [ProductName], [ProductPrice], [IsDeleted], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (9, N'shirt', 400, 0, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[ProductMaster] OFF
GO
USE [master]
GO
ALTER DATABASE [PurchaseOrderSystem] SET  READ_WRITE 
GO
