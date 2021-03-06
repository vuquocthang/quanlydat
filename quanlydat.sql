USE [master]
GO
/****** Object:  Database [quanlydat]    Script Date: 5/8/2018 2:27:19 PM ******/
CREATE DATABASE [quanlydat]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'quanlydat', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\quanlydat.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'quanlydat_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\quanlydat_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [quanlydat] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [quanlydat].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [quanlydat] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [quanlydat] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [quanlydat] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [quanlydat] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [quanlydat] SET ARITHABORT OFF 
GO
ALTER DATABASE [quanlydat] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [quanlydat] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [quanlydat] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [quanlydat] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [quanlydat] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [quanlydat] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [quanlydat] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [quanlydat] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [quanlydat] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [quanlydat] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [quanlydat] SET  DISABLE_BROKER 
GO
ALTER DATABASE [quanlydat] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [quanlydat] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [quanlydat] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [quanlydat] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [quanlydat] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [quanlydat] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [quanlydat] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [quanlydat] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [quanlydat] SET  MULTI_USER 
GO
ALTER DATABASE [quanlydat] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [quanlydat] SET DB_CHAINING OFF 
GO
ALTER DATABASE [quanlydat] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [quanlydat] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [quanlydat]
GO
/****** Object:  User [root3]    Script Date: 5/8/2018 2:27:19 PM ******/
CREATE USER [root3] FOR LOGIN [root3] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [root2]    Script Date: 5/8/2018 2:27:20 PM ******/
CREATE USER [root2] FOR LOGIN [root2] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [root]    Script Date: 5/8/2018 2:27:20 PM ******/
CREATE USER [root] FOR LOGIN [root] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [root3]
GO
ALTER ROLE [db_owner] ADD MEMBER [root2]
GO
ALTER ROLE [db_owner] ADD MEMBER [root]
GO
/****** Object:  Table [dbo].[nha]    Script Date: 5/8/2018 2:27:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[nha](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dia_chi] [text] NULL,
	[nam_xay_dung] [nchar](10) NULL,
	[nam_dua_vao_sd] [nchar](10) NULL,
	[so_nam_sd_con_lai] [nchar](10) NULL,
	[nam_ket_thuc] [nchar](10) NULL,
	[so_tang] [int] NULL,
	[dm_cap_cong_trinh] [text] NULL,
	[dien_tich] [nchar](10) NULL,
	[dm_hang_muc_cong_trinh] [text] NULL,
	[nguyen_gia] [nchar](10) NULL,
	[ty_le_con_lai] [nchar](10) NULL,
	[ty_le_khau_hao] [nchar](10) NULL,
	[luy_ke] [nchar](10) NULL,
	[gia_tri_con_lai] [nchar](10) NULL,
	[so_do] [nchar](10) NULL,
	[dm_tinh_trang_su_dung] [text] NULL,
	[ghi_chu_nha] [text] NULL,
	[thoi_gian] [date] NULL,
	[user_id] [int] NULL,
	[ten] [ntext] NULL,
	[lng] [nvarchar](50) NULL,
	[lat] [nvarchar](50) NULL,
 CONSTRAINT [PK_nha] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBL_HO_SO_DAT]    Script Date: 5/8/2018 2:27:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_HO_SO_DAT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_DAT] [nvarchar](50) NULL,
	[MO_TA] [nvarchar](100) NULL,
	[DUONG_DAN] [nvarchar](200) NULL,
	[LOAI] [nvarchar](200) NULL,
	[GHI_CHU] [nvarchar](200) NULL,
	[THOI_GIAN] [date] NULL,
	[USER] [nvarchar](50) NULL,
 CONSTRAINT [PK_TBL_HO_SO_DAT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[users]    Script Date: 5/8/2018 2:27:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nchar](50) NULL,
	[password] [nchar](50) NULL,
	[role_id] [int] NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [quanlydat] SET  READ_WRITE 
GO
