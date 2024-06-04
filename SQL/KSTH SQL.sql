USE [KSTH]
GO
SET IDENTITY_INSERT [dbo].[Lteacher] ON 

INSERT [dbo].[Lteacher] ([TeacherId], [Name], [Username], [Password], [Department], [Email], [PhoneNumber]) VALUES (1, N'Nguyen Van Teacher', N'Teacher123', N'123456', N'209', N'Teacher1@fpt.edu.vn', N'0987654321')
SET IDENTITY_INSERT [dbo].[Lteacher] OFF
GO
SET IDENTITY_INSERT [dbo].[Majors] ON 

INSERT [dbo].[Majors] ([MajorId], [MajorName]) VALUES (1, N'Cong nghe thong tin')
SET IDENTITY_INSERT [dbo].[Majors] OFF
GO
SET IDENTITY_INSERT [dbo].[Lstudent] ON 

INSERT [dbo].[Lstudent] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password]) VALUES (1, N'Doan van toan', CAST(N'2004-09-17T00:00:00.0000000' AS DateTime2), N'Bac Ninh', N'967327356', N'Doanvantoan3365@gmail.com', 1, N'vantoan123', N'123456')
SET IDENTITY_INSERT [dbo].[Lstudent] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240523085241_asm', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240523101711_asm2', N'8.0.4')
GO
SET IDENTITY_INSERT [dbo].[Ladmin] ON 

INSERT [dbo].[Ladmin] ([Id], [Username], [Password]) VALUES (1, N'Admin123', N'123456')
SET IDENTITY_INSERT [dbo].[Ladmin] OFF
GO
