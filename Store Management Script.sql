USE [master]
GO
/****** Object:  Database [StoreManagement]    Script Date: 15.4.2020 14:17:31 ******/
CREATE DATABASE [StoreManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StoreManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.BERKAY\MSSQL\DATA\StoreManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'StoreManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.BERKAY\MSSQL\DATA\StoreManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [StoreManagement] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StoreManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StoreManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StoreManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StoreManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StoreManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StoreManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [StoreManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [StoreManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StoreManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StoreManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StoreManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StoreManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StoreManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StoreManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StoreManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StoreManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [StoreManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StoreManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StoreManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StoreManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StoreManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StoreManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StoreManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StoreManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [StoreManagement] SET  MULTI_USER 
GO
ALTER DATABASE [StoreManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StoreManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StoreManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StoreManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [StoreManagement] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'StoreManagement', N'ON'
GO
ALTER DATABASE [StoreManagement] SET QUERY_STORE = OFF
GO
USE [StoreManagement]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 15.4.2020 14:17:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](10) NULL,
	[Explanation] [nchar](10) NULL,
	[RegistrationStatus] [tinyint] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 15.4.2020 14:17:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Surname] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[RegistrationStatus] [tinyint] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 15.4.2020 14:17:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NULL,
	[Name] [nvarchar](max) NULL,
	[Explanation] [nvarchar](max) NULL,
	[PurchasePrice] [decimal](18, 0) NULL,
	[SalePrice] [decimal](18, 0) NULL,
	[Stock] [int] NULL,
	[RegistrationStatus] [tinyint] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sale]    Script Date: 15.4.2020 14:17:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sale](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[CustomerID] [int] NULL,
	[Profit] [decimal](18, 0) NULL,
	[DateOfSale] [time](7) NULL,
	[NumberOfProducts] [int] NULL,
	[RegistrationStatus] [tinyint] NULL,
 CONSTRAINT [PK_Sale] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 15.4.2020 14:17:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Password] [nvarchar](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [Name], [Password]) VALUES (1, N'admin', N'123456')
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD  CONSTRAINT [FK_Sale_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Sale] CHECK CONSTRAINT [FK_Sale_Customer]
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD  CONSTRAINT [FK_Sale_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Sale] CHECK CONSTRAINT [FK_Sale_Product]
GO
USE [master]
GO
ALTER DATABASE [StoreManagement] SET  READ_WRITE 
GO
