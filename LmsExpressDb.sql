/* LMS Express - script tao CSDL LmsExpressDb (cau truc + du lieu mau).
   Chay duoc tren SQL Server Express / LocalDB / ban day du (2016 tro len).
   Script tu XOA database LmsExpressDb cu (neu co) roi tao moi. */
USE [master]
GO
IF DB_ID(N'LmsExpressDb') IS NOT NULL
BEGIN
    ALTER DATABASE [LmsExpressDb] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [LmsExpressDb];
END
GO
CREATE DATABASE [LmsExpressDb]
GO
USE [LmsExpressDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/8/2026 3:59:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AboutItems]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AboutItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kind] [nvarchar](20) NOT NULL,
	[Value] [nvarchar](150) NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Content] [nvarchar](2000) NULL,
	[Icon] [nvarchar](60) NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsVisible] [bit] NOT NULL,
 CONSTRAINT [PK_AboutItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdminUsers]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[PasswordHash] [nvarchar](200) NOT NULL,
	[PasswordSalt] [nvarchar](100) NOT NULL,
	[RoleId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_AdminUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArticleBlocks]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleBlocks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NOT NULL,
	[Heading] [nvarchar](250) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_ArticleBlocks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Articles]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Icon] [nvarchar](60) NOT NULL,
	[Category] [nvarchar](100) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Summary] [nvarchar](500) NOT NULL,
	[PublishedAt] [datetime2](7) NOT NULL,
	[ArtClass] [nvarchar](50) NOT NULL,
	[IsPublished] [bit] NOT NULL,
	[FeaturedOrder] [int] NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[IsFeatured] [bit] NOT NULL,
 CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CargoTypes]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CargoTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](300) NULL,
	[IsSpecial] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_CargoTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](150) NOT NULL,
	[PhoneNumber] [nvarchar](30) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Message] [nvarchar](2000) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[IsRead] [bit] NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Drivers]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drivers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](150) NOT NULL,
	[Phone] [nvarchar](30) NOT NULL,
	[LicenseNumber] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](30) NOT NULL,
	[Note] [nvarchar](500) NULL,
 CONSTRAINT [PK_Drivers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeSectionItems]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeSectionItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SectionId] [int] NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Icon] [nvarchar](60) NULL,
	[ImageUrl] [nvarchar](300) NULL,
	[LinkUrl] [nvarchar](300) NULL,
	[Value] [nvarchar](50) NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsVisible] [bit] NOT NULL,
 CONSTRAINT [PK_HomeSectionItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeSections]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeSections](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SectionType] [nvarchar](50) NOT NULL,
	[Kicker] [nvarchar](100) NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Subtitle] [nvarchar](500) NULL,
	[Body] [nvarchar](max) NULL,
	[ButtonText] [nvarchar](100) NULL,
	[ButtonUrl] [nvarchar](300) NULL,
	[SecondaryButtonText] [nvarchar](100) NULL,
	[SecondaryButtonUrl] [nvarchar](300) NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsVisible] [bit] NOT NULL,
	[IsSystem] [bit] NOT NULL,
 CONSTRAINT [PK_HomeSections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MediaItems]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MediaItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](20) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[ImageUrl] [nvarchar](300) NULL,
	[LinkUrl] [nvarchar](300) NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsVisible] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_MediaItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsletterSubscribers]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsletterSubscribers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[SubscribedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_NewsletterSubscribers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderEvents]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderEvents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[SequenceNumber] [int] NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Detail] [nvarchar](500) NULL,
	[EventAt] [datetime2](7) NOT NULL,
	[IsCompleted] [bit] NOT NULL,
 CONSTRAINT [PK_OrderEvents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[CargoTypeId] [int] NULL,
	[CargoTypeName] [nvarchar](100) NOT NULL,
	[Quantity] [int] NOT NULL,
	[WeightKg] [decimal](18, 2) NULL,
	[IsFragile] [bit] NOT NULL,
	[Note] [nvarchar](300) NULL,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderCode] [nvarchar](50) NOT NULL,
	[CustomerId] [int] NULL,
	[CustomerName] [nvarchar](150) NOT NULL,
	[Phone] [nvarchar](30) NOT NULL,
	[Email] [nvarchar](200) NULL,
	[ServiceId] [int] NULL,
	[VehicleId] [int] NULL,
	[DriverId] [int] NULL,
	[PickupAddress] [nvarchar](300) NOT NULL,
	[DeliveryAddress] [nvarchar](300) NOT NULL,
	[CargoType] [nvarchar](150) NULL,
	[WeightKg] [decimal](18, 2) NULL,
	[PackageCount] [int] NULL,
	[ExpectedShipDate] [datetime2](7) NULL,
	[Status] [nvarchar](30) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[Note] [nvarchar](1000) NULL,
	[QuoteNote] [nvarchar](1000) NULL,
	[QuoteToken] [nvarchar](64) NULL,
	[QuoteValidUntil] [datetime2](7) NULL,
	[QuotedPrice] [decimal](18, 0) NULL,
	[ReceiverName] [nvarchar](150) NULL,
	[ReceiverPhone] [nvarchar](30) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PasswordResetRequests]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PasswordResetRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[IsHandled] [bit] NOT NULL,
	[HandledAt] [datetime2](7) NULL,
 CONSTRAINT [PK_PasswordResetRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcessSteps]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessSteps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [int] NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Icon] [nvarchar](60) NOT NULL,
 CONSTRAINT [PK_ProcessSteps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](300) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SavedAddresses]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SavedAddresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Label] [nvarchar](60) NOT NULL,
	[Address] [nvarchar](300) NOT NULL,
	[ContactName] [nvarchar](100) NULL,
	[Phone] [nvarchar](30) NULL,
 CONSTRAINT [PK_SavedAddresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SeoPages]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeoPages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Route] [nvarchar](100) NOT NULL,
	[PageName] [nvarchar](100) NOT NULL,
	[MetaTitle] [nvarchar](70) NULL,
	[MetaDescription] [nvarchar](300) NULL,
	[Keywords] [nvarchar](300) NULL,
 CONSTRAINT [PK_SeoPages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Summary] [nvarchar](500) NOT NULL,
	[Icon] [nvarchar](60) NOT NULL,
	[DeliveryTime] [nvarchar](100) NOT NULL,
	[PriceNote] [nvarchar](150) NOT NULL,
	[IsFeatured] [bit] NOT NULL,
	[FeaturedOrder] [int] NOT NULL,
	[ImageUrl] [nvarchar](300) NULL,
	[Description] [nvarchar](4000) NULL,
	[IsVisible] [bit] NOT NULL,
	[IsExclusive] [bit] NOT NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiteSettings]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteName] [nvarchar](100) NOT NULL,
	[LogoUrl] [nvarchar](300) NULL,
	[FaviconUrl] [nvarchar](300) NULL,
	[Hotline] [nvarchar](30) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Address] [nvarchar](300) NOT NULL,
	[WorkingHours] [nvarchar](100) NULL,
	[FooterIntro] [nvarchar](500) NULL,
	[Copyright] [nvarchar](200) NULL,
	[FacebookUrl] [nvarchar](300) NULL,
	[YoutubeUrl] [nvarchar](300) NULL,
	[LinkedInUrl] [nvarchar](300) NULL,
	[TikTokUrl] [nvarchar](300) NULL,
	[ZaloUrl] [nvarchar](300) NULL,
 CONSTRAINT [PK_SiteSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StaticPages]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StaticPages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Slug] [nvarchar](80) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Subtitle] [nvarchar](300) NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Icon] [nvarchar](60) NOT NULL,
	[IsPublished] [bit] NOT NULL,
	[IsSystem] [bit] NOT NULL,
 CONSTRAINT [PK_StaticPages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TripOrders]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TripOrders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TripId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_TripOrders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trips]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trips](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Route] [nvarchar](300) NOT NULL,
	[VehicleId] [int] NOT NULL,
	[DriverId] [int] NOT NULL,
	[DepartAt] [datetime2](7) NULL,
	[Status] [nvarchar](30) NOT NULL,
	[Note] [nvarchar](500) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Trips] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Phone] [nvarchar](30) NOT NULL,
	[PasswordHash] [nvarchar](200) NOT NULL,
	[PasswordSalt] [nvarchar](100) NOT NULL,
	[Role] [nvarchar](30) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 11/8/2026 3:59:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlateNumber] [nvarchar](20) NOT NULL,
	[VehicleType] [nvarchar](50) NOT NULL,
	[CapacityKg] [decimal](18, 2) NULL,
	[Status] [nvarchar](30) NOT NULL,
	[Note] [nvarchar](500) NULL,
 CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20260928021241_InitialCreate', N'8.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261003033813_AddIsSystemToHomeSection', N'8.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261003091815_AddImageUrlToService', N'8.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261004130344_AddArticleImageAndFeatured', N'8.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261005063649_AddVehicleAndDriver', N'8.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261005070336_AddOrderAndOrderEvent', N'8.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261005091659_AddQuoteFields', N'8.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261005103531_AddPasswordResetRequest', N'8.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261010084247_AddReceiverAndSavedAddress', N'8.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261010102128_AddServiceFieldsAndCargoType', N'8.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261010130131_AddSettingsSeoPagesMedia', N'8.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261011073350_AddAboutItems', N'8.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261012030834_AddZaloAndIsActive', N'8.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261013150811_AddOrderItemsAndAddressContact', N'8.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261031092402_AddTrips', N'8.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261031190806_AddAdminUsersAndRoles', N'8.0.31')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261101080517_AddServiceExclusive', N'8.0.31')
GO
SET IDENTITY_INSERT [dbo].[AboutItems] ON 

