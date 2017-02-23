USE [yelp]
GO
/****** Object:  Table [dbo].[cuisines]    Script Date: 2/22/2017 4:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cuisines](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[restaurants]    Script Date: 2/22/2017 4:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[restaurants](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[fav_dish] [varchar](255) NULL,
	[opening_date] [datetime] NULL,
	[cuisine_id] [int] NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[cuisines] ON 

INSERT [dbo].[cuisines] ([id], [name]) VALUES (4, N'chinese')
INSERT [dbo].[cuisines] ([id], [name]) VALUES (6, N'mexican')
INSERT [dbo].[cuisines] ([id], [name]) VALUES (7, N'western')
SET IDENTITY_INSERT [dbo].[cuisines] OFF
SET IDENTITY_INSERT [dbo].[restaurants] ON 

INSERT [dbo].[restaurants] ([id], [name], [fav_dish], [opening_date], [cuisine_id]) VALUES (10, N'fsdfad', N'fdsfad', CAST(N'2017-02-09T00:00:00.000' AS DateTime), 6)
SET IDENTITY_INSERT [dbo].[restaurants] OFF
