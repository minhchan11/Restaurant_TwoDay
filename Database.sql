USE [yelp]
GO
/****** Object:  Table [dbo].[cuisines]    Script Date: 2/23/2017 4:49:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cuisines](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[restaurants]    Script Date: 2/23/2017 4:49:40 PM ******/
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
/****** Object:  Table [dbo].[reviews]    Script Date: 2/23/2017 4:49:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reviews](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[review] [varchar](255) NULL,
	[rating] [int] NULL,
	[restaurant_id] [int] NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[cuisines] ON 

INSERT [dbo].[cuisines] ([id], [name]) VALUES (8, N'mexican')
INSERT [dbo].[cuisines] ([id], [name]) VALUES (9, N'chinese')
INSERT [dbo].[cuisines] ([id], [name]) VALUES (10, N'Western')
SET IDENTITY_INSERT [dbo].[cuisines] OFF
SET IDENTITY_INSERT [dbo].[restaurants] ON 

INSERT [dbo].[restaurants] ([id], [name], [fav_dish], [opening_date], [cuisine_id]) VALUES (20, N'wendys', N'chicken', CAST(N'2017-02-01T00:00:00.000' AS DateTime), 9)
SET IDENTITY_INSERT [dbo].[restaurants] OFF
SET IDENTITY_INSERT [dbo].[reviews] ON 

INSERT [dbo].[reviews] ([id], [review], [rating], [restaurant_id]) VALUES (1, N'its good', 3, 20)
INSERT [dbo].[reviews] ([id], [review], [rating], [restaurant_id]) VALUES (2, N'amazing', 5, 20)
SET IDENTITY_INSERT [dbo].[reviews] OFF
