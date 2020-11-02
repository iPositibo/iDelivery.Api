-- Install-Package Bricelam.EntityFrameworkCore.Pluralizer
-- Scaffold-DbContext "Server=.\sqlexpress;Database=iDelivery;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Context "DataContext"

USE [iDelivery]
GO
/****** Object:  Table [dbo].[AllowedLocation]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllowedLocation](
	[AllowedLocationId] [int] IDENTITY(1,1) NOT NULL,
	[Location] [nvarchar](max) NOT NULL,
	[IsAllowed] [bit] NOT NULL,
 CONSTRAINT [PK_AllowedLocation] PRIMARY KEY CLUSTERED 
(
	[AllowedLocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppRating]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppRating](
	[AppRatingId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Feedback] [ntext] NULL,
	[Rating] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_AppRating] PRIMARY KEY CLUSTERED 
(
	[AppRatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlockedCustomer]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlockedCustomer](
	[BlockedCustomerId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Reason] [ntext] NOT NULL,
	[DateBlocked] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_BlockedCustomer] PRIMARY KEY CLUSTERED 
(
	[BlockedCustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlockedRider]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlockedRider](
	[BlockedRiderId] [int] IDENTITY(1,1) NOT NULL,
	[RiderId] [int] NOT NULL,
	[Reason] [ntext] NOT NULL,
	[DateBlocked] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_BlockedRider] PRIMARY KEY CLUSTERED 
(
	[BlockedRiderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[BookingId] [int] IDENTITY(1,1) NOT NULL,
	[BookingStatusId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ContactName] [nvarchar](50) NOT NULL,
	[ContactNumber] [nvarchar](50) NOT NULL,
	[Items] [text] NOT NULL,
	[PhotoUrl] [nvarchar](256) NULL,
	[TotalEstimatedWeight] [nvarchar](max) NULL,
	[TotalKilometers] [nvarchar](10) NULL,
	[EstimatedTime] [nvarchar](10) NULL,
	[Notes] [ntext] NULL,
	[FareId] [int] NULL,
	[BookingDate] [datetime] NOT NULL,
	[RiderId] [int] NULL,
	[PickupLocation] [nvarchar](max) NOT NULL,
	[PickupLongitude] [nvarchar](max) NULL,
	[PickupLatitude] [nvarchar](max) NULL,
	[PickupTime] [datetime] NULL,
	[DropOffLocation] [nvarchar](max) NOT NULL,
	[DropOffLongitude] [nvarchar](max) NULL,
	[DropOffLatitude] [nvarchar](max) NULL,
	[DropOffTime] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[ReferenceNumber] [nvarchar](max) NOT NULL,
	[ReceiptNumber] [nvarchar](max) NULL,
	[IsSpam] [bit] NULL,
	[TotalFare] [money] NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingHistory]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingHistory](
	[BookingHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[BookingStatusId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[BookingDate] [datetime] NOT NULL,
 CONSTRAINT [PK_BookingHistory] PRIMARY KEY CLUSTERED 
(
	[BookingHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingStatus]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingStatus](
	[BookingStatusId] [int] IDENTITY(1,1) NOT NULL,
	[BookingStatusName] [nvarchar](50) NOT NULL,
	[StatusColor] [nvarchar](50) NULL,
 CONSTRAINT [PK_BookingStatus] PRIMARY KEY CLUSTERED 
(
	[BookingStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[PhotoUrl] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[ContactNumber] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[ActivateEmailReceipts] [bit] NOT NULL,
	[Longitude] [nvarchar](max) NULL,
	[Latitude] [nvarchar](max) NULL,
	[CustomerStatusId] [int] NULL,
	[RatingId] [int] NULL,
	[UserId] [int] NULL,
	[FareId] [int] NULL,
	[VerificationCode] [nvarchar](max) NULL,
	[IsVerified] [bit] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerBookingHistory]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerBookingHistory](
	[CustomerBookingHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[ReceiverCompleteName] [nvarchar](50) NOT NULL,
	[ReceiverCompleteAddress] [nvarchar](max) NOT NULL,
	[ItemDetails] [ntext] NOT NULL,
	[TotalFare] [money] NOT NULL,
	[TotalKilometers] [nvarchar](10) NOT NULL,
	[EstimatedTime] [nvarchar](10) NOT NULL,
	[BookingStatusId] [int] NOT NULL,
	[Receipt] [nvarchar](50) NOT NULL,
	[BookingDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerBookingHistory] PRIMARY KEY CLUSTERED 
(
	[CustomerBookingHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerFare]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerFare](
	[CustomerFareId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[FareId] [int] NOT NULL,
 CONSTRAINT [PK_CustomerFare] PRIMARY KEY CLUSTERED 
(
	[CustomerFareId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerRating]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerRating](
	[CustomerRatingId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[BlockedCount] [int] NULL,
	[ReportedCount] [int] NULL,
	[Rating] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[Feedback] [nvarchar](max) NULL,
 CONSTRAINT [PK_CustomerRating] PRIMARY KEY CLUSTERED 
(
	[CustomerRatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerStatus]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerStatus](
	[CustomerStatusId] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_CustomerStatus] PRIMARY KEY CLUSTERED 
(
	[CustomerStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstimatedWeight]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstimatedWeight](
	[EstimatedWeightId] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_EstimatedWeight] PRIMARY KEY CLUSTERED 
(
	[EstimatedWeightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExternalAccount]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExternalAccount](
	[ExternalAccountId] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[AccountId] [nvarchar](max) NOT NULL,
	[UserId] [int] NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ExternalAccount] PRIMARY KEY CLUSTERED 
(
	[ExternalAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAQ]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAQ](
	[FAQId] [int] IDENTITY(1,1) NOT NULL,
	[FAQContent] [ntext] NOT NULL,
	[Answer] [ntext] NOT NULL,
 CONSTRAINT [PK_FAQ] PRIMARY KEY CLUSTERED 
(
	[FAQId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fare]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fare](
	[FareId] [int] IDENTITY(1,1) NOT NULL,
	[BaseFare] [money] NOT NULL,
	[TotalBaseKilometers] [nvarchar](20) NOT NULL,
	[Surcharge] [money] NULL,
	[PricePerKilometer] [money] NOT NULL,
	[RidersPercentage] [nvarchar](10) NOT NULL,
	[CompanyPercentage] [nvarchar](20) NOT NULL,
	[IsDefault] [bit] NULL,
	[AllowedBalance] [money] NULL,
 CONSTRAINT [PK_Fare] PRIMARY KEY CLUSTERED 
(
	[FareId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[FeedbackId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Message] [ntext] NOT NULL,
	[DateReported] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[FeedbackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuAccess]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuAccess](
	[MenuAccessId] [int] IDENTITY(1,1) NOT NULL,
	[MenuItemId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NULL,
 CONSTRAINT [PK_MenuAccess] PRIMARY KEY CLUSTERED 
(
	[MenuAccessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuItem]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuItem](
	[MenuItemId] [int] IDENTITY(1,1) NOT NULL,
	[Icon] [nvarchar](50) NOT NULL,
	[Text] [nvarchar](50) NOT NULL,
	[Link] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MenuItem] PRIMARY KEY CLUSTERED 
(
	[MenuItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OTPRegistration]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OTPRegistration](
	[OTPRegistrationId] [int] IDENTITY(1,1) NOT NULL,
	[OTPCode] [nvarchar](10) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[DateRegistered] [datetime] NOT NULL,
 CONSTRAINT [PK_OTPRegistration] PRIMARY KEY CLUSTERED 
(
	[OTPRegistrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rate]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rate](
	[RateId] [int] IDENTITY(1,1) NOT NULL,
	[Kilometer] [nvarchar](30) NOT NULL,
	[Fare] [money] NOT NULL,
 CONSTRAINT [PK_Rate] PRIMARY KEY CLUSTERED 
(
	[RateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rating]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[RatingId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[RiderId] [int] NOT NULL,
	[Value] [int] NOT NULL,
 CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED 
(
	[RatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportCustomer]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportCustomer](
	[ReportCustomerId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[RiderId] [int] NOT NULL,
	[Comments] [ntext] NOT NULL,
	[DateReported] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ReportCustomer] PRIMARY KEY CLUSTERED 
(
	[ReportCustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportRider]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportRider](
	[ReportRiderId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[RiderId] [int] NOT NULL,
	[Comments] [ntext] NOT NULL,
	[DateReported] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ReportRider] PRIMARY KEY CLUSTERED 
(
	[ReportRiderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rider]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rider](
	[RiderId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[PhotoUrl] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[ContactNumber] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Longitude] [nvarchar](max) NULL,
	[Latitude] [nvarchar](max) NULL,
	[RiderStatusId] [int] NULL,
	[RatingId] [int] NULL,
	[UserId] [int] NULL,
	[TotalCancelledBooking] [int] NULL,
	[ActivateEmailReceipts] [bit] NOT NULL,
	[IsOnline] [bit] NOT NULL,
 CONSTRAINT [PK_Rider] PRIMARY KEY CLUSTERED 
(
	[RiderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RiderBookingHistory]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RiderBookingHistory](
	[RiderBookingHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[RiderId] [int] NOT NULL,
	[CustomerName] [nvarchar](50) NOT NULL,
	[CustomerNumber] [nvarchar](50) NOT NULL,
	[ReceiverName] [nvarchar](50) NOT NULL,
	[ReceiverNumber] [nvarchar](50) NOT NULL,
	[PickupLocation] [nvarchar](max) NOT NULL,
	[DropOffLocation] [nvarchar](max) NOT NULL,
	[ItemDetails] [ntext] NOT NULL,
	[TotalFare] [money] NOT NULL,
	[TotalKilometers] [nvarchar](10) NOT NULL,
	[RiderShares] [nvarchar](10) NOT NULL,
	[BookingStatusId] [int] NOT NULL,
	[RiderFare] [money] NULL,
	[RiderDeduction] [money] NULL,
	[BookingDate] [datetime] NULL,
 CONSTRAINT [PK_RiderBookingHistory] PRIMARY KEY CLUSTERED 
(
	[RiderBookingHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RiderRating]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RiderRating](
	[RiderRatingId] [int] IDENTITY(1,1) NOT NULL,
	[RiderId] [int] NOT NULL,
	[BlockedCount] [int] NULL,
	[ReportedCount] [int] NULL,
	[Rating] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[Feedback] [nvarchar](max) NULL,
 CONSTRAINT [PK_RiderRating] PRIMARY KEY CLUSTERED 
(
	[RiderRatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RiderStatus]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RiderStatus](
	[RiderStatusId] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_RiderStatus] PRIMARY KEY CLUSTERED 
(
	[RiderStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurchargeSchedule]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurchargeSchedule](
	[SurchargeScheduleId] [int] IDENTITY(1,1) NOT NULL,
	[ScheduleRun] [datetime2](7) NOT NULL,
	[TimeLimit] [int] NOT NULL,
 CONSTRAINT [PK_SurchargeSchedule] PRIMARY KEY CLUSTERED 
(
	[SurchargeScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TermsAndConditions]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TermsAndConditions](
	[TermsAndConditionsId] [int] IDENTITY(1,1) NOT NULL,
	[Content] [ntext] NOT NULL,
 CONSTRAINT [PK_TermsAndConditions] PRIMARY KEY CLUSTERED 
(
	[TermsAndConditionsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[Token] [nvarchar](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInRole]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInRole](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserInRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_UserInRole_RoleId_UserId] UNIQUE NONCLUSTERED 
(
	[RoleId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VehicleDetails]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleDetails](
	[VehicleDetailId] [int] IDENTITY(1,1) NOT NULL,
	[PlateNumber] [nvarchar](30) NOT NULL,
	[ORCR] [nvarchar](50) NOT NULL,
	[Brand] [nvarchar](30) NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Color] [nvarchar](30) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[RiderId] [int] NOT NULL,
 CONSTRAINT [PK_VehicleDetails] PRIMARY KEY CLUSTERED 
(
	[VehicleDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wallet]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wallet](
	[WalletId] [int] IDENTITY(1,1) NOT NULL,
	[RiderId] [int] NOT NULL,
	[CurrentPoints] [money] NOT NULL,
	[WalletStatusId] [int] NOT NULL,
	[DateUpdated] [datetime2](7) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[PointsLoaded] [money] NOT NULL,
	[NegativeBalance] [money] NULL,
 CONSTRAINT [PK_Wallet] PRIMARY KEY CLUSTERED 
(
	[WalletId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WalletLog]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WalletLog](
	[WalletLogId] [int] IDENTITY(1,1) NOT NULL,
	[RiderId] [int] NOT NULL,
	[Points] [money] NOT NULL,
	[CurrentPoints] [money] NOT NULL,
	[CurrentStatus] [nvarchar](30) NOT NULL,
	[LogDate] [datetime] NOT NULL,
 CONSTRAINT [PK_WalletLog] PRIMARY KEY CLUSTERED 
(
	[WalletLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WalletStatus]    Script Date: 11/2/2020 12:14:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WalletStatus](
	[WalletStatusId] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_WalletStatus] PRIMARY KEY CLUSTERED 
(
	[WalletStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppRating]  WITH CHECK ADD  CONSTRAINT [FK_AppRating_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[AppRating] CHECK CONSTRAINT [FK_AppRating_Customer]
GO
ALTER TABLE [dbo].[BlockedCustomer]  WITH CHECK ADD  CONSTRAINT [FK_BlockedCustomer_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[BlockedCustomer] CHECK CONSTRAINT [FK_BlockedCustomer_Customer]
GO
ALTER TABLE [dbo].[BlockedRider]  WITH CHECK ADD  CONSTRAINT [FK_BlockedRider_Rider] FOREIGN KEY([RiderId])
REFERENCES [dbo].[Rider] ([RiderId])
GO
ALTER TABLE [dbo].[BlockedRider] CHECK CONSTRAINT [FK_BlockedRider_Rider]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_BookingStatus] FOREIGN KEY([BookingStatusId])
REFERENCES [dbo].[BookingStatus] ([BookingStatusId])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_BookingStatus]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Fare] FOREIGN KEY([FareId])
REFERENCES [dbo].[Fare] ([FareId])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Fare]
GO
ALTER TABLE [dbo].[BookingHistory]  WITH CHECK ADD  CONSTRAINT [FK_BookingHistory_BookingStatus] FOREIGN KEY([BookingStatusId])
REFERENCES [dbo].[BookingStatus] ([BookingStatusId])
GO
ALTER TABLE [dbo].[BookingHistory] CHECK CONSTRAINT [FK_BookingHistory_BookingStatus]
GO
ALTER TABLE [dbo].[BookingHistory]  WITH CHECK ADD  CONSTRAINT [FK_BookingHistory_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[BookingHistory] CHECK CONSTRAINT [FK_BookingHistory_Customer]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_CustomerRating] FOREIGN KEY([RatingId])
REFERENCES [dbo].[CustomerRating] ([CustomerRatingId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_CustomerRating]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_CustomerStatus] FOREIGN KEY([CustomerStatusId])
REFERENCES [dbo].[CustomerStatus] ([CustomerStatusId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_CustomerStatus]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Fare] FOREIGN KEY([FareId])
REFERENCES [dbo].[Fare] ([FareId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Fare]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_User]
GO
ALTER TABLE [dbo].[CustomerBookingHistory]  WITH CHECK ADD  CONSTRAINT [FK_CustomerBookingHistory_BookingStatus] FOREIGN KEY([BookingStatusId])
REFERENCES [dbo].[BookingStatus] ([BookingStatusId])
GO
ALTER TABLE [dbo].[CustomerBookingHistory] CHECK CONSTRAINT [FK_CustomerBookingHistory_BookingStatus]
GO
ALTER TABLE [dbo].[CustomerBookingHistory]  WITH CHECK ADD  CONSTRAINT [FK_CustomerBookingHistory_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerBookingHistory] CHECK CONSTRAINT [FK_CustomerBookingHistory_Customer]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_User]
GO
ALTER TABLE [dbo].[MenuAccess]  WITH CHECK ADD  CONSTRAINT [FK_MenuAccess_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[MenuAccess] CHECK CONSTRAINT [FK_MenuAccess_Role]
GO
ALTER TABLE [dbo].[ReportCustomer]  WITH CHECK ADD  CONSTRAINT [FK_ReportCustomer_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[ReportCustomer] CHECK CONSTRAINT [FK_ReportCustomer_Customer]
GO
ALTER TABLE [dbo].[ReportCustomer]  WITH CHECK ADD  CONSTRAINT [FK_ReportCustomer_Rider] FOREIGN KEY([RiderId])
REFERENCES [dbo].[Rider] ([RiderId])
GO
ALTER TABLE [dbo].[ReportCustomer] CHECK CONSTRAINT [FK_ReportCustomer_Rider]
GO
ALTER TABLE [dbo].[Rider]  WITH CHECK ADD  CONSTRAINT [FK_Rider_RiderRating] FOREIGN KEY([RatingId])
REFERENCES [dbo].[RiderRating] ([RiderRatingId])
GO
ALTER TABLE [dbo].[Rider] CHECK CONSTRAINT [FK_Rider_RiderRating]
GO
ALTER TABLE [dbo].[Rider]  WITH CHECK ADD  CONSTRAINT [FK_Rider_RiderStatus] FOREIGN KEY([RiderStatusId])
REFERENCES [dbo].[RiderStatus] ([RiderStatusId])
GO
ALTER TABLE [dbo].[Rider] CHECK CONSTRAINT [FK_Rider_RiderStatus]
GO
ALTER TABLE [dbo].[Rider]  WITH CHECK ADD  CONSTRAINT [FK_Rider_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Rider] CHECK CONSTRAINT [FK_Rider_User]
GO
ALTER TABLE [dbo].[RiderBookingHistory]  WITH CHECK ADD  CONSTRAINT [FK_RiderBookingHistory_BookingStatus] FOREIGN KEY([BookingStatusId])
REFERENCES [dbo].[BookingStatus] ([BookingStatusId])
GO
ALTER TABLE [dbo].[RiderBookingHistory] CHECK CONSTRAINT [FK_RiderBookingHistory_BookingStatus]
GO
ALTER TABLE [dbo].[UserInRole]  WITH CHECK ADD  CONSTRAINT [FK_UserInRole_Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserInRole] CHECK CONSTRAINT [FK_UserInRole_Role_RoleId]
GO
ALTER TABLE [dbo].[UserInRole]  WITH CHECK ADD  CONSTRAINT [FK_UserInRole_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserInRole] CHECK CONSTRAINT [FK_UserInRole_User_UserId]
GO
ALTER TABLE [dbo].[VehicleDetails]  WITH CHECK ADD  CONSTRAINT [FK_VehicleDetails_User] FOREIGN KEY([RiderId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[VehicleDetails] CHECK CONSTRAINT [FK_VehicleDetails_User]
GO
ALTER TABLE [dbo].[Wallet]  WITH CHECK ADD  CONSTRAINT [FK_Wallet_Rider] FOREIGN KEY([RiderId])
REFERENCES [dbo].[Rider] ([RiderId])
GO
ALTER TABLE [dbo].[Wallet] CHECK CONSTRAINT [FK_Wallet_Rider]
GO
ALTER TABLE [dbo].[Wallet]  WITH CHECK ADD  CONSTRAINT [FK_Wallet_WalletStatus] FOREIGN KEY([WalletStatusId])
REFERENCES [dbo].[WalletStatus] ([WalletStatusId])
GO
ALTER TABLE [dbo].[Wallet] CHECK CONSTRAINT [FK_Wallet_WalletStatus]
GO