INSERT [dbo].[AboutItems] ([Id], [Kind], [Value], [Title], [Content], [Icon], [DisplayOrder], [IsVisible]) VALUES (1, N'Intro', N'CÂU CHUYỆN CỦA CHÚNG TÔI', N'Vận chuyển không chỉ là giao một kiện hàng', N'Đó là hành trình trao gửi sự tin cậy. LMS Express xây dựng hệ thống giao nhận minh bạch, đội ngũ chuyên nghiệp và quy trình kiểm soát chặt chẽ để mỗi đơn hàng đến nơi an toàn.

Chúng tôi không ngừng cải tiến công nghệ, mở rộng mạng lưới và tối ưu trải nghiệm cho cả khách hàng cá nhân lẫn doanh nghiệp.', NULL, 1, 1)
INSERT [dbo].[AboutItems] ([Id], [Kind], [Value], [Title], [Content], [Icon], [DisplayOrder], [IsVisible]) VALUES (2, N'Vision', NULL, N'Tầm nhìn', N'Trở thành đơn vị logistics đáng tin cậy, kết nối hàng hóa trên mọi miền đất nước bằng công nghệ minh bạch.', N'bi-eye', 1, 1)
INSERT [dbo].[AboutItems] ([Id], [Kind], [Value], [Title], [Content], [Icon], [DisplayOrder], [IsVisible]) VALUES (3, N'Mission', NULL, N'Sứ mệnh', N'Mang đến dịch vụ giao nhận an toàn, đúng hẹn và chi phí hợp lý cho cá nhân và doanh nghiệp.', N'bi-bullseye', 1, 1)
INSERT [dbo].[AboutItems] ([Id], [Kind], [Value], [Title], [Content], [Icon], [DisplayOrder], [IsVisible]) VALUES (4, N'CoreValue', NULL, N'Tận tâm', N'Đặt lợi ích và sự yên tâm của khách hàng lên hàng đầu.', N'bi-heart', 1, 1)
INSERT [dbo].[AboutItems] ([Id], [Kind], [Value], [Title], [Content], [Icon], [DisplayOrder], [IsVisible]) VALUES (5, N'CoreValue', NULL, N'Minh bạch', N'Báo giá rõ ràng, theo dõi hành trình đơn hàng từng bước.', N'bi-patch-check', 2, 1)
INSERT [dbo].[AboutItems] ([Id], [Kind], [Value], [Title], [Content], [Icon], [DisplayOrder], [IsVisible]) VALUES (6, N'CoreValue', NULL, N'Đúng hẹn', N'Cam kết thời gian giao hàng và chủ động thông báo khi có thay đổi.', N'bi-clock-history', 3, 1)
INSERT [dbo].[AboutItems] ([Id], [Kind], [Value], [Title], [Content], [Icon], [DisplayOrder], [IsVisible]) VALUES (7, N'CoreValue', NULL, N'An toàn', N'Kiểm soát hàng hóa chặt chẽ từ lúc nhận đến lúc giao.', N'bi-shield-check', 4, 1)
INSERT [dbo].[AboutItems] ([Id], [Kind], [Value], [Title], [Content], [Icon], [DisplayOrder], [IsVisible]) VALUES (8, N'Stat', N'8+', N'Năm kinh nghiệm', NULL, NULL, 1, 1)
INSERT [dbo].[AboutItems] ([Id], [Kind], [Value], [Title], [Content], [Icon], [DisplayOrder], [IsVisible]) VALUES (9, N'Stat', N'63', N'Tỉnh thành', NULL, NULL, 2, 1)
INSERT [dbo].[AboutItems] ([Id], [Kind], [Value], [Title], [Content], [Icon], [DisplayOrder], [IsVisible]) VALUES (10, N'Stat', N'500+', N'Đối tác', NULL, NULL, 3, 1)
INSERT [dbo].[AboutItems] ([Id], [Kind], [Value], [Title], [Content], [Icon], [DisplayOrder], [IsVisible]) VALUES (11, N'Stat', N'1M+', N'Đơn thành công', NULL, NULL, 4, 1)
INSERT [dbo].[AboutItems] ([Id], [Kind], [Value], [Title], [Content], [Icon], [DisplayOrder], [IsVisible]) VALUES (12, N'Milestone', N'2018', N'Thành lập LMS Express', N'Bắt đầu với dịch vụ giao hàng nội thành TP. Hồ Chí Minh.', NULL, 1, 1)
INSERT [dbo].[AboutItems] ([Id], [Kind], [Value], [Title], [Content], [Icon], [DisplayOrder], [IsVisible]) VALUES (13, N'Milestone', N'2020', N'Mở rộng liên tỉnh', N'Mở tuyến vận chuyển Bắc - Nam và dịch vụ cho thuê xe tải.', NULL, 2, 1)
INSERT [dbo].[AboutItems] ([Id], [Kind], [Value], [Title], [Content], [Icon], [DisplayOrder], [IsVisible]) VALUES (14, N'Milestone', N'2023', N'Số hóa quy trình', N'Đưa vào hệ thống theo dõi trạng thái đơn hàng theo thời gian thực.', NULL, 3, 1)
INSERT [dbo].[AboutItems] ([Id], [Kind], [Value], [Title], [Content], [Icon], [DisplayOrder], [IsVisible]) VALUES (15, N'Milestone', N'2026', N'Nền tảng báo giá trực tuyến', N'Khách hàng gửi yêu cầu, nhận báo giá và theo dõi đơn ngay trên website.', NULL, 4, 1)
SET IDENTITY_INSERT [dbo].[AboutItems] OFF
GO
SET IDENTITY_INSERT [dbo].[AdminUsers] ON 

INSERT [dbo].[AdminUsers] ([Id], [FullName], [Email], [PasswordHash], [PasswordSalt], [RoleId], [IsActive], [CreatedAt]) VALUES (1, N'Quản trị viên', N'admin@lms.com', N'+FNcIKkFXW1S6IXrwHURAbjQuelEfEON73g2n1tb0eg=', N'sZxgXmxq6S+vvdiUFEFMtg==', 1, 1, CAST(N'2026-11-01T02:40:48.2692641' AS DateTime2))
INSERT [dbo].[AdminUsers] ([Id], [FullName], [Email], [PasswordHash], [PasswordSalt], [RoleId], [IsActive], [CreatedAt]) VALUES (2, N'Đỗ Duy Tấn', N'haiduy01366@gmail.com', N'ok1gCeNEkkjlxfp4S08GDhcoA/hWnK05EkBsfkxs0aY=', N'M98xeD5NcjfkrG9aS7jXiA==', 2, 1, CAST(N'2026-11-01T02:42:28.0569687' AS DateTime2))
SET IDENTITY_INSERT [dbo].[AdminUsers] OFF
GO
SET IDENTITY_INSERT [dbo].[ArticleBlocks] ON 

INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (1, 1, N'1. Số hóa thông tin vận chuyển', N'Quản lý vận đơn tại một nơi giúp hạn chế nhập liệu lặp lại. Doanh nghiệp có thể bắt đầu bằng việc thống nhất mã hàng, trạng thái xử lý và dữ liệu người nhận.', 1)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (2, 1, N'2. Tối ưu tuyến đường', N'Gom các điểm giao gần nhau và lựa chọn phương tiện phù hợp có thể giảm số chuyến đi rỗng. Cần đánh giá thêm giờ nhận hàng, tải trọng và điều kiện giao nhận.', 2)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (3, 1, N'3. Tái sử dụng vật liệu đóng gói', N'Thùng và vật liệu chèn lót phù hợp giúp bảo vệ sản phẩm. Doanh nghiệp nên cân nhắc tái sử dụng khi vật liệu vẫn đáp ứng yêu cầu an toàn.', 3)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (4, 1, N'4. Bố trí kho linh hoạt', N'Vị trí lưu hàng gần khu vực có nhu cầu lớn có thể hỗ trợ giao nhận thuận tiện hơn. Quyết định cần dựa trên lượng đơn, chi phí thuê kho và tồn kho thực tế.', 4)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (5, 1, N'5. Chủ động thông báo', N'Thông tin rõ ràng về thời gian dự kiến và các thay đổi giúp khách hàng sắp xếp nhận hàng. Mỗi thông báo nên gắn với trạng thái đã được kiểm chứng.', 5)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (6, 2, N'1. Chọn bao bì phù hợp', N'Kiểm tra trọng lượng và kích thước hàng trước khi chọn thùng. Bao bì cần nguyên vẹn, khô và có khả năng bảo vệ sản phẩm trong quá trình bốc xếp.', 1)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (7, 2, N'2. Cố định hàng bên trong', N'Dùng vật liệu chèn lót để hạn chế xê dịch. Các vật dễ vỡ cần được ngăn cách, tránh va chạm trực tiếp với nhau và thành thùng.', 2)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (8, 2, N'3. Dán nhãn và kiểm tra', N'Ghi rõ thông tin người nhận, mã kiện và hướng đặt hàng nếu cần. Chụp lại tình trạng kiện hàng khi bàn giao để thuận tiện đối chiếu.', 3)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (9, 2, N'4. Hàng đặc thù', N'Hàng lạnh, chất lỏng, pin hoặc vật liệu có yêu cầu riêng cần được trao đổi trước với đơn vị vận chuyển. Không áp dụng một cách đóng gói cho tất cả hàng hóa.', 4)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (10, 3, N'1. Cung cấp mô tả hàng hóa rõ ràng', N'Chuẩn bị tên hàng, số lượng, khối lượng và kích thước đóng gói để đơn vị vận chuyển tư vấn tuyến phù hợp.', 1)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (11, 3, N'2. Xác minh yêu cầu trước khi gửi', N'Quy định khác nhau theo nơi đi, nơi đến và nhóm hàng. Cần kiểm tra thông tin hiện hành với cơ quan hoặc đơn vị chuyên môn; nội dung mẫu này không thay thế tư vấn thủ tục.', 2)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (12, 3, N'3. Làm rõ phạm vi báo giá', N'Hỏi rõ dịch vụ bao gồm những chặng nào, thời gian dự kiến và các khoản chưa bao gồm. Thống nhất đầu mối liên hệ khi phát sinh.', 3)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (13, 3, N'4. Lưu hồ sơ bàn giao', N'Giữ mã vận đơn, ảnh kiện hàng và các chứng từ do bên liên quan cung cấp để đối chiếu khi cần.', 4)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (14, 4, N'Hàng ghép', N'Nhiều lô hàng chia sẻ cùng phương tiện. Cách này có thể phù hợp với lô nhỏ và lịch giao linh hoạt; cần làm rõ số lần trung chuyển và quy cách đóng gói.', 1)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (15, 4, N'Xe nguyên chuyến', N'Phương tiện phục vụ riêng lô hàng theo thỏa thuận. Có thể phù hợp với lượng hàng lớn, nhiều điểm giao hoặc yêu cầu kiểm soát lịch trình.', 2)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (16, 4, N'Thông tin cần so sánh', N'Đánh giá tổng chi phí, tải trọng thực, thời gian, điều kiện bốc dỡ và khả năng tiếp cận điểm giao. Nên yêu cầu báo giá cho cả hai phương án.', 3)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (17, 5, N'Đo lường trước khi thay đổi', N'Theo dõi số chuyến, quãng đường, mức sử dụng xe và lượng bao bì. Chọn một chỉ số cụ thể để đánh giá hiệu quả của mỗi thử nghiệm.', 1)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (18, 5, N'Gom đơn có kế hoạch', N'Kết hợp các đơn tương thích về thời gian và khu vực có thể giảm số chuyến. Không bỏ qua giới hạn tải trọng và yêu cầu bảo quản riêng.', 2)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (19, 5, N'Cải thiện bao bì', N'Chọn kích thước vừa với sản phẩm, tránh dùng quá nhiều vật liệu chèn. Cần kiểm tra chất lượng bảo vệ hàng trước khi áp dụng rộng rãi.', 3)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (20, 6, N'Thống nhất mã sản phẩm', N'Một mã hàng thống nhất giữa cửa hàng và kênh trực tuyến giúp giảm nhầm lẫn khi soạn hàng và cập nhật tồn kho.', 1)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (21, 6, N'Chuẩn hóa quy trình bàn giao', N'Xác định thời điểm chốt đơn, người phụ trách đóng gói và thông tin cần kiểm tra trước khi chuyển hàng cho đơn vị giao nhận.', 2)
INSERT [dbo].[ArticleBlocks] ([Id], [ArticleId], [Heading], [Body], [DisplayOrder]) VALUES (22, 6, N'Theo dõi ngoại lệ', N'Tách riêng đơn thiếu hàng, sai địa chỉ hoặc cần đổi lịch. Lưu lý do để nhóm hỗ trợ có thể xử lý và thông báo nhất quán cho khách hàng.', 3)
SET IDENTITY_INSERT [dbo].[ArticleBlocks] OFF
GO
SET IDENTITY_INSERT [dbo].[Articles] ON 

INSERT [dbo].[Articles] ([Id], [Icon], [Category], [Title], [Summary], [PublishedAt], [ArtClass], [IsPublished], [FeaturedOrder], [ImageUrl], [IsFeatured]) VALUES (1, N'bi-graph-up-arrow', N'Thị trường logistics', N'5 xu hướng logistics nổi bật giúp doanh nghiệp tối ưu chi phí a', N'Công nghệ, dữ liệu và những cách tổ chức vận hành đáng cân nhắc trong một chuỗi cung ứng hiện đại.', CAST(N'2026-09-12T00:00:00.0000000' AS DateTime2), N'art-one', 1, 1, N'/images/News/4.jpg', 1)
INSERT [dbo].[Articles] ([Id], [Icon], [Category], [Title], [Summary], [PublishedAt], [ArtClass], [IsPublished], [FeaturedOrder], [ImageUrl], [IsFeatured]) VALUES (2, N'bi-box-seam', N'Kinh nghiệm', N'Cách đóng gói hàng hóa an toàn khi vận chuyển đường dài', N'Chọn thùng, chèn lót và ghi nhãn phù hợp với đặc tính từng loại hàng hóa.', CAST(N'2026-09-08T00:00:00.0000000' AS DateTime2), N'art-two', 1, 2, N'/images/News/Lua-chon-bao-bi-dong-goi-hang-gia-dung-chat-luong-phu-hop.png', 1)
INSERT [dbo].[Articles] ([Id], [Icon], [Category], [Title], [Summary], [PublishedAt], [ArtClass], [IsPublished], [FeaturedOrder], [ImageUrl], [IsFeatured]) VALUES (3, N'bi-globe2', N'Xuất nhập khẩu', N'Những lưu ý khi gửi hàng quốc tế doanh nghiệp cần biết', N'Bài viết mẫu về việc chuẩn bị thông tin và trao đổi với đơn vị vận chuyển.', CAST(N'2026-09-02T00:00:00.0000000' AS DateTime2), N'art-three', 1, 3, N'/images/News/4.jpg', 1)
INSERT [dbo].[Articles] ([Id], [Icon], [Category], [Title], [Summary], [PublishedAt], [ArtClass], [IsPublished], [FeaturedOrder], [ImageUrl], [IsFeatured]) VALUES (4, N'bi-truck', N'Kinh nghiệm', N'Nên chọn hàng ghép hay thuê xe nguyên chuyến?', N'So sánh hai phương án theo khối lượng, lịch giao và yêu cầu bảo quản.', CAST(N'2026-08-28T00:00:00.0000000' AS DateTime2), N'art-four', 1, 0, NULL, 0)
INSERT [dbo].[Articles] ([Id], [Icon], [Category], [Title], [Summary], [PublishedAt], [ArtClass], [IsPublished], [FeaturedOrder], [ImageUrl], [IsFeatured]) VALUES (5, N'bi-leaf', N'Thị trường logistics', N'Logistics xanh: Bắt đầu từ những thay đổi nhỏ', N'Những ý tưởng thực hành để giảm lãng phí trong đóng gói và điều phối.', CAST(N'2026-08-21T00:00:00.0000000' AS DateTime2), N'art-five', 1, 0, NULL, 0)
INSERT [dbo].[Articles] ([Id], [Icon], [Category], [Title], [Summary], [PublishedAt], [ArtClass], [IsPublished], [FeaturedOrder], [ImageUrl], [IsFeatured]) VALUES (6, N'bi-shop', N'Kinh nghiệm', N'Giải pháp giao hàng cho doanh nghiệp bán lẻ đa kênh', N'Tổ chức tồn kho và bàn giao để xử lý đơn từ nhiều kênh bán hàng.', CAST(N'2026-08-15T00:00:00.0000000' AS DateTime2), N'art-six', 1, 0, NULL, 0)
SET IDENTITY_INSERT [dbo].[Articles] OFF
GO
SET IDENTITY_INSERT [dbo].[CargoTypes] ON 

INSERT [dbo].[CargoTypes] ([Id], [Name], [Description], [IsSpecial], [IsActive]) VALUES (1, N'Hàng tiêu dùng', N'Quần áo, đồ gia dụng, hàng bán lẻ.', 0, 1)
INSERT [dbo].[CargoTypes] ([Id], [Name], [Description], [IsSpecial], [IsActive]) VALUES (2, N'Thiết bị điện tử', N'Máy tính, điện thoại, linh kiện.', 1, 1)
INSERT [dbo].[CargoTypes] ([Id], [Name], [Description], [IsSpecial], [IsActive]) VALUES (3, N'Nội thất', N'Bàn ghế, tủ, đồ trang trí kích thước lớn.', 0, 1)
INSERT [dbo].[CargoTypes] ([Id], [Name], [Description], [IsSpecial], [IsActive]) VALUES (4, N'Thực phẩm đóng gói', N'Thực phẩm khô, đồ uống đóng chai/hộp.', 0, 1)
INSERT [dbo].[CargoTypes] ([Id], [Name], [Description], [IsSpecial], [IsActive]) VALUES (5, N'Vật liệu xây dựng', N'Gạch, xi măng, thép, vật tư công trình.', 0, 1)
INSERT [dbo].[CargoTypes] ([Id], [Name], [Description], [IsSpecial], [IsActive]) VALUES (6, N'Hàng dễ vỡ', N'Thủy tinh, gốm sứ, cần đóng gói và xếp riêng.', 1, 1)
SET IDENTITY_INSERT [dbo].[CargoTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Contacts] ON 

INSERT [dbo].[Contacts] ([Id], [FullName], [PhoneNumber], [Email], [Message], [CreatedAt], [IsRead]) VALUES (1, N'Tấn Đỗ', N'0563426487', N'dot01366@gmail.com', N'a', CAST(N'2026-10-05T10:48:23.8752327' AS DateTime2), 0)
INSERT [dbo].[Contacts] ([Id], [FullName], [PhoneNumber], [Email], [Message], [CreatedAt], [IsRead]) VALUES (2, N'Tấn Đỗ', N'0563426487', N'dot01366@gmail.com', N'a', CAST(N'2026-10-05T14:06:06.7022635' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[Contacts] OFF
GO
SET IDENTITY_INSERT [dbo].[Drivers] ON 

INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (1, N'Đỗ Duy Tấn', N'0563426487', N'22222', N'Đang làm việc', NULL)
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (2, N'Nguyễn Văn Hùng', N'0901234501', N'790123450001', N'Đang làm việc', N'Bằng C, 10 năm kinh nghiệm')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (3, N'Trần Minh Tuấn', N'0901234502', N'790123450002', N'Đang làm việc', N'Bằng C')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (4, N'Lê Quốc Bảo', N'0901234503', N'790123450003', N'Đang làm việc', N'Bằng B2, chạy xe nhỏ nội thành')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (5, N'Phạm Thanh Sơn', N'0901234504', N'790123450004', N'Đang làm việc', N'Bằng C')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (6, N'Hoàng Đức Thịnh', N'0901234505', N'790123450005', N'Đang làm việc', N'Bằng D')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (7, N'Võ Hoàng Nam', N'0901234506', N'790123450006', N'Đang làm việc', N'Bằng C, quen tuyến miền Tây')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (8, N'Đặng Văn Long', N'0901234507', N'790123450007', N'Đang làm việc', N'Bằng FC (container)')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (9, N'Bùi Anh Khoa', N'0901234508', N'790123450008', N'Đang làm việc', N'Bằng C')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (10, N'Ngô Thanh Phong', N'0901234509', N'790123450009', N'Nghỉ', N'Nghỉ phép đến hết tháng')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (11, N'Đỗ Mạnh Cường', N'0901234510', N'790123450010', N'Đang làm việc', N'Bằng FC, tuyến Bắc - Nam')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (12, N'Dương Quang Vinh', N'0901234511', N'790123450011', N'Đang làm việc', N'Bằng C')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (13, N'Lý Hữu Phước', N'0901234512', N'790123450012', N'Đang làm việc', N'Bằng C')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (14, N'Huỳnh Gia Huy', N'0901234513', N'790123450013', N'Đang làm việc', N'Bằng B2')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (15, N'Phan Văn Thắng', N'0901234514', N'790123450014', N'Đang làm việc', N'Bằng D')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (16, N'Trương Công Danh', N'0901234515', N'790123450015', N'Đang làm việc', N'Bằng FC')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (17, N'Vũ Đình Trọng', N'0901234516', N'790123450016', N'Nghỉ', N'Nghỉ ốm')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (18, N'Mai Xuân Trường', N'0901234517', N'790123450017', N'Đang làm việc', N'Bằng C')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (19, N'Tạ Quốc Việt', N'0901234518', N'790123450018', N'Đang làm việc', N'Bằng C')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (20, N'Châu Minh Khang', N'0901234519', N'790123450019', N'Đang làm việc', N'Bằng A2, giao hỏa tốc xe máy')
INSERT [dbo].[Drivers] ([Id], [FullName], [Phone], [LicenseNumber], [Status], [Note]) VALUES (21, N'Lâm Thế Anh', N'0901234520', N'790123450020', N'Đang làm việc', N'Bằng FC, container 40 feet')
SET IDENTITY_INSERT [dbo].[Drivers] OFF
GO
SET IDENTITY_INSERT [dbo].[HomeSectionItems] ON 

INSERT [dbo].[HomeSectionItems] ([Id], [SectionId], [Title], [Description], [Icon], [ImageUrl], [LinkUrl], [Value], [DisplayOrder], [IsVisible]) VALUES (1, 1, N'Tỉnh thành', NULL, NULL, NULL, NULL, N'63', 1, 1)
INSERT [dbo].[HomeSectionItems] ([Id], [SectionId], [Title], [Description], [Icon], [ImageUrl], [LinkUrl], [Value], [DisplayOrder], [IsVisible]) VALUES (2, 1, N'Đúng hẹn', NULL, NULL, NULL, NULL, N'98%', 2, 1)
INSERT [dbo].[HomeSectionItems] ([Id], [SectionId], [Title], [Description], [Icon], [ImageUrl], [LinkUrl], [Value], [DisplayOrder], [IsVisible]) VALUES (3, 1, N'Hỗ trợ', NULL, NULL, NULL, NULL, N'24/7', 3, 1)
INSERT [dbo].[HomeSectionItems] ([Id], [SectionId], [Title], [Description], [Icon], [ImageUrl], [LinkUrl], [Value], [DisplayOrder], [IsVisible]) VALUES (4, 4, N'Giao đúng hẹn', N'Lịch trình rõ ràng, chủ động thông báo khi có thay đổi.', N'bi-clock-history', NULL, NULL, NULL, 1, 1)
INSERT [dbo].[HomeSectionItems] ([Id], [SectionId], [Title], [Description], [Icon], [ImageUrl], [LinkUrl], [Value], [DisplayOrder], [IsVisible]) VALUES (5, 4, N'Theo dõi minh bạch', N'Cập nhật trạng thái đơn hàng xuyên suốt hành trình.', N'bi-geo-alt', NULL, NULL, NULL, 2, 1)
INSERT [dbo].[HomeSectionItems] ([Id], [SectionId], [Title], [Description], [Icon], [ImageUrl], [LinkUrl], [Value], [DisplayOrder], [IsVisible]) VALUES (6, 4, N'Chi phí tối ưu', N'Báo giá rõ ràng, phương án phù hợp với ngân sách.', N'bi-cash-coin', NULL, NULL, NULL, 3, 1)
INSERT [dbo].[HomeSectionItems] ([Id], [SectionId], [Title], [Description], [Icon], [ImageUrl], [LinkUrl], [Value], [DisplayOrder], [IsVisible]) VALUES (7, 4, N'Hỗ trợ tận tâm', N'Đội ngũ tư vấn sẵn sàng đồng hành khi bạn cần.', N'bi-headset', NULL, NULL, NULL, 4, 1)
INSERT [dbo].[HomeSectionItems] ([Id], [SectionId], [Title], [Description], [Icon], [ImageUrl], [LinkUrl], [Value], [DisplayOrder], [IsVisible]) VALUES (8, 5, N'Vận chuyển hàng nội thất Bắc - Nam', N'Tuyến vận chuyển ổn định cho lô hàng cồng kềnh, có hỗ trợ bốc xếp tận nơi.', N'bi-truck', N'/images/Projects/van-chuyen-noi-that.jpg', NULL, NULL, 1, 1)
INSERT [dbo].[HomeSectionItems] ([Id], [SectionId], [Title], [Description], [Icon], [ImageUrl], [LinkUrl], [Value], [DisplayOrder], [IsVisible]) VALUES (9, 5, N'Phân phối hàng tiêu dùng cho chuỗi bán lẻ', N'Giao hàng đa điểm theo lịch cố định, đối soát rõ ràng từng cửa hàng.', N'bi-shop', N'/images/Projects/nha-phan-phoi.jpg', NULL, NULL, 2, 1)
INSERT [dbo].[HomeSectionItems] ([Id], [SectionId], [Title], [Description], [Icon], [ImageUrl], [LinkUrl], [Value], [DisplayOrder], [IsVisible]) VALUES (10, 5, N'Kho vận cho đơn vị thương mại điện tử', N'Lưu kho, đóng gói và giao COD, đồng bộ trạng thái đơn hàng.', N'bi-buildings', N'/images/Projects/Kho-thuong-mai-dien-tu-1.webp', NULL, NULL, 3, 1)
INSERT [dbo].[HomeSectionItems] ([Id], [SectionId], [Title], [Description], [Icon], [ImageUrl], [LinkUrl], [Value], [DisplayOrder], [IsVisible]) VALUES (13, 5, N'Vận chuyển hàng lạnh cho chuỗi siêu thị thực phẩm', N'Giữ nhiệt độ ổn định suốt hành trình cho rau củ, thực phẩm đông lạnh; giao đúng giờ tới từng điểm bán, không gián đoạn chuỗi lạnh.', N'bi-snow2', N'/images/Projects/Chuoi-cung-ung-lanh.jpg', NULL, NULL, 4, 1)
INSERT [dbo].[HomeSectionItems] ([Id], [SectionId], [Title], [Description], [Icon], [ImageUrl], [LinkUrl], [Value], [DisplayOrder], [IsVisible]) VALUES (14, 5, N'Vận chuyển thiết bị cho hội chợ, triển lãm', N'Hỗ trợ tháo lắp, đóng gói thiết bị cồng kềnh và vận chuyển đúng tiến độ dựng gian hàng, đảm bảo an toàn tuyệt đối cho thiết bị dễ hư hỏng.', N'bi-calendar2-event', N'/images/Projects/hoi-tro-trien-lam.jpg', NULL, NULL, 5, 1)
INSERT [dbo].[HomeSectionItems] ([Id], [SectionId], [Title], [Description], [Icon], [ImageUrl], [LinkUrl], [Value], [DisplayOrder], [IsVisible]) VALUES (15, 5, N'Vận chuyển vật liệu cho công trình xây dựng', N'Phối hợp lịch giao nhận theo tiến độ thi công, xe tải trọng lớn, hỗ trợ bốc xếp tận công trình ở cả khu vực nội thành và ngoại tỉnh.', N'bi-bricks', N'/images/Projects/cho-vlxd.png', NULL, NULL, 6, 1)
INSERT [dbo].[HomeSectionItems] ([Id], [SectionId], [Title], [Description], [Icon], [ImageUrl], [LinkUrl], [Value], [DisplayOrder], [IsVisible]) VALUES (16, 8, N'Trụ sở chính', N'123 Đường Logistics, Quận 1, TP. Hồ Chí Minh', N'bi-geo-alt', NULL, NULL, NULL, 1, 1)
INSERT [dbo].[HomeSectionItems] ([Id], [SectionId], [Title], [Description], [Icon], [ImageUrl], [LinkUrl], [Value], [DisplayOrder], [IsVisible]) VALUES (17, 8, N'Tổng đài hỗ trợ (24/7)', N'1900 6868', N'bi-telephone', NULL, NULL, NULL, 2, 1)
INSERT [dbo].[HomeSectionItems] ([Id], [SectionId], [Title], [Description], [Icon], [ImageUrl], [LinkUrl], [Value], [DisplayOrder], [IsVisible]) VALUES (18, 8, N'Email CSKH', N'support@lmsexpress.vn', N'bi-envelope', NULL, NULL, NULL, 3, 1)
SET IDENTITY_INSERT [dbo].[HomeSectionItems] OFF
GO
SET IDENTITY_INSERT [dbo].[HomeSections] ON 

INSERT [dbo].[HomeSections] ([Id], [SectionType], [Kicker], [Title], [Subtitle], [Body], [ButtonText], [ButtonUrl], [SecondaryButtonText], [SecondaryButtonUrl], [DisplayOrder], [IsVisible], [IsSystem]) VALUES (1, N'Hero', N'Giao đúng hẹn · Theo dõi 24/7', N'Kết nối mọi hành trình, giao trọn niềm tin', N'Giải pháp vận chuyển toàn diện dành cho cá nhân và doanh nghiệp. Nhanh chóng, minh bạch và tối ưu chi phí trên từng đơn hàng.', NULL, N'Khám phá dịch vụ', N'/Service', N'Xem quy trình', N'/Process', 1, 1, 1)
INSERT [dbo].[HomeSections] ([Id], [SectionType], [Kicker], [Title], [Subtitle], [Body], [ButtonText], [ButtonUrl], [SecondaryButtonText], [SecondaryButtonUrl], [DisplayOrder], [IsVisible], [IsSystem]) VALUES (2, N'Tracking', NULL, N'Tra cứu đơn hàng', N'Nhập mã vận đơn để xem hành trình', NULL, NULL, NULL, NULL, NULL, 3, 1, 1)
INSERT [dbo].[HomeSections] ([Id], [SectionType], [Kicker], [Title], [Subtitle], [Body], [ButtonText], [ButtonUrl], [SecondaryButtonText], [SecondaryButtonUrl], [DisplayOrder], [IsVisible], [IsSystem]) VALUES (3, N'ServiceIntro', N'DỊCH VỤ NỔI BẬT', N'Giải pháp phù hợp với mọi nhu cầu', N'Từ kiện hàng nhỏ đến nguyên chuyến, LMS Express luôn có phương án vận chuyển tối ưu cho bạn.', N'LMS Express cung cấp đa dạng dịch vụ vận chuyển, kho bãi và giao nhận cho cá nhân lẫn doanh nghiệp.', N'Xem tất cả dịch vụ', N'/Service', NULL, NULL, 4, 1, 1)
INSERT [dbo].[HomeSections] ([Id], [SectionType], [Kicker], [Title], [Subtitle], [Body], [ButtonText], [ButtonUrl], [SecondaryButtonText], [SecondaryButtonUrl], [DisplayOrder], [IsVisible], [IsSystem]) VALUES (4, N'WhyChooseUs', N'VÌ SAO CHỌN CHÚNG TÔI', N'Vì sao khách hàng tin chọn LMS Express', N'Những giá trị chúng tôi cam kết trên từng đơn hàng.', NULL, NULL, NULL, NULL, NULL, 5, 1, 1)
INSERT [dbo].[HomeSections] ([Id], [SectionType], [Kicker], [Title], [Subtitle], [Body], [ButtonText], [ButtonUrl], [SecondaryButtonText], [SecondaryButtonUrl], [DisplayOrder], [IsVisible], [IsSystem]) VALUES (5, N'Projects', N'DỰ ÁN TIÊU BIỂU', N'Những hành trình chúng tôi đã đồng hành', N'Một số dự án vận chuyển và phân phối tiêu biểu.', NULL, NULL, NULL, NULL, NULL, 6, 1, 1)
INSERT [dbo].[HomeSections] ([Id], [SectionType], [Kicker], [Title], [Subtitle], [Body], [ButtonText], [ButtonUrl], [SecondaryButtonText], [SecondaryButtonUrl], [DisplayOrder], [IsVisible], [IsSystem]) VALUES (6, N'Process', N'QUY TRÌNH MINH BẠCH', N'7 bước để hàng đến nơi an toàn', N'Mỗi kiện hàng đều được kiểm soát xuyên suốt từ lúc tiếp nhận đến khi giao thành công.', NULL, N'Xem toàn bộ quy trình', N'/Process', NULL, NULL, 7, 1, 1)
INSERT [dbo].[HomeSections] ([Id], [SectionType], [Kicker], [Title], [Subtitle], [Body], [ButtonText], [ButtonUrl], [SecondaryButtonText], [SecondaryButtonUrl], [DisplayOrder], [IsVisible], [IsSystem]) VALUES (7, N'News', N'TIN TỨC', N'Tin tức & kinh nghiệm logistics', N'Cập nhật xu hướng và mẹo hữu ích cho việc vận chuyển hàng hóa.', NULL, N'Xem tất cả tin tức', N'/News', NULL, NULL, 8, 1, 1)
INSERT [dbo].[HomeSections] ([Id], [SectionType], [Kicker], [Title], [Subtitle], [Body], [ButtonText], [ButtonUrl], [SecondaryButtonText], [SecondaryButtonUrl], [DisplayOrder], [IsVisible], [IsSystem]) VALUES (8, N'Contact', N'LIÊN HỆ', N'Để lại thông tin, chúng tôi sẽ liên hệ lại', N'Chuyên viên LMS Express sẵn sàng tư vấn miễn phí.', NULL, NULL, NULL, NULL, NULL, 10, 1, 1)
INSERT [dbo].[HomeSections] ([Id], [SectionType], [Kicker], [Title], [Subtitle], [Body], [ButtonText], [ButtonUrl], [SecondaryButtonText], [SecondaryButtonUrl], [DisplayOrder], [IsVisible], [IsSystem]) VALUES (9, N'Cta', N'CAM KẾT CỦA CHÚNG TÔI', N'Mọi chuyến hàng, một lời hứa.', N'LMS Express — vận chuyển an toàn, đúng giờ, trên từng cây số.', NULL, N'Nhận báo giá ngay', N'/Service', N'Gọi hotline 1900 6868', N'tel:19006868', 11, 1, 1)
INSERT [dbo].[HomeSections] ([Id], [SectionType], [Kicker], [Title], [Subtitle], [Body], [ButtonText], [ButtonUrl], [SecondaryButtonText], [SecondaryButtonUrl], [DisplayOrder], [IsVisible], [IsSystem]) VALUES (11, N'Partners', NULL, N'Đối tác đồng hành', N'Những doanh nghiệp tin tưởng LMS Express.', NULL, NULL, NULL, NULL, NULL, 9, 0, 1)
INSERT [dbo].[HomeSections] ([Id], [SectionType], [Kicker], [Title], [Subtitle], [Body], [ButtonText], [ButtonUrl], [SecondaryButtonText], [SecondaryButtonUrl], [DisplayOrder], [IsVisible], [IsSystem]) VALUES (12, N'Banner', NULL, N'Banner quảng cáo', NULL, NULL, NULL, NULL, NULL, NULL, 2, 0, 1)
SET IDENTITY_INSERT [dbo].[HomeSections] OFF
GO
SET IDENTITY_INSERT [dbo].[MediaItems] ON 

INSERT [dbo].[MediaItems] ([Id], [Type], [Title], [ImageUrl], [LinkUrl], [DisplayOrder], [IsVisible], [CreatedAt]) VALUES (5, N'Partner', N'a', N'/uploads/media/24b0c877401247aea4ec1a69a89b32df.jpg', NULL, 0, 1, CAST(N'2026-10-11T02:47:28.0536319' AS DateTime2))
INSERT [dbo].[MediaItems] ([Id], [Type], [Title], [ImageUrl], [LinkUrl], [DisplayOrder], [IsVisible], [CreatedAt]) VALUES (6, N'Partner', N'asa', N'/uploads/media/21ba163c57f6452d9e36db6731098cda.jpg', NULL, 1, 1, CAST(N'2026-10-11T02:47:45.5322879' AS DateTime2))
INSERT [dbo].[MediaItems] ([Id], [Type], [Title], [ImageUrl], [LinkUrl], [DisplayOrder], [IsVisible], [CreatedAt]) VALUES (7, N'Partner', N'a', N'/uploads/media/38a6710faa9a4441b27c4130c6516757.jpg', NULL, 0, 1, CAST(N'2026-10-11T02:48:01.0295617' AS DateTime2))
INSERT [dbo].[MediaItems] ([Id], [Type], [Title], [ImageUrl], [LinkUrl], [DisplayOrder], [IsVisible], [CreatedAt]) VALUES (8, N'Partner', N'a', N'/uploads/media/000d9744ce214694baf0cdb7e0930ae3.jpg', NULL, 0, 1, CAST(N'2026-10-11T02:48:10.6947221' AS DateTime2))
INSERT [dbo].[MediaItems] ([Id], [Type], [Title], [ImageUrl], [LinkUrl], [DisplayOrder], [IsVisible], [CreatedAt]) VALUES (9, N'Partner', N'a', N'/uploads/media/9e1dc3cefbc0440a981db916907665fc.jpg', NULL, 0, 1, CAST(N'2026-10-11T02:48:21.3359939' AS DateTime2))
INSERT [dbo].[MediaItems] ([Id], [Type], [Title], [ImageUrl], [LinkUrl], [DisplayOrder], [IsVisible], [CreatedAt]) VALUES (10, N'Partner', N'a', N'/uploads/media/97c03138e0a14f62ba53f71ffb297b3e.jpg', NULL, 0, 1, CAST(N'2026-10-11T02:48:31.7372235' AS DateTime2))
INSERT [dbo].[MediaItems] ([Id], [Type], [Title], [ImageUrl], [LinkUrl], [DisplayOrder], [IsVisible], [CreatedAt]) VALUES (11, N'Partner', N'a', N'/uploads/media/818b340df2744f9a8e92c26bba8bd2b0.jpg', NULL, 0, 1, CAST(N'2026-10-11T02:48:53.3085020' AS DateTime2))
SET IDENTITY_INSERT [dbo].[MediaItems] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderEvents] ON 

INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (14, 3, 1, N'Đã tiếp nhận yêu cầu báo giá', N'Chuyên viên sẽ liên hệ trong thời gian sớm nhất.', CAST(N'2026-10-05T15:46:54.7283359' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (15, 3, 2, N'Đã gửi email xác nhận', N'Gửi tới dot01366@gmail.com', CAST(N'2026-10-05T15:47:00.6693398' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (16, 3, 3, N'Phân công xe / tài xế', N'Xe: 68Ma-2000 · Tài xế: Đỗ Duy Tấn', CAST(N'2026-10-05T15:48:36.5343871' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (17, 3, 4, N'Đã gửi báo giá', N'Hiệu lực đến hết ngày 12/10/2026', CAST(N'2026-10-05T16:18:32.0649483' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (18, 3, 5, N'Đã gửi email báo giá', NULL, CAST(N'2026-10-05T16:18:36.7972329' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (19, 3, 6, N'Khách đồng ý báo giá', N'Qua email', CAST(N'2026-10-05T16:19:19.4151012' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (20, 3, 7, N'Đã gửi email thông báo', NULL, CAST(N'2026-10-05T16:19:23.8955450' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (21, 3, 8, N'Đang vận chuyển', NULL, CAST(N'2026-10-05T16:19:56.2279656' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (22, 3, 9, N'Đã gửi email thông báo', NULL, CAST(N'2026-10-05T16:20:01.3601374' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (23, 3, 10, N'Đã giao', NULL, CAST(N'2026-10-05T16:20:26.6366221' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (24, 3, 11, N'Đã gửi email thông báo', NULL, CAST(N'2026-10-05T16:20:30.6451511' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (25, 4, 1, N'Đã tiếp nhận yêu cầu báo giá', N'Chuyên viên sẽ liên hệ trong thời gian sớm nhất.', CAST(N'2026-10-05T17:04:42.5280461' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (26, 4, 2, N'Đã gửi email xác nhận', NULL, CAST(N'2026-10-05T17:04:49.6557766' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (27, 4, 3, N'Đã gửi báo giá', N'Hiệu lực đến hết ngày 12/10/2026', CAST(N'2026-10-05T17:05:17.3610273' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (28, 4, 4, N'Đã gửi email báo giá', NULL, CAST(N'2026-10-05T17:05:24.4511272' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (29, 4, 5, N'Khách đồng ý báo giá', N'Nhân viên ghi nhận qua điện thoại / trao đổi trực tiếp', CAST(N'2026-10-05T17:05:34.8576144' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (30, 4, 6, N'Đã gửi email thông báo', NULL, CAST(N'2026-10-05T17:05:41.4512654' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (31, 5, 1, N'Đã tiếp nhận yêu cầu báo giá', N'Chuyên viên sẽ liên hệ trong thời gian sớm nhất.', CAST(N'2026-10-05T17:06:37.5504967' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (32, 5, 2, N'Đã gửi email xác nhận', NULL, CAST(N'2026-10-05T17:06:45.1409145' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (33, 4, 7, N'Đã phân công xe và tài xế', NULL, CAST(N'2026-10-05T17:08:32.7081416' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (34, 4, 8, N'Đang vận chuyển', NULL, CAST(N'2026-10-05T17:08:36.2340482' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (35, 4, 9, N'Đã gửi email thông báo', NULL, CAST(N'2026-10-05T17:08:43.1032178' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (36, 4, 10, N'Đã giao', NULL, CAST(N'2026-10-05T17:08:47.9250416' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (37, 4, 11, N'Đã gửi email thông báo', NULL, CAST(N'2026-10-05T17:08:55.2890604' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (38, 5, 3, N'Đã gửi báo giá', N'Hiệu lực đến hết ngày 12/10/2026', CAST(N'2026-10-05T17:09:36.6879072' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (39, 5, 4, N'Đã gửi email báo giá', NULL, CAST(N'2026-10-05T17:09:42.8501774' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (40, 5, 5, N'Khách đồng ý báo giá', N'Nhân viên ghi nhận qua điện thoại / trao đổi trực tiếp', CAST(N'2026-10-05T17:09:54.2346555' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (41, 5, 6, N'Đã gửi email thông báo', NULL, CAST(N'2026-10-05T17:10:01.7452490' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (42, 5, 7, N'Đã phân công xe và tài xế', NULL, CAST(N'2026-10-05T17:10:06.3415625' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (43, 5, 8, N'Đang vận chuyển', NULL, CAST(N'2026-10-05T17:10:07.2984427' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (44, 5, 9, N'Đã gửi email thông báo', NULL, CAST(N'2026-10-05T17:10:13.9129579' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (45, 5, 10, N'Đã giao', NULL, CAST(N'2026-10-05T17:10:14.5778161' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (46, 5, 11, N'Đã gửi email thông báo', NULL, CAST(N'2026-10-05T17:10:20.5255471' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (53, 7, 1, N'Đã tiếp nhận yêu cầu báo giá', N'Chuyên viên sẽ liên hệ trong thời gian sớm nhất.', CAST(N'2026-10-10T16:02:36.3513362' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (54, 7, 2, N'Đã gửi email xác nhận', NULL, CAST(N'2026-10-10T16:02:41.1554043' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (57, 7, 3, N'Đã gửi báo giá', N'Hiệu lực đến hết ngày 17/10/2026', CAST(N'2026-10-10T16:05:04.4373216' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (58, 7, 4, N'Đã gửi email báo giá', NULL, CAST(N'2026-10-10T16:05:10.1367153' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (59, 7, 5, N'Khách đồng ý báo giá', N'Trong tài khoản', CAST(N'2026-10-10T16:35:19.5317548' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (60, 7, 6, N'Đã gửi email thông báo', NULL, CAST(N'2026-10-10T16:35:25.0533614' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (62, 7, 7, N'Đã phân công xe và tài xế', NULL, CAST(N'2026-10-12T13:11:45.6558977' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (81, 13, 1, N'Đã tiếp nhận yêu cầu báo giá', N'Chuyên viên sẽ liên hệ trong thời gian sớm nhất.', CAST(N'2026-10-14T15:31:39.3951797' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (82, 13, 2, N'Đã gửi email xác nhận', NULL, CAST(N'2026-10-14T15:32:00.3561613' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (83, 13, 3, N'Đã gửi báo giá', N'Hiệu lực đến hết ngày 21/10/2026', CAST(N'2026-10-14T15:33:12.3504085' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (84, 13, 4, N'Đã gửi email báo giá', NULL, CAST(N'2026-10-14T15:33:24.7909002' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (85, 13, 5, N'Khách đồng ý báo giá', N'Trong tài khoản', CAST(N'2026-10-14T15:33:46.5896528' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (86, 13, 6, N'Đã phân công xe và tài xế', NULL, CAST(N'2026-10-14T15:35:35.0636718' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (87, 13, 7, N'Đang vận chuyển', NULL, CAST(N'2026-10-14T15:35:45.3124449' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (88, 13, 8, N'Đã gửi email thông báo', NULL, CAST(N'2026-10-14T15:35:53.8851601' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (89, 13, 9, N'Đã đến kho bình dương', NULL, CAST(N'2026-10-14T15:36:17.1310877' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (90, 13, 10, N'Đã giao', NULL, CAST(N'2026-10-14T15:36:22.6353025' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (109, 17, 1, N'Đã tiếp nhận yêu cầu báo giá', N'Chuyên viên sẽ liên hệ trong thời gian sớm nhất.', CAST(N'2026-10-31T16:04:34.9830140' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (110, 17, 2, N'Đã gửi email xác nhận', NULL, CAST(N'2026-10-31T16:04:40.1324967' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (111, 17, 3, N'Đã gửi báo giá', N'Hiệu lực đến hết ngày 07/11/2026', CAST(N'2026-10-31T16:05:11.3700759' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (112, 17, 4, N'Đã gửi email báo giá', NULL, CAST(N'2026-10-31T16:05:17.1553925' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (113, 17, 5, N'Khách đồng ý báo giá', N'Nhân viên ghi nhận qua điện thoại / trao đổi trực tiếp', CAST(N'2026-10-31T16:05:53.8163726' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (114, 17, 6, N'Đã gửi email thông báo', NULL, CAST(N'2026-10-31T16:05:58.9810919' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (115, 17, 7, N'Đã phân công xe và tài xế', NULL, CAST(N'2026-10-31T16:06:31.0774370' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (116, 17, 8, N'Đã nhận hàng', NULL, CAST(N'2026-11-01T02:46:01.1327794' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (117, 17, 9, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-01T02:46:06.8947116' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (118, 17, 10, N'Đang vận chuyển', NULL, CAST(N'2026-11-01T02:46:11.2337781' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (119, 17, 11, N'Đang giao hàng', NULL, CAST(N'2026-11-01T02:46:13.1735188' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (120, 17, 12, N'Đã giao', NULL, CAST(N'2026-11-01T02:46:15.8023590' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (121, 17, 13, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-01T02:46:21.5340207' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (122, 17, 14, N'Hoàn thành', NULL, CAST(N'2026-11-01T02:46:23.2084693' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (125, 3, 12, N'Hoàn thành', NULL, CAST(N'2026-11-01T15:08:51.6307076' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (138, 18, 1, N'Đã tiếp nhận yêu cầu báo giá', N'Chuyên viên sẽ liên hệ trong thời gian sớm nhất.', CAST(N'2026-11-01T15:42:19.3620030' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (139, 18, 2, N'Đã gửi email xác nhận', NULL, CAST(N'2026-11-01T15:42:23.3652815' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (140, 18, 3, N'Đã gửi báo giá', N'Hiệu lực đến hết ngày 08/11/2026', CAST(N'2026-11-01T15:43:02.7353032' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (141, 18, 4, N'Đã gửi email báo giá', NULL, CAST(N'2026-11-01T15:43:07.2944959' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (142, 18, 5, N'Khách đồng ý báo giá', N'Nhân viên ghi nhận qua điện thoại / trao đổi trực tiếp', CAST(N'2026-11-01T15:43:08.8729256' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (143, 18, 6, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-01T15:43:12.9840132' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (144, 18, 7, N'Đã phân công xe và tài xế', NULL, CAST(N'2026-11-01T15:52:29.6260106' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (145, 18, 8, N'Đã nhận hàng', NULL, CAST(N'2026-11-01T15:53:22.9994766' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (146, 18, 9, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-01T15:53:28.3505477' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (147, 18, 10, N'Đang vận chuyển', NULL, CAST(N'2026-11-01T15:53:37.4737398' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (148, 18, 11, N'Đang giao hàng', NULL, CAST(N'2026-11-01T15:53:40.3335188' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (149, 18, 12, N'Đã giao', NULL, CAST(N'2026-11-01T15:53:45.8589054' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (150, 18, 13, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-01T15:53:51.0728150' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (151, 18, 14, N'Hoàn thành', NULL, CAST(N'2026-11-01T15:53:53.1263781' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (152, 7, 8, N'Đã nhận hàng', NULL, CAST(N'2026-11-01T15:54:07.8275905' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (153, 7, 9, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-01T15:54:13.1922908' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (154, 7, 10, N'Đang vận chuyển', NULL, CAST(N'2026-11-01T15:54:46.2373731' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (155, 7, 11, N'Đang giao hàng', NULL, CAST(N'2026-11-01T15:54:52.8538311' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (156, 7, 12, N'Đã giao', NULL, CAST(N'2026-11-01T15:54:59.5546128' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (157, 7, 13, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-01T15:55:04.2915168' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (158, 7, 14, N'Hoàn thành', NULL, CAST(N'2026-11-01T15:55:07.8727393' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (159, 19, 1, N'Đã tiếp nhận yêu cầu báo giá', N'Chuyên viên sẽ liên hệ trong thời gian sớm nhất.', CAST(N'2026-11-01T15:57:34.1390015' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (160, 19, 2, N'Đã gửi email xác nhận', NULL, CAST(N'2026-11-01T15:57:39.8805069' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (161, 20, 1, N'Đã tiếp nhận yêu cầu báo giá', N'Chuyên viên sẽ liên hệ trong thời gian sớm nhất.', CAST(N'2026-11-01T15:58:56.9971104' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (162, 20, 2, N'Đã gửi email xác nhận', NULL, CAST(N'2026-11-01T15:59:01.4472784' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (163, 20, 3, N'Đã gửi báo giá', N'Hiệu lực đến hết ngày 08/11/2026', CAST(N'2026-11-01T15:59:32.7141258' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (164, 20, 4, N'Đã gửi email báo giá', NULL, CAST(N'2026-11-01T15:59:37.6964684' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (165, 20, 5, N'Khách đồng ý báo giá', N'Nhân viên ghi nhận qua điện thoại / trao đổi trực tiếp', CAST(N'2026-11-01T15:59:48.1031547' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (166, 20, 6, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-01T15:59:53.8772821' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (167, 19, 3, N'Đã gửi báo giá', N'Hiệu lực đến hết ngày 08/11/2026', CAST(N'2026-11-01T16:00:46.3512211' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (168, 19, 4, N'Đã gửi email báo giá', NULL, CAST(N'2026-11-01T16:00:52.4599992' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (169, 19, 5, N'Khách đồng ý báo giá', N'Nhân viên ghi nhận qua điện thoại / trao đổi trực tiếp', CAST(N'2026-11-01T16:00:53.5987743' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (170, 19, 6, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-01T16:00:58.1798936' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (171, 19, 7, N'Đã phân công xe và tài xế', NULL, CAST(N'2026-11-01T16:57:59.0974437' AS DateTime2), 1)
GO
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (172, 19, 8, N'Đã nhận hàng', N'Chuyến vận chuyển bắt đầu chạy', CAST(N'2026-11-01T16:58:45.9266317' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (173, 19, 9, N'Đang vận chuyển', N'Chuyến vận chuyển bắt đầu chạy', CAST(N'2026-11-01T16:58:45.9266336' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (174, 19, 10, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-01T16:58:51.7558674' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (175, 19, 11, N'Đang giao hàng', NULL, CAST(N'2026-11-01T16:58:58.1589041' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (176, 19, 12, N'Đã giao', NULL, CAST(N'2026-11-01T16:58:58.9634906' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (177, 19, 13, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-01T16:59:03.4255169' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (178, 19, 14, N'Hoàn thành', N'Chuyến vận chuyển đã hoàn thành', CAST(N'2026-11-01T16:59:10.3059130' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (179, 19, 15, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-01T16:59:14.7921280' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (180, 20, 7, N'Đã phân công xe và tài xế', NULL, CAST(N'2026-11-01T17:06:12.3261184' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (181, 20, 8, N'Đã nhận hàng', N'Chuyến vận chuyển bắt đầu chạy', CAST(N'2026-11-02T11:06:31.7672964' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (182, 20, 9, N'Đang vận chuyển', N'Chuyến vận chuyển bắt đầu chạy', CAST(N'2026-11-02T11:06:31.7674501' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (183, 20, 10, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-02T11:06:38.3853404' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (184, 20, 11, N'Hoàn hàng', N'a', CAST(N'2026-11-02T11:07:15.3301740' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (185, 20, 12, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-02T11:07:20.3644648' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (186, 21, 1, N'Đã tiếp nhận yêu cầu báo giá', N'Chuyên viên sẽ liên hệ trong thời gian sớm nhất.', CAST(N'2026-11-02T14:01:13.3518007' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (187, 21, 2, N'Đã gửi email xác nhận', NULL, CAST(N'2026-11-02T14:01:19.7222251' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (188, 22, 1, N'Đã tiếp nhận yêu cầu báo giá', N'Chuyên viên sẽ liên hệ trong thời gian sớm nhất.', CAST(N'2026-11-02T14:02:59.7915900' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (189, 22, 2, N'Đã gửi email xác nhận', NULL, CAST(N'2026-11-02T14:03:04.8905004' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (190, 22, 3, N'Đã gửi báo giá', N'Hiệu lực đến hết ngày 09/11/2026', CAST(N'2026-11-02T14:03:59.4045265' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (191, 22, 4, N'Đã gửi email báo giá', NULL, CAST(N'2026-11-02T14:04:03.6884622' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (192, 21, 3, N'Đã gửi báo giá', N'Hiệu lực đến hết ngày 09/11/2026', CAST(N'2026-11-02T14:04:10.0445681' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (193, 21, 4, N'Đã gửi email báo giá', NULL, CAST(N'2026-11-02T14:04:14.5090398' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (194, 22, 5, N'Khách đồng ý báo giá', N'Trong tài khoản', CAST(N'2026-11-02T14:05:07.2777691' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (195, 22, 6, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-02T14:05:11.8634241' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (196, 21, 5, N'Khách đồng ý báo giá', N'Trong tài khoản', CAST(N'2026-11-02T14:05:22.4896798' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (197, 21, 6, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-02T14:05:26.6569627' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (198, 21, 7, N'Đã phân công xe và tài xế', NULL, CAST(N'2026-11-02T14:12:28.0895586' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (199, 21, 8, N'Đã phân công xe và tài xế', NULL, CAST(N'2026-11-02T14:12:46.9682929' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (200, 21, 9, N'Đã nhận hàng', N'Chuyến vận chuyển bắt đầu chạy', CAST(N'2026-11-02T14:13:21.7382724' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (201, 21, 10, N'Đang vận chuyển', N'Chuyến vận chuyển bắt đầu chạy', CAST(N'2026-11-02T14:13:21.7382750' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (202, 21, 11, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-02T14:13:26.0330370' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (203, 21, 12, N'Đang giao hàng', NULL, CAST(N'2026-11-02T14:13:35.7349820' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (204, 21, 13, N'Đã giao', NULL, CAST(N'2026-11-02T14:13:37.3470682' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (205, 21, 14, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-02T14:13:42.0079763' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (206, 21, 15, N'Hoàn thành', N'Chuyến vận chuyển đã hoàn thành', CAST(N'2026-11-02T14:13:59.3916578' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (207, 21, 16, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-02T14:14:04.1257934' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (208, 23, 1, N'Đã tiếp nhận yêu cầu báo giá', N'Chuyên viên sẽ liên hệ trong thời gian sớm nhất.', CAST(N'2026-11-02T14:15:53.9010290' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (209, 23, 2, N'Đã gửi email xác nhận', NULL, CAST(N'2026-11-02T14:15:58.7096395' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (210, 23, 3, N'Đã gửi báo giá', N'Hiệu lực đến hết ngày 09/11/2026', CAST(N'2026-11-02T14:16:27.0630903' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (211, 23, 4, N'Đã gửi email báo giá', NULL, CAST(N'2026-11-02T14:16:31.4787635' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (212, 23, 5, N'Khách đồng ý báo giá', N'Nhân viên ghi nhận qua điện thoại / trao đổi trực tiếp', CAST(N'2026-11-02T14:16:35.9930746' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (213, 23, 6, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-02T14:16:41.5134692' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (214, 22, 7, N'Đã phân công xe và tài xế', NULL, CAST(N'2026-11-02T14:17:10.0426093' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (215, 22, 8, N'Đã nhận hàng', N'Chuyến vận chuyển bắt đầu chạy', CAST(N'2026-11-02T14:17:15.4601240' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (216, 22, 9, N'Đang vận chuyển', N'Chuyến vận chuyển bắt đầu chạy', CAST(N'2026-11-02T14:17:15.4601259' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (217, 22, 10, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-02T14:17:19.7123246' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (218, 24, 1, N'Đã tiếp nhận yêu cầu báo giá', N'Chuyên viên sẽ liên hệ trong thời gian sớm nhất.', CAST(N'2026-11-02T14:19:59.5351156' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (219, 24, 2, N'Đã gửi email xác nhận', NULL, CAST(N'2026-11-02T14:20:03.8544296' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (220, 25, 1, N'Đã tiếp nhận yêu cầu báo giá', N'Chuyên viên sẽ liên hệ trong thời gian sớm nhất.', CAST(N'2026-11-02T14:20:37.1157107' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (221, 25, 2, N'Đã gửi email xác nhận', NULL, CAST(N'2026-11-02T14:20:41.5399233' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (222, 25, 3, N'Đã gửi báo giá', N'Hiệu lực đến hết ngày 09/11/2026', CAST(N'2026-11-02T14:20:58.2378651' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (223, 25, 4, N'Đã gửi email báo giá', NULL, CAST(N'2026-11-02T14:21:02.4290292' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (224, 25, 5, N'Khách đồng ý báo giá', N'Nhân viên ghi nhận qua điện thoại / trao đổi trực tiếp', CAST(N'2026-11-02T14:21:08.9299935' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (225, 25, 6, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-02T14:21:13.1795461' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (226, 24, 3, N'Đã gửi báo giá', N'Hiệu lực đến hết ngày 09/11/2026', CAST(N'2026-11-02T14:21:21.0561558' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (227, 24, 4, N'Đã gửi email báo giá', NULL, CAST(N'2026-11-02T14:21:25.4671741' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (228, 24, 5, N'Khách đồng ý báo giá', N'Nhân viên ghi nhận qua điện thoại / trao đổi trực tiếp', CAST(N'2026-11-02T14:21:26.6985219' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (229, 24, 6, N'Đã gửi email thông báo', NULL, CAST(N'2026-11-02T14:21:31.9307996' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (230, 24, 7, N'Đã phân công xe và tài xế', NULL, CAST(N'2026-11-02T14:22:28.5258936' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (231, 25, 7, N'Đã phân công xe và tài xế', NULL, CAST(N'2026-11-02T14:22:28.5261227' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (232, 25, 8, N'Đã gỡ khỏi chuyến vận chuyển', NULL, CAST(N'2026-11-02T14:24:05.3414086' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (233, 24, 8, N'Đã nhận hàng', N'Chuyến vận chuyển bắt đầu chạy', CAST(N'2026-11-02T14:26:33.7322317' AS DateTime2), 1)
INSERT [dbo].[OrderEvents] ([Id], [OrderId], [SequenceNumber], [Title], [Detail], [EventAt], [IsCompleted]) VALUES (234, 24, 9, N'Đang vận chuyển', N'Chuyến vận chuyển bắt đầu chạy', CAST(N'2026-11-02T14:26:33.7322336' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[OrderEvents] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderItems] ON 

INSERT [dbo].[OrderItems] ([Id], [OrderId], [CargoTypeId], [CargoTypeName], [Quantity], [WeightKg], [IsFragile], [Note]) VALUES (1, 3, 1, N'Hàng tiêu dùng', 4, CAST(500.00 AS Decimal(18, 2)), 0, NULL)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [CargoTypeId], [CargoTypeName], [Quantity], [WeightKg], [IsFragile], [Note]) VALUES (2, 4, 1, N'Hàng tiêu dùng', 10, CAST(500.00 AS Decimal(18, 2)), 0, NULL)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [CargoTypeId], [CargoTypeName], [Quantity], [WeightKg], [IsFragile], [Note]) VALUES (3, 5, 3, N'Nội thất', 10, CAST(555.00 AS Decimal(18, 2)), 0, NULL)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [CargoTypeId], [CargoTypeName], [Quantity], [WeightKg], [IsFragile], [Note]) VALUES (5, 7, 1, N'Hàng tiêu dùng', 48, CAST(50000.00 AS Decimal(18, 2)), 0, NULL)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [CargoTypeId], [CargoTypeName], [Quantity], [WeightKg], [IsFragile], [Note]) VALUES (11, 13, 3, N'Nội thất', 120, CAST(1000.00 AS Decimal(18, 2)), 0, NULL)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [CargoTypeId], [CargoTypeName], [Quantity], [WeightKg], [IsFragile], [Note]) VALUES (16, 17, 1, N'Hàng tiêu dùng', 1, CAST(10.00 AS Decimal(18, 2)), 0, NULL)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [CargoTypeId], [CargoTypeName], [Quantity], [WeightKg], [IsFragile], [Note]) VALUES (17, 18, 1, N'Hàng tiêu dùng', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [CargoTypeId], [CargoTypeName], [Quantity], [WeightKg], [IsFragile], [Note]) VALUES (18, 19, 1, N'Hàng tiêu dùng', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [CargoTypeId], [CargoTypeName], [Quantity], [WeightKg], [IsFragile], [Note]) VALUES (19, 20, 1, N'Hàng tiêu dùng', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [CargoTypeId], [CargoTypeName], [Quantity], [WeightKg], [IsFragile], [Note]) VALUES (20, 21, 3, N'Nội thất', 1, CAST(10.70 AS Decimal(18, 2)), 0, NULL)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [CargoTypeId], [CargoTypeName], [Quantity], [WeightKg], [IsFragile], [Note]) VALUES (21, 22, 6, N'Hàng dễ vỡ', 1, CAST(13.00 AS Decimal(18, 2)), 0, NULL)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [CargoTypeId], [CargoTypeName], [Quantity], [WeightKg], [IsFragile], [Note]) VALUES (22, 23, 1, N'Hàng tiêu dùng', 11, CAST(22.00 AS Decimal(18, 2)), 0, NULL)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [CargoTypeId], [CargoTypeName], [Quantity], [WeightKg], [IsFragile], [Note]) VALUES (23, 24, 1, N'Hàng tiêu dùng', 10, CAST(100.00 AS Decimal(18, 2)), 0, NULL)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [CargoTypeId], [CargoTypeName], [Quantity], [WeightKg], [IsFragile], [Note]) VALUES (24, 25, 2, N'Thiết bị điện tử', 100, CAST(1000.00 AS Decimal(18, 2)), 0, NULL)
SET IDENTITY_INSERT [dbo].[OrderItems] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [OrderCode], [CustomerId], [CustomerName], [Phone], [Email], [ServiceId], [VehicleId], [DriverId], [PickupAddress], [DeliveryAddress], [CargoType], [WeightKg], [PackageCount], [ExpectedShipDate], [Status], [CreatedAt], [Note], [QuoteNote], [QuoteToken], [QuoteValidUntil], [QuotedPrice], [ReceiverName], [ReceiverPhone]) VALUES (3, N'LMS20261005-6869', NULL, N'Đỗ Duy Tấn', N'0563426487', N'dot01366@gmail.com', 2, 1, 1, N'Phú Quốc', N'hn', N'Hàng tiêu dùng', CAST(500.00 AS Decimal(18, 2)), 4, CAST(N'2026-10-22T00:00:00.0000000' AS DateTime2), N'Đã giao', CAST(N'2026-10-05T15:46:54.7278132' AS DateTime2), NULL, N'dad', N'5b2f55f4f3caab22e548fae0b9b3386d', CAST(N'2026-10-12T23:59:59.0000000' AS DateTime2), CAST(100000000 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[Orders] ([Id], [OrderCode], [CustomerId], [CustomerName], [Phone], [Email], [ServiceId], [VehicleId], [DriverId], [PickupAddress], [DeliveryAddress], [CargoType], [WeightKg], [PackageCount], [ExpectedShipDate], [Status], [CreatedAt], [Note], [QuoteNote], [QuoteToken], [QuoteValidUntil], [QuotedPrice], [ReceiverName], [ReceiverPhone]) VALUES (4, N'LMS20261005-7194', NULL, N'Đỗ Duy Tấn', N'0968320717', N'dot01366@gmail.com', 4, 1, 1, N'Phú Quốc', N'hn', N'Hàng tiêu dùng', CAST(500.00 AS Decimal(18, 2)), 10, CAST(N'2026-11-05T00:00:00.0000000' AS DateTime2), N'Đã giao', CAST(N'2026-10-05T17:04:42.5231951' AS DateTime2), NULL, NULL, N'a3a1400d0c3d86a36d2e113edf646174', CAST(N'2026-10-12T23:59:59.0000000' AS DateTime2), CAST(10000000 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[Orders] ([Id], [OrderCode], [CustomerId], [CustomerName], [Phone], [Email], [ServiceId], [VehicleId], [DriverId], [PickupAddress], [DeliveryAddress], [CargoType], [WeightKg], [PackageCount], [ExpectedShipDate], [Status], [CreatedAt], [Note], [QuoteNote], [QuoteToken], [QuoteValidUntil], [QuotedPrice], [ReceiverName], [ReceiverPhone]) VALUES (5, N'LMS20261005-8648', 2, N'Tấn Đỗ', N'0968320717', N'dot01366@gmail.com', 3, 1, 1, N'Phú Quốc', N'hn', N'Nội thất', CAST(555.00 AS Decimal(18, 2)), 10, CAST(N'2026-10-07T00:00:00.0000000' AS DateTime2), N'Đã giao', CAST(N'2026-10-05T17:06:37.5504946' AS DateTime2), NULL, NULL, N'dc9704aef8f683d0fb661e707a803c25', CAST(N'2026-10-12T23:59:59.0000000' AS DateTime2), CAST(500000000 AS Decimal(18, 0)), NULL, NULL)
INSERT [dbo].[Orders] ([Id], [OrderCode], [CustomerId], [CustomerName], [Phone], [Email], [ServiceId], [VehicleId], [DriverId], [PickupAddress], [DeliveryAddress], [CargoType], [WeightKg], [PackageCount], [ExpectedShipDate], [Status], [CreatedAt], [Note], [QuoteNote], [QuoteToken], [QuoteValidUntil], [QuotedPrice], [ReceiverName], [ReceiverPhone]) VALUES (7, N'LMS20261010-6282', 2, N'Đỗ Duy Tấn', N'0968320717', N'dot01366@gmail.com', 4, 1, 1, N'sấdasdasdasdasd', N'Cổng sau ký túc xá khu B DHQG', N'Hàng tiêu dùng', CAST(50000.00 AS Decimal(18, 2)), 48, CAST(N'2026-10-22T00:00:00.0000000' AS DateTime2), N'Đã giao', CAST(N'2026-10-10T16:02:36.3509235' AS DateTime2), NULL, NULL, N'a6cd1425ca59dfb8a952db4470f41842', CAST(N'2026-10-17T23:59:59.0000000' AS DateTime2), CAST(5000 AS Decimal(18, 0)), N'Đỗ Duy Tấn', N'0563426487')
INSERT [dbo].[Orders] ([Id], [OrderCode], [CustomerId], [CustomerName], [Phone], [Email], [ServiceId], [VehicleId], [DriverId], [PickupAddress], [DeliveryAddress], [CargoType], [WeightKg], [PackageCount], [ExpectedShipDate], [Status], [CreatedAt], [Note], [QuoteNote], [QuoteToken], [QuoteValidUntil], [QuotedPrice], [ReceiverName], [ReceiverPhone]) VALUES (13, N'LMS20261014-7058', 2, N'Đỗ Duy Tấn', N'0968320717', N'dot01366@gmail.com', 4, 1, 1, N'Phú Quốc', N'Cổng sau ký túc xá khu B DHQG', N'Nội thất', CAST(1000.00 AS Decimal(18, 2)), 120, CAST(N'2026-10-22T00:00:00.0000000' AS DateTime2), N'Đã giao', CAST(N'2026-10-14T15:31:39.3948738' AS DateTime2), NULL, NULL, N'd89eb37011c871cbeb23bc1b82b74cab', CAST(N'2026-10-21T23:59:59.0000000' AS DateTime2), CAST(50000000 AS Decimal(18, 0)), N'Đỗ Duy Tấn', N'0563426487')
INSERT [dbo].[Orders] ([Id], [OrderCode], [CustomerId], [CustomerName], [Phone], [Email], [ServiceId], [VehicleId], [DriverId], [PickupAddress], [DeliveryAddress], [CargoType], [WeightKg], [PackageCount], [ExpectedShipDate], [Status], [CreatedAt], [Note], [QuoteNote], [QuoteToken], [QuoteValidUntil], [QuotedPrice], [ReceiverName], [ReceiverPhone]) VALUES (17, N'LMS20261031-2431', 2, N'Đỗ Duy Tấn', N'0968320717', N'dot01366@gmail.com', 4, 1, 1, N'Phú Quốc', N'Cổng sau ký túc xá khu B DHQG', N'Hàng tiêu dùng', CAST(10.00 AS Decimal(18, 2)), 1, NULL, N'Đã giao', CAST(N'2026-10-31T16:04:34.9830120' AS DateTime2), NULL, NULL, N'a1c118bddb70e93594cab8046f1e1437', CAST(N'2026-11-07T23:59:59.0000000' AS DateTime2), CAST(10000000 AS Decimal(18, 0)), N'Đỗ Duy Tấn', N'0563426487')
INSERT [dbo].[Orders] ([Id], [OrderCode], [CustomerId], [CustomerName], [Phone], [Email], [ServiceId], [VehicleId], [DriverId], [PickupAddress], [DeliveryAddress], [CargoType], [WeightKg], [PackageCount], [ExpectedShipDate], [Status], [CreatedAt], [Note], [QuoteNote], [QuoteToken], [QuoteValidUntil], [QuotedPrice], [ReceiverName], [ReceiverPhone]) VALUES (18, N'LMS20261101-9769', 2, N'Đỗ Duy Tấn', N'0968320717', N'dot01366@gmail.com', 1, 1, 1, N'Phú Quốc', N'Cổng sau ký túc xá khu B DHQG', N'Hàng tiêu dùng', CAST(1.00 AS Decimal(18, 2)), 1, CAST(N'2026-11-03T00:00:00.0000000' AS DateTime2), N'Đã giao', CAST(N'2026-11-01T15:42:19.3617743' AS DateTime2), NULL, NULL, N'fd20d904e107604f83597e560be1ddbc', CAST(N'2026-11-08T23:59:59.0000000' AS DateTime2), CAST(100000000 AS Decimal(18, 0)), N'Đỗ Duy Tấn', N'0563426487')
INSERT [dbo].[Orders] ([Id], [OrderCode], [CustomerId], [CustomerName], [Phone], [Email], [ServiceId], [VehicleId], [DriverId], [PickupAddress], [DeliveryAddress], [CargoType], [WeightKg], [PackageCount], [ExpectedShipDate], [Status], [CreatedAt], [Note], [QuoteNote], [QuoteToken], [QuoteValidUntil], [QuotedPrice], [ReceiverName], [ReceiverPhone]) VALUES (19, N'LMS20261101-1297', NULL, N'Tấn Đỗ', N'0563426487', N'dot01366@gmail.com', 4, 1, 1, N'Phú Quốc', N'Cổng sau ký túc xá khu B DHQG', N'Hàng tiêu dùng', CAST(1.00 AS Decimal(18, 2)), 1, CAST(N'2026-11-18T00:00:00.0000000' AS DateTime2), N'Đã giao', CAST(N'2026-11-01T15:57:34.1389316' AS DateTime2), NULL, NULL, N'1ab0da883f100cb1f0324b88d11df038', CAST(N'2026-11-08T23:59:59.0000000' AS DateTime2), CAST(1000 AS Decimal(18, 0)), N'Đỗ Duy hải', N'0563426487')
INSERT [dbo].[Orders] ([Id], [OrderCode], [CustomerId], [CustomerName], [Phone], [Email], [ServiceId], [VehicleId], [DriverId], [PickupAddress], [DeliveryAddress], [CargoType], [WeightKg], [PackageCount], [ExpectedShipDate], [Status], [CreatedAt], [Note], [QuoteNote], [QuoteToken], [QuoteValidUntil], [QuotedPrice], [ReceiverName], [ReceiverPhone]) VALUES (20, N'LMS20261101-6969', 2, N'Đỗ Duy Hải', N'0968320717', N'dot01366@gmail.com', 4, 1, 1, N'Phú Quốc', N'Cổng sau ký túc xá khu B DHQG', N'Hàng tiêu dùng', CAST(1.00 AS Decimal(18, 2)), 1, NULL, N'Hoàn hàng', CAST(N'2026-11-01T15:58:56.9971078' AS DateTime2), NULL, NULL, N'cea45eff7f0f82d66489c77fd2d57f32', CAST(N'2026-11-08T23:59:59.0000000' AS DateTime2), CAST(10000000 AS Decimal(18, 0)), N'Đỗ Duy Tấn', N'0563426487')
INSERT [dbo].[Orders] ([Id], [OrderCode], [CustomerId], [CustomerName], [Phone], [Email], [ServiceId], [VehicleId], [DriverId], [PickupAddress], [DeliveryAddress], [CargoType], [WeightKg], [PackageCount], [ExpectedShipDate], [Status], [CreatedAt], [Note], [QuoteNote], [QuoteToken], [QuoteValidUntil], [QuotedPrice], [ReceiverName], [ReceiverPhone]) VALUES (21, N'LMS20261102-5712', 2, N'Đỗ Duy Tấn', N'0968320717', N'dot01366@gmail.com', 1, 11, 20, N'Cổng sau ký túc xá khu B DHQG', N'Cổng sau ký túc xá khu B DHQG2', N'Nội thất', CAST(10.70 AS Decimal(18, 2)), 1, CAST(N'2026-11-12T00:00:00.0000000' AS DateTime2), N'Đã giao', CAST(N'2026-11-02T14:01:13.3508114' AS DateTime2), NULL, NULL, N'620a7f57cd65c39bcb61ceb2a5d55b13', CAST(N'2026-11-09T23:59:59.0000000' AS DateTime2), CAST(1000000 AS Decimal(18, 0)), N'Đỗ Duy Tấn', N'0563426487')
INSERT [dbo].[Orders] ([Id], [OrderCode], [CustomerId], [CustomerName], [Phone], [Email], [ServiceId], [VehicleId], [DriverId], [PickupAddress], [DeliveryAddress], [CargoType], [WeightKg], [PackageCount], [ExpectedShipDate], [Status], [CreatedAt], [Note], [QuoteNote], [QuoteToken], [QuoteValidUntil], [QuotedPrice], [ReceiverName], [ReceiverPhone]) VALUES (22, N'LMS20261102-7837', 2, N'Đỗ Duy Tấn', N'0968320717', N'dot01366@gmail.com', 4, 19, 9, N'khi', N'dá', N'Hàng dễ vỡ', CAST(13.00 AS Decimal(18, 2)), 1, CAST(N'2026-11-28T00:00:00.0000000' AS DateTime2), N'Đang vận chuyển', CAST(N'2026-11-02T14:02:59.7915863' AS DateTime2), NULL, NULL, N'6e2e481d20684c81c7c1a9362e4f3d28', CAST(N'2026-11-09T23:59:59.0000000' AS DateTime2), CAST(5000000 AS Decimal(18, 0)), N'Đỗ Duy Tấn', N'0563426487')
INSERT [dbo].[Orders] ([Id], [OrderCode], [CustomerId], [CustomerName], [Phone], [Email], [ServiceId], [VehicleId], [DriverId], [PickupAddress], [DeliveryAddress], [CargoType], [WeightKg], [PackageCount], [ExpectedShipDate], [Status], [CreatedAt], [Note], [QuoteNote], [QuoteToken], [QuoteValidUntil], [QuotedPrice], [ReceiverName], [ReceiverPhone]) VALUES (23, N'LMS20261102-1298', 2, N'Đỗ Duy Tấn', N'0968320717', N'dot01366@gmail.com', 4, NULL, NULL, N'Cổng sau ký túc xá khu B DHQG', N'Cổng sau ký túc xá khu B DHQG', N'Hàng tiêu dùng', CAST(22.00 AS Decimal(18, 2)), 11, CAST(N'2026-11-03T00:00:00.0000000' AS DateTime2), N'Đã xác nhận', CAST(N'2026-11-02T14:15:53.9010250' AS DateTime2), NULL, NULL, N'06c0cd1f82526574a82852b0a18c4b2d', CAST(N'2026-11-09T23:59:59.0000000' AS DateTime2), CAST(100000 AS Decimal(18, 0)), N'Đỗ Duy Tấn', N'0563426487')
INSERT [dbo].[Orders] ([Id], [OrderCode], [CustomerId], [CustomerName], [Phone], [Email], [ServiceId], [VehicleId], [DriverId], [PickupAddress], [DeliveryAddress], [CargoType], [WeightKg], [PackageCount], [ExpectedShipDate], [Status], [CreatedAt], [Note], [QuoteNote], [QuoteToken], [QuoteValidUntil], [QuotedPrice], [ReceiverName], [ReceiverPhone]) VALUES (24, N'LMS20261102-7231', 3, N'Đạt', N'0326970003', N'nguyenxuanquoc0916@gmail.com', 2, 3, 20, N'Phú Quốc', N'Hồ chí minh', N'Hàng tiêu dùng', CAST(100.00 AS Decimal(18, 2)), 10, CAST(N'2026-11-05T00:00:00.0000000' AS DateTime2), N'Đang vận chuyển', CAST(N'2026-11-02T14:19:59.5351124' AS DateTime2), NULL, NULL, N'8b30e721b031af6323ed40f794979de2', CAST(N'2026-11-09T23:59:59.0000000' AS DateTime2), CAST(1000000 AS Decimal(18, 0)), N'Đỗ Duy Tấn', N'0563426487')
INSERT [dbo].[Orders] ([Id], [OrderCode], [CustomerId], [CustomerName], [Phone], [Email], [ServiceId], [VehicleId], [DriverId], [PickupAddress], [DeliveryAddress], [CargoType], [WeightKg], [PackageCount], [ExpectedShipDate], [Status], [CreatedAt], [Note], [QuoteNote], [QuoteToken], [QuoteValidUntil], [QuotedPrice], [ReceiverName], [ReceiverPhone]) VALUES (25, N'LMS20261102-2440', 2, N'Đỗ Duy Tấn', N'0968320717', N'dot01366@gmail.com', 2, NULL, NULL, N'Phú Quốc', N'Hồ chí minh', N'Thiết bị điện tử', CAST(1000.00 AS Decimal(18, 2)), 100, CAST(N'2026-11-05T00:00:00.0000000' AS DateTime2), N'Đã xác nhận', CAST(N'2026-11-02T14:20:37.1157075' AS DateTime2), NULL, NULL, N'6ceab2478520ca6d97df44661080469f', CAST(N'2026-11-09T23:59:59.0000000' AS DateTime2), CAST(100000 AS Decimal(18, 0)), N'Đỗ Duy Tấn', N'0563426487')
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[PasswordResetRequests] ON 

INSERT [dbo].[PasswordResetRequests] ([Id], [UserId], [CreatedAt], [IsHandled], [HandledAt]) VALUES (1, 2, CAST(N'2026-10-05T17:41:21.1632156' AS DateTime2), 1, CAST(N'2026-10-05T17:42:04.7458678' AS DateTime2))
SET IDENTITY_INSERT [dbo].[PasswordResetRequests] OFF
GO
SET IDENTITY_INSERT [dbo].[ProcessSteps] ON 

INSERT [dbo].[ProcessSteps] ([Id], [Number], [Title], [Description], [Icon]) VALUES (1, 1, N'Tiếp nhận yêu cầu', N'Khách gửi yêu cầu vận chuyển, hệ thống ghi nhận và gửi xác nhận.', N'bi-pencil-square')
INSERT [dbo].[ProcessSteps] ([Id], [Number], [Title], [Description], [Icon]) VALUES (2, 2, N'Xác nhận đơn', N'Nhân viên báo giá, khách đồng ý thì đơn được xác nhận.', N'bi-patch-check')
INSERT [dbo].[ProcessSteps] ([Id], [Number], [Title], [Description], [Icon]) VALUES (3, 3, N'Nhận hàng', N'Nhân viên đến lấy hàng, kiểm tra và đóng gói.', N'bi-box-seam')
INSERT [dbo].[ProcessSteps] ([Id], [Number], [Title], [Description], [Icon]) VALUES (4, 4, N'Phân loại', N'Hàng được phân tuyến tại kho, cập nhật trạng thái trên hệ thống.', N'bi-diagram-3')
INSERT [dbo].[ProcessSteps] ([Id], [Number], [Title], [Description], [Icon]) VALUES (5, 5, N'Vận chuyển', N'Phương tiện và tài xế được phân công đưa hàng đi.', N'bi-truck')
INSERT [dbo].[ProcessSteps] ([Id], [Number], [Title], [Description], [Icon]) VALUES (6, 6, N'Giao hàng', N'Giao đúng người nhận.', N'bi-geo-alt')
INSERT [dbo].[ProcessSteps] ([Id], [Number], [Title], [Description], [Icon]) VALUES (7, 7, N'Hoàn thành', N'Xác nhận giao thành công, đối soát COD nếu có.', N'bi-check2-circle')
SET IDENTITY_INSERT [dbo].[ProcessSteps] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Code], [Name], [Description]) VALUES (1, N'SuperAdmin', N'SuperAdmin', N'Toàn quyền hệ thống')
INSERT [dbo].[Roles] ([Id], [Code], [Name], [Description]) VALUES (2, N'Dispatcher', N'Điều phối viên', N'Đơn hàng, phương tiện, tài xế, chuyến, liên hệ')
INSERT [dbo].[Roles] ([Id], [Code], [Name], [Description]) VALUES (3, N'Editor', N'Biên tập viên', N'Nội dung website: trang chủ, giới thiệu, quy trình, bài viết, trang tĩnh, hình ảnh, SEO')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[SavedAddresses] ON 

INSERT [dbo].[SavedAddresses] ([Id], [UserId], [Label], [Address], [ContactName], [Phone]) VALUES (1, 2, N'Ktx khu b đại học quốc gia', N'Cổng sau ký túc xá khu B DHQG', NULL, NULL)
SET IDENTITY_INSERT [dbo].[SavedAddresses] OFF
GO
SET IDENTITY_INSERT [dbo].[SeoPages] ON 

INSERT [dbo].[SeoPages] ([Id], [Route], [PageName], [MetaTitle], [MetaDescription], [Keywords]) VALUES (1, N'Home/Index', N'Trang chủ', N'LMS Express - Vận chuyển nhanh chóng, an toàn', N'Dịch vụ vận chuyển, giao nhận hàng hóa trên toàn quốc, theo dõi hành trình minh bạch, báo giá nhanh.', N'vận chuyển, logistics, giao hàng, báo giá')
INSERT [dbo].[SeoPages] ([Id], [Route], [PageName], [MetaTitle], [MetaDescription], [Keywords]) VALUES (2, N'Home/About', N'Giới thiệu', N'Giới thiệu LMS Express', N'Đơn vị logistics tận tâm, kết nối hàng hóa trên mọi miền đất nước.', NULL)
INSERT [dbo].[SeoPages] ([Id], [Route], [PageName], [MetaTitle], [MetaDescription], [Keywords]) VALUES (3, N'Service/Index', N'Dịch vụ', N'Dịch vụ vận chuyển - LMS Express', N'Giao hàng hỏa tốc, vận chuyển đường bộ, Bắc - Nam, cho thuê xe tải, kho bãi và COD.', NULL)
INSERT [dbo].[SeoPages] ([Id], [Route], [PageName], [MetaTitle], [MetaDescription], [Keywords]) VALUES (4, N'Service/Quote', N'Yêu cầu báo giá', N'Yêu cầu báo giá vận chuyển', N'Gửi yêu cầu để nhận báo giá vận chuyển nhanh từ LMS Express.', NULL)
INSERT [dbo].[SeoPages] ([Id], [Route], [PageName], [MetaTitle], [MetaDescription], [Keywords]) VALUES (5, N'Process/Index', N'Quy trình', N'Quy trình vận chuyển - LMS Express', N'Các bước xử lý đơn hàng từ tiếp nhận đến giao hàng thành công.', NULL)
INSERT [dbo].[SeoPages] ([Id], [Route], [PageName], [MetaTitle], [MetaDescription], [Keywords]) VALUES (6, N'News/Index', N'Tin tức', N'Tin tức logistics - LMS Express', N'Tin tức, kinh nghiệm và xu hướng ngành vận chuyển, logistics.', NULL)
INSERT [dbo].[SeoPages] ([Id], [Route], [PageName], [MetaTitle], [MetaDescription], [Keywords]) VALUES (7, N'Home/Policy', N'Chính sách', N'Chính sách vận chuyển - LMS Express', N'Các quy định giúp bảo vệ quyền lợi của khách hàng và hàng hóa.', NULL)
INSERT [dbo].[SeoPages] ([Id], [Route], [PageName], [MetaTitle], [MetaDescription], [Keywords]) VALUES (8, N'Home/Terms', N'Điều khoản', N'Điều khoản sử dụng - LMS Express', N'Quy định chung khi sử dụng hệ thống LMS Express.', NULL)
INSERT [dbo].[SeoPages] ([Id], [Route], [PageName], [MetaTitle], [MetaDescription], [Keywords]) VALUES (9, N'Home/Faq', N'Câu hỏi thường gặp', N'Câu hỏi thường gặp - LMS Express', N'Giải đáp nhanh những thắc mắc về dịch vụ vận chuyển.', NULL)
INSERT [dbo].[SeoPages] ([Id], [Route], [PageName], [MetaTitle], [MetaDescription], [Keywords]) VALUES (10, N'Home/Contact', N'Liên hệ', N'Liên hệ LMS Express', N'Liên hệ hotline hoặc gửi yêu cầu để được tư vấn.', NULL)
SET IDENTITY_INSERT [dbo].[SeoPages] OFF
GO
SET IDENTITY_INSERT [dbo].[Services] ON 

INSERT [dbo].[Services] ([Id], [Name], [Summary], [Icon], [DeliveryTime], [PriceNote], [IsFeatured], [FeaturedOrder], [ImageUrl], [Description], [IsVisible], [IsExclusive]) VALUES (1, N'Giao hàng nội thành', N'Nhận và giao hàng ưu tiên trong nội thành, phù hợp chứng từ và kiện hàng cần gấp.', N'bi-lightning-charge', N'1 - 2 ngày', N'Báo giá theo chuyến', 1, 1, N'/images/Services/giao-hang-trong-ngay-ebay.jpg', NULL, 1, 0)
INSERT [dbo].[Services] ([Id], [Name], [Summary], [Icon], [DeliveryTime], [PriceNote], [IsFeatured], [FeaturedOrder], [ImageUrl], [Description], [IsVisible], [IsExclusive]) VALUES (2, N'Vận chuyển hàng ghép liên tỉnh', N'Vận chuyển hàng hóa với nhiều tỉnh và lịch trình ổn định.', N'bi-truck', N'2 - 5 ngày', N'Báo giá theo khối lượng', 1, 2, N'/images/Services/van-tai-duong-bo.png', NULL, 1, 0)
INSERT [dbo].[Services] ([Id], [Name], [Summary], [Icon], [DeliveryTime], [PriceNote], [IsFeatured], [FeaturedOrder], [ImageUrl], [Description], [IsVisible], [IsExclusive]) VALUES (3, N'Vận chuyển Bắc - Nam', N'Kết nối kho hàng hai miền, theo dõi hành trình và hỗ trợ giao nhận tận nơi.', N'bi-map', N'3 - 5 ngày', N'Từ 1.500đ/kg', 0, 3, N'/images/Services/dich-vu-van-chuyen-hang-hoa-bac-nam-gia-re-chat-luong-1.jpg', NULL, 0, 0)
INSERT [dbo].[Services] ([Id], [Name], [Summary], [Icon], [DeliveryTime], [PriceNote], [IsFeatured], [FeaturedOrder], [ImageUrl], [Description], [IsVisible], [IsExclusive]) VALUES (4, N'Cho thuê xe tải nguyên chuyến', N'Xe tải đa tải trọng từ 500kg đến 15 tấn, có tài xế và hỗ trợ bốc xếp theo yêu cầu.', N'bi-box-seam', N'Theo lịch đặt', N'Linh hoạt theo chuyến', 1, 3, NULL, NULL, 1, 1)
SET IDENTITY_INSERT [dbo].[Services] OFF
GO
SET IDENTITY_INSERT [dbo].[SiteSettings] ON 

INSERT [dbo].[SiteSettings] ([Id], [SiteName], [LogoUrl], [FaviconUrl], [Hotline], [Email], [Address], [WorkingHours], [FooterIntro], [Copyright], [FacebookUrl], [YoutubeUrl], [LinkedInUrl], [TikTokUrl], [ZaloUrl]) VALUES (1, N'LMS Express', NULL, NULL, N'1900 6868', N'support@lmsexpress.vn', N'123 Đường Logistics, Quận 1, TP. Hồ Chí Minh', N'T2 - T7: 7:30 - 18:00', N'Đồng hành cùng hàng triệu hành trình, mang đến dịch vụ giao nhận an toàn và hiệu quả trên toàn quốc.', N'© 2026 LMS Express Logistics. Bảo lưu mọi quyền.', NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[SiteSettings] OFF
GO
SET IDENTITY_INSERT [dbo].[StaticPages] ON 

INSERT [dbo].[StaticPages] ([Id], [Slug], [Title], [Subtitle], [Content], [Icon], [IsPublished], [IsSystem]) VALUES (1, N'chinh-sach', N'Chính sách vận chuyển', N'Các quy định giúp bảo vệ quyền lợi của khách hàng và hàng hóa.', N'<h2>1. Phạm vi phục vụ</h2><p>LMS Express nhận vận chuyển hàng hóa hợp pháp trên phạm vi 63 tỉnh thành, tùy thuộc vào loại dịch vụ và khu vực giao nhận.</p>
<h2>2. Quy định đóng gói</h2><p>Hàng hóa cần được đóng gói phù hợp với đặc tính sản phẩm. Hàng dễ vỡ, chất lỏng hoặc hàng có giá trị cần được khai báo khi tạo yêu cầu.</p>
<h2>3. Thời gian giao hàng</h2><p>Thời gian dự kiến được tính từ lúc đơn hàng được tiếp nhận thành công và có thể thay đổi do thời tiết, giao thông hoặc sự kiện bất khả kháng.</p>
<h2>4. Bồi thường</h2><p>Yêu cầu khiếu nại cần kèm mã vận đơn và chứng từ chứng minh giá trị hàng hóa. Mức bồi thường được áp dụng theo điều khoản của dịch vụ đã chọn.</p>', N'bi-shield-check', 1, 1)
INSERT [dbo].[StaticPages] ([Id], [Slug], [Title], [Subtitle], [Content], [Icon], [IsPublished], [IsSystem]) VALUES (2, N'dieu-khoan', N'Điều khoản sử dụng', N'Quy định chung khi sử dụng hệ thống LMS Express.', N'<h2>Trách nhiệm người dùng</h2><p>Người dùng cung cấp thông tin chính xác, không gửi hàng thuộc danh mục cấm và chịu trách nhiệm về nội dung kiện hàng.</p>
<h2>Bảo mật tài khoản</h2><p>Khách hàng có trách nhiệm bảo mật thông tin đăng nhập và thông báo ngay khi phát hiện hoạt động bất thường.</p>
<h2>Thay đổi điều khoản</h2><p>LMS Express có thể cập nhật nội dung để phù hợp với quy định pháp luật và hoạt động vận hành thực tế.</p>', N'bi-file-earmark-text', 1, 1)
INSERT [dbo].[StaticPages] ([Id], [Slug], [Title], [Subtitle], [Content], [Icon], [IsPublished], [IsSystem]) VALUES (3, N'cau-hoi-thuong-gap', N'Câu hỏi thường gặp', N'Giải đáp nhanh những thắc mắc về dịch vụ vận chuyển.', N'<div class="accordion faq-accordion" id="faq">
<div class="accordion-item"><h3 class="accordion-header"><button class="accordion-button" data-bs-toggle="collapse" data-bs-target="#f1">Làm sao tra cứu hành trình đơn hàng?</button></h3><div id="f1" class="accordion-collapse collapse show" data-bs-parent="#faq"><div class="accordion-body">Nhập mã vận đơn vào ô “Tra cứu đơn hàng” tại trang chủ hoặc trang tra cứu.</div></div></div>
<div class="accordion-item"><h3 class="accordion-header"><button class="accordion-button collapsed" data-bs-toggle="collapse" data-bs-target="#f2">Thời gian giao hàng mất bao lâu?</button></h3><div id="f2" class="accordion-collapse collapse" data-bs-parent="#faq"><div class="accordion-body">Tùy dịch vụ và tuyến đường, thời gian dự kiến từ 2 giờ đến 5 ngày.</div></div></div>
<div class="accordion-item"><h3 class="accordion-header"><button class="accordion-button collapsed" data-bs-toggle="collapse" data-bs-target="#f3">LMS Express có hỗ trợ thu hộ COD không?</button></h3><div id="f3" class="accordion-collapse collapse" data-bs-parent="#faq"><div class="accordion-body">Có. Tiền thu hộ được đối soát và chuyển theo chu kỳ đã đăng ký.</div></div></div>
</div>', N'bi-question-circle', 1, 1)
INSERT [dbo].[StaticPages] ([Id], [Slug], [Title], [Subtitle], [Content], [Icon], [IsPublished], [IsSystem]) VALUES (4, N'bao-mat', N'Chính sách bảo mật', N'Cách LMS Express thu thập và bảo vệ thông tin của bạn.', N'<h2>Thông tin thu thập</h2><p>Chúng tôi thu thập họ tên, số điện thoại, email và địa chỉ giao nhận để xử lý yêu cầu vận chuyển.</p>
<h2>Mục đích sử dụng</h2><p>Thông tin chỉ dùng để báo giá, giao nhận, chăm sóc khách hàng và không chia sẻ cho bên thứ ba ngoài phạm vi vận chuyển.</p>
<h2>Bảo mật</h2><p>Mật khẩu được mã hóa và dữ liệu được lưu trữ trên hệ thống có kiểm soát truy cập.</p>', N'bi-lock', 1, 0)
INSERT [dbo].[StaticPages] ([Id], [Slug], [Title], [Subtitle], [Content], [Icon], [IsPublished], [IsSystem]) VALUES (5, N'quy-dinh-gui-hang', N'Quy định gửi hàng', N'Danh mục hàng được nhận và hàng cấm gửi.', N'<h2>Hàng được nhận</h2><p>Hàng hóa hợp pháp, đóng gói đúng quy cách và khai báo đầy đủ loại hàng, khối lượng, số kiện.</p>
<h2>Hàng cấm gửi</h2><p>Chất cháy nổ, chất ma túy, vũ khí, hàng hóa vi phạm pháp luật và các mặt hàng thuộc danh mục cấm vận chuyển.</p>
<h2>Hàng đặc biệt</h2><p>Hàng dễ vỡ, hàng giá trị cao hoặc cần bảo quản riêng phải khai báo khi tạo yêu cầu để được xử lý phù hợp.</p>', N'bi-box-seam', 1, 0)
SET IDENTITY_INSERT [dbo].[StaticPages] OFF
GO
SET IDENTITY_INSERT [dbo].[TripOrders] ON 

INSERT [dbo].[TripOrders] ([Id], [TripId], [OrderId]) VALUES (1, 1, 17)
INSERT [dbo].[TripOrders] ([Id], [TripId], [OrderId]) VALUES (2, 2, 18)
INSERT [dbo].[TripOrders] ([Id], [TripId], [OrderId]) VALUES (3, 3, 19)
INSERT [dbo].[TripOrders] ([Id], [TripId], [OrderId]) VALUES (4, 4, 20)
INSERT [dbo].[TripOrders] ([Id], [TripId], [OrderId]) VALUES (5, 5, 21)
INSERT [dbo].[TripOrders] ([Id], [TripId], [OrderId]) VALUES (6, 6, 22)
INSERT [dbo].[TripOrders] ([Id], [TripId], [OrderId]) VALUES (7, 7, 24)
SET IDENTITY_INSERT [dbo].[TripOrders] OFF
GO
SET IDENTITY_INSERT [dbo].[Trips] ON 

INSERT [dbo].[Trips] ([Id], [Route], [VehicleId], [DriverId], [DepartAt], [Status], [Note], [CreatedAt]) VALUES (1, N'qấ', 1, 1, NULL, N'Hoàn thành', NULL, CAST(N'2026-11-01T02:44:07.3566686' AS DateTime2))
INSERT [dbo].[Trips] ([Id], [Route], [VehicleId], [DriverId], [DepartAt], [Status], [Note], [CreatedAt]) VALUES (2, N'fa', 1, 1, CAST(N'2026-11-03T06:54:00.0000000' AS DateTime2), N'Hoàn thành', NULL, CAST(N'2026-11-01T15:52:29.1945347' AS DateTime2))
INSERT [dbo].[Trips] ([Id], [Route], [VehicleId], [DriverId], [DepartAt], [Status], [Note], [CreatedAt]) VALUES (3, N'ádasd', 1, 1, CAST(N'2026-11-09T06:59:00.0000000' AS DateTime2), N'Hoàn thành', NULL, CAST(N'2026-11-01T16:57:58.6708636' AS DateTime2))
INSERT [dbo].[Trips] ([Id], [Route], [VehicleId], [DriverId], [DepartAt], [Status], [Note], [CreatedAt]) VALUES (4, N'âdsadas', 1, 1, NULL, N'Hoàn thành', NULL, CAST(N'2026-11-01T17:06:12.2836666' AS DateTime2))
INSERT [dbo].[Trips] ([Id], [Route], [VehicleId], [DriverId], [DepartAt], [Status], [Note], [CreatedAt]) VALUES (5, N'Hồ Chí Minh - Hà Nội', 11, 20, CAST(N'2026-11-12T14:14:00.0000000' AS DateTime2), N'Hoàn thành', NULL, CAST(N'2026-11-02T14:12:27.9022697' AS DateTime2))
INSERT [dbo].[Trips] ([Id], [Route], [VehicleId], [DriverId], [DepartAt], [Status], [Note], [CreatedAt]) VALUES (6, N'Hồ Chí Minh - Hà Nội', 19, 9, CAST(N'2026-11-04T19:18:00.0000000' AS DateTime2), N'Đang chạy', NULL, CAST(N'2026-11-02T14:17:09.9905735' AS DateTime2))
INSERT [dbo].[Trips] ([Id], [Route], [VehicleId], [DriverId], [DepartAt], [Status], [Note], [CreatedAt]) VALUES (7, N'Phú Quốc - Hồ Chí Minh', 3, 20, CAST(N'2026-11-05T09:21:00.0000000' AS DateTime2), N'Đang chạy', NULL, CAST(N'2026-11-02T14:22:28.4586072' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Trips] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [FullName], [Email], [Phone], [PasswordHash], [PasswordSalt], [Role], [IsActive]) VALUES (2, N'Đỗ Duy Tấn', N'dot01366@gmail.com', N'0968320717', N'HVN4PD7SkhoizbxYUaBG0HsohsmGBu7f9klmHo+LEyo=', N'jm1Y+/1dwKnrOU8Ha7SRwg==', N'Customer', 1)
INSERT [dbo].[Users] ([Id], [FullName], [Email], [Phone], [PasswordHash], [PasswordSalt], [Role], [IsActive]) VALUES (3, N'Đạt', N'nguyenxuanquoc0916@gmail.com', N'0326970003', N'T6wF5fygN4nuGiZ7OvBMejSfIYwMdJqZWzg/qXVaUhM=', N'fej3vcm+M9z/ujK03pudIA==', N'Customer', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Vehicles] ON 

INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (1, N'68Ma-2000', N'Xe tải nhỏ', CAST(500.00 AS Decimal(18, 2)), N'Đang hoạt động', NULL)
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (2, N'59C-123.45', N'Xe tải nhỏ', CAST(1250.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Thùng kín, chạy nội thành')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (3, N'59C-234.56', N'Xe tải nhỏ', CAST(1500.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Thùng lửng')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (4, N'51D-345.67', N'Xe tải nhỏ', CAST(990.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Xe van, giao hàng nhanh')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (5, N'51D-456.78', N'Xe tải nhỏ', CAST(1900.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Thùng mui bạt')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (6, N'60C-567.89', N'Xe tải nhỏ', CAST(2000.00 AS Decimal(18, 2)), N'Bảo trì', N'Đang thay lốp, dự kiến xong sau 3 ngày')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (7, N'61C-678.90', N'Xe tải trung', CAST(3500.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Thùng kín 4,3 m')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (8, N'61C-789.01', N'Xe tải trung', CAST(5000.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Thùng mui bạt 6 m')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (9, N'72C-890.12', N'Xe tải trung', CAST(5000.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Tuyến Sài Gòn - Vũng Tàu')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (10, N'72C-901.23', N'Xe tải trung', CAST(6500.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Có bửng nâng hàng')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (11, N'50H-012.34', N'Xe tải trung', CAST(7000.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Thùng kín')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (12, N'50H-111.22', N'Xe tải trung', CAST(8000.00 AS Decimal(18, 2)), N'Bảo trì', N'Bảo dưỡng định kỳ')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (13, N'51C-222.33', N'Xe tải lớn', CAST(10000.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Thùng kín 7,5 m')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (14, N'51C-333.44', N'Xe tải lớn', CAST(12000.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Tuyến liên tỉnh Bắc - Nam')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (15, N'60H-444.55', N'Xe tải lớn', CAST(15000.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Xe 3 chân, thùng mui bạt')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (16, N'60H-555.66', N'Xe tải lớn', CAST(15000.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Xe 3 chân')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (17, N'61H-666.77', N'Xe tải lớn', CAST(18000.00 AS Decimal(18, 2)), N'Ngừng sử dụng', N'Xe cũ, đã thanh lý')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (18, N'29H-777.88', N'Container', CAST(20000.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Container 20 feet')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (19, N'29H-888.99', N'Container', CAST(24000.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Container 40 feet')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (20, N'51F-999.00', N'Container', CAST(28000.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Container 40 feet cao')
INSERT [dbo].[Vehicles] ([Id], [PlateNumber], [VehicleType], [CapacityKg], [Status], [Note]) VALUES (21, N'51M-12345', N'Xe máy', CAST(50.00 AS Decimal(18, 2)), N'Đang hoạt động', N'Giao hỏa tốc nội thành')
SET IDENTITY_INSERT [dbo].[Vehicles] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AdminUsers_Email]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_AdminUsers_Email] ON [dbo].[AdminUsers]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AdminUsers_RoleId]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_AdminUsers_RoleId] ON [dbo].[AdminUsers]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ArticleBlocks_ArticleId]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_ArticleBlocks_ArticleId] ON [dbo].[ArticleBlocks]
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_CargoTypes_Name]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_CargoTypes_Name] ON [dbo].[CargoTypes]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Drivers_LicenseNumber]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Drivers_LicenseNumber] ON [dbo].[Drivers]
(
	[LicenseNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HomeSectionItems_SectionId]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_HomeSectionItems_SectionId] ON [dbo].[HomeSectionItems]
(
	[SectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_NewsletterSubscribers_Email]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_NewsletterSubscribers_Email] ON [dbo].[NewsletterSubscribers]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderEvents_OrderId]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_OrderEvents_OrderId] ON [dbo].[OrderEvents]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderItems_CargoTypeId]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_OrderItems_CargoTypeId] ON [dbo].[OrderItems]
(
	[CargoTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderItems_OrderId]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_OrderItems_OrderId] ON [dbo].[OrderItems]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_CustomerId]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_CustomerId] ON [dbo].[Orders]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_DriverId]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_DriverId] ON [dbo].[Orders]
(
	[DriverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Orders_OrderCode]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Orders_OrderCode] ON [dbo].[Orders]
(
	[OrderCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Orders_QuoteToken]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Orders_QuoteToken] ON [dbo].[Orders]
(
	[QuoteToken] ASC
)
WHERE ([QuoteToken] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_ServiceId]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_ServiceId] ON [dbo].[Orders]
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_VehicleId]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_VehicleId] ON [dbo].[Orders]
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PasswordResetRequests_UserId]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_PasswordResetRequests_UserId] ON [dbo].[PasswordResetRequests]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Roles_Code]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Roles_Code] ON [dbo].[Roles]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SavedAddresses_UserId]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_SavedAddresses_UserId] ON [dbo].[SavedAddresses]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_SeoPages_Route]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_SeoPages_Route] ON [dbo].[SeoPages]
(
	[Route] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_StaticPages_Slug]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_StaticPages_Slug] ON [dbo].[StaticPages]
(
	[Slug] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TripOrders_OrderId]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_TripOrders_OrderId] ON [dbo].[TripOrders]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TripOrders_TripId]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_TripOrders_TripId] ON [dbo].[TripOrders]
(
	[TripId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Trips_DriverId]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_Trips_DriverId] ON [dbo].[Trips]
(
	[DriverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Trips_VehicleId]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_Trips_VehicleId] ON [dbo].[Trips]
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_Email]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Email] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Vehicles_PlateNumber]    Script Date: 11/8/2026 3:59:34 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Vehicles_PlateNumber] ON [dbo].[Vehicles]
(
	[PlateNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Articles] ADD  DEFAULT ((0)) FOR [FeaturedOrder]
GO
ALTER TABLE [dbo].[Articles] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsFeatured]
GO
ALTER TABLE [dbo].[HomeSections] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsSystem]
GO
ALTER TABLE [dbo].[Services] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsVisible]
GO
ALTER TABLE [dbo].[Services] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsExclusive]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsActive]
GO
ALTER TABLE [dbo].[AdminUsers]  WITH CHECK ADD  CONSTRAINT [FK_AdminUsers_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[AdminUsers] CHECK CONSTRAINT [FK_AdminUsers_Roles_RoleId]
GO
ALTER TABLE [dbo].[ArticleBlocks]  WITH CHECK ADD  CONSTRAINT [FK_ArticleBlocks_Articles_ArticleId] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ArticleBlocks] CHECK CONSTRAINT [FK_ArticleBlocks_Articles_ArticleId]
GO
ALTER TABLE [dbo].[HomeSectionItems]  WITH CHECK ADD  CONSTRAINT [FK_HomeSectionItems_HomeSections_SectionId] FOREIGN KEY([SectionId])
REFERENCES [dbo].[HomeSections] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HomeSectionItems] CHECK CONSTRAINT [FK_HomeSectionItems_HomeSections_SectionId]
GO
ALTER TABLE [dbo].[OrderEvents]  WITH CHECK ADD  CONSTRAINT [FK_OrderEvents_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderEvents] CHECK CONSTRAINT [FK_OrderEvents_Orders_OrderId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_CargoTypes_CargoTypeId] FOREIGN KEY([CargoTypeId])
REFERENCES [dbo].[CargoTypes] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_CargoTypes_CargoTypeId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Orders_OrderId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Drivers_DriverId] FOREIGN KEY([DriverId])
REFERENCES [dbo].[Drivers] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Drivers_DriverId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Services_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Services_ServiceId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users_CustomerId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Vehicles_VehicleId] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicles] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Vehicles_VehicleId]
GO
ALTER TABLE [dbo].[PasswordResetRequests]  WITH CHECK ADD  CONSTRAINT [FK_PasswordResetRequests_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PasswordResetRequests] CHECK CONSTRAINT [FK_PasswordResetRequests_Users_UserId]
GO
ALTER TABLE [dbo].[SavedAddresses]  WITH CHECK ADD  CONSTRAINT [FK_SavedAddresses_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SavedAddresses] CHECK CONSTRAINT [FK_SavedAddresses_Users_UserId]
GO
ALTER TABLE [dbo].[TripOrders]  WITH CHECK ADD  CONSTRAINT [FK_TripOrders_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TripOrders] CHECK CONSTRAINT [FK_TripOrders_Orders_OrderId]
GO
ALTER TABLE [dbo].[TripOrders]  WITH CHECK ADD  CONSTRAINT [FK_TripOrders_Trips_TripId] FOREIGN KEY([TripId])
REFERENCES [dbo].[Trips] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TripOrders] CHECK CONSTRAINT [FK_TripOrders_Trips_TripId]
GO
ALTER TABLE [dbo].[Trips]  WITH CHECK ADD  CONSTRAINT [FK_Trips_Drivers_DriverId] FOREIGN KEY([DriverId])
REFERENCES [dbo].[Drivers] ([Id])
GO
ALTER TABLE [dbo].[Trips] CHECK CONSTRAINT [FK_Trips_Drivers_DriverId]
GO
ALTER TABLE [dbo].[Trips]  WITH CHECK ADD  CONSTRAINT [FK_Trips_Vehicles_VehicleId] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicles] ([Id])
GO
ALTER TABLE [dbo].[Trips] CHECK CONSTRAINT [FK_Trips_Vehicles_VehicleId]
GO
