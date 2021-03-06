CREATE DATABASE [dbGameOfDrones]
GO
USE [dbGameOfDrones]
GO
/****** Object:  Table [dbo].[Logging]    Script Date: 17/09/2017 7:08:08 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Logging](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[FullException] [varchar](max) NOT NULL,
	[SimpleError] [varchar](max) NOT NULL,
	[ModuleName] [varchar](max) NOT NULL,
	[RegisterDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Players]    Script Date: 17/09/2017 7:08:08 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Players](
	[PlayerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[Score] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PlayerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Players] ADD  DEFAULT ((1)) FOR [Score]
GO
