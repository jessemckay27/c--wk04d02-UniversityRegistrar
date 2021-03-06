CREATE DATABASE [registrar]
GO
USE [registrar]
GO
/****** Object:  Table [dbo].[courses]    Script Date: 12/13/2016 4:52:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[courses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[number] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[courses_students]    Script Date: 12/13/2016 4:52:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[courses_students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[course_id] [int] NULL,
	[student_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[students]    Script Date: 12/13/2016 4:52:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[date] [datetime] NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[courses] ON 

INSERT [dbo].[courses] ([id], [name], [number]) VALUES (1, N'History', N'HIST100')
INSERT [dbo].[courses] ([id], [name], [number]) VALUES (2, N'Japanese', N'JAPN100')
INSERT [dbo].[courses] ([id], [name], [number]) VALUES (3, N'Horticulture', N'HORT101')
INSERT [dbo].[courses] ([id], [name], [number]) VALUES (4, N'Advanced Japanese', N'JAPN301')
SET IDENTITY_INSERT [dbo].[courses] OFF
SET IDENTITY_INSERT [dbo].[courses_students] ON 

INSERT [dbo].[courses_students] ([id], [course_id], [student_id]) VALUES (1, 1, 1)
INSERT [dbo].[courses_students] ([id], [course_id], [student_id]) VALUES (2, 1, 2)
INSERT [dbo].[courses_students] ([id], [course_id], [student_id]) VALUES (3, 1, 3)
INSERT [dbo].[courses_students] ([id], [course_id], [student_id]) VALUES (4, 1, 1)
INSERT [dbo].[courses_students] ([id], [course_id], [student_id]) VALUES (5, 2, 1)
INSERT [dbo].[courses_students] ([id], [course_id], [student_id]) VALUES (6, 2, 2)
INSERT [dbo].[courses_students] ([id], [course_id], [student_id]) VALUES (7, 2, 3)
INSERT [dbo].[courses_students] ([id], [course_id], [student_id]) VALUES (8, 4, 2)
SET IDENTITY_INSERT [dbo].[courses_students] OFF
SET IDENTITY_INSERT [dbo].[students] ON 

INSERT [dbo].[students] ([id], [name], [date]) VALUES (1, N'Annie', CAST(N'2014-03-02T00:00:00.000' AS DateTime))
INSERT [dbo].[students] ([id], [name], [date]) VALUES (2, N'Bryant', CAST(N'2013-04-09T00:00:00.000' AS DateTime))
INSERT [dbo].[students] ([id], [name], [date]) VALUES (3, N'Steven', CAST(N'2012-12-12T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[students] OFF
