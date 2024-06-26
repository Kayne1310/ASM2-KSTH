USE [master]
GO
/****** Object:  Database [KSTH]    Script Date: 6/4/2024 9:52:27 PM ******/
CREATE DATABASE [KSTH]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'KSTH', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\KSTH_Primary.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'KSTH_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\KSTH_Primary.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [KSTH] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [KSTH].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [KSTH] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [KSTH] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [KSTH] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [KSTH] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [KSTH] SET ARITHABORT OFF 
GO
ALTER DATABASE [KSTH] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [KSTH] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [KSTH] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [KSTH] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [KSTH] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [KSTH] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [KSTH] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [KSTH] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [KSTH] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [KSTH] SET  ENABLE_BROKER 
GO
ALTER DATABASE [KSTH] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [KSTH] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [KSTH] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [KSTH] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [KSTH] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [KSTH] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [KSTH] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [KSTH] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [KSTH] SET  MULTI_USER 
GO
ALTER DATABASE [KSTH] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [KSTH] SET DB_CHAINING OFF 
GO
ALTER DATABASE [KSTH] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [KSTH] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [KSTH] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [KSTH] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'KSTH', N'ON'
GO
ALTER DATABASE [KSTH] SET QUERY_STORE = ON
GO
ALTER DATABASE [KSTH] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [KSTH]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/4/2024 9:52:27 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 6/4/2024 9:52:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](255) NULL,
	[Password] [nvarchar](max) NOT NULL,
	[RoleId] [int] NULL,
 CONSTRAINT [PK_Ladmin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 6/4/2024 9:52:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[ClassId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[TeacherId] [int] NOT NULL,
	[Semester] [nvarchar](max) NULL,
	[Year] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
	[ClassName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Classes] PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 6/4/2024 9:52:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [nvarchar](max) NULL,
	[CourseDescription] [nvarchar](max) NULL,
	[Credits] [int] NOT NULL,
	[MajorId] [int] NOT NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enrollments]    Script Date: 6/4/2024 9:52:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enrollments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[ClassId] [int] NOT NULL,
 CONSTRAINT [PK_Enrollments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grades]    Script Date: 6/4/2024 9:52:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grades](
	[GradeId] [int] IDENTITY(1,1) NOT NULL,
	[EnrollmentId] [int] NOT NULL,
	[Grade1] [decimal](3, 2) NULL,
	[CourseId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Majors]    Script Date: 6/4/2024 9:52:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Majors](
	[MajorId] [int] IDENTITY(1,1) NOT NULL,
	[MajorName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Majors] PRIMARY KEY CLUSTERED 
(
	[MajorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 6/4/2024 9:52:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 6/4/2024 9:52:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[RoomId] [int] IDENTITY(1,1) NOT NULL,
	[RoomNumber] [nvarchar](max) NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 6/4/2024 9:52:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[MajorId] [int] NOT NULL,
	[Username] [varchar](255) NULL,
	[Password] [nvarchar](max) NOT NULL,
	[RandomKey] [nvarchar](55) NULL,
	[RoleId] [int] NULL,
 CONSTRAINT [PK_Lstudent] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 6/4/2024 9:52:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[TeacherId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Username] [varchar](255) NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[RandomKey] [nvarchar](max) NULL,
	[RoleId] [int] NULL,
 CONSTRAINT [PK_Lteacher] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Admins] ON 

INSERT [dbo].[Admins] ([Id], [Username], [Password], [RoleId]) VALUES (1, N'Admin', N'123456', NULL)
INSERT [dbo].[Admins] ([Id], [Username], [Password], [RoleId]) VALUES (2, N'Admin1', N'123456', 3)
SET IDENTITY_INSERT [dbo].[Admins] OFF
GO
SET IDENTITY_INSERT [dbo].[Classes] ON 

INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (15, 2, 1, N'Fall', 2024, 1, N'English 101')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (16, 3, 2, N'Summer', 2024, 2, N'History 101')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (17, 4, 3, N'Winter', 2024, 3, N'Science 101')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (18, 5, 4, N'Spring', 2024, 4, N'Art 101')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (19, 6, 5, N'Fall', 2024, 5, N'Music 101')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (20, 7, 6, N'Summer', 2024, 6, N'PE 101')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (21, 8, 7, N'Winter', 2024, 7, N'Math 102')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (22, 9, 8, N'Spring', 2024, 8, N'English 102')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (23, 10, 9, N'Fall', 2024, 9, N'History 102')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (24, 11, 10, N'Spring', 2024, 10, N'Math 101')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (25, 7, 6, N'Fall', 2024, 1, N'Kayne')
SET IDENTITY_INSERT [dbo].[Classes] OFF
GO
SET IDENTITY_INSERT [dbo].[Courses] ON 

INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (2, N'Math 101', N'Basic Mathematics', 3, 1)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (3, N'English 101', N'Basic English', 3, 2)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (4, N'History 101', N'World History', 3, 3)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (5, N'Science 101', N'Basic Science', 3, 4)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (6, N'Art 101', N'Introduction to Art', 3, 5)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (7, N'Music 101', N'Introduction to Music', 3, 6)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (8, N'PE 101', N'Physical Education', 3, 7)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (9, N'Math 102', N'Advanced Mathematics', 3, 1)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (10, N'English 102', N'Advanced English', 3, 2)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (11, N'History 102', N'Advanced History', 3, 3)
SET IDENTITY_INSERT [dbo].[Courses] OFF
GO
SET IDENTITY_INSERT [dbo].[Enrollments] ON 

INSERT [dbo].[Enrollments] ([Id], [StudentId], [ClassId]) VALUES (1, 1, 15)
INSERT [dbo].[Enrollments] ([Id], [StudentId], [ClassId]) VALUES (2, 2, 16)
INSERT [dbo].[Enrollments] ([Id], [StudentId], [ClassId]) VALUES (3, 3, 17)
INSERT [dbo].[Enrollments] ([Id], [StudentId], [ClassId]) VALUES (4, 4, 18)
INSERT [dbo].[Enrollments] ([Id], [StudentId], [ClassId]) VALUES (5, 5, 19)
INSERT [dbo].[Enrollments] ([Id], [StudentId], [ClassId]) VALUES (6, 6, 20)
INSERT [dbo].[Enrollments] ([Id], [StudentId], [ClassId]) VALUES (7, 7, 21)
INSERT [dbo].[Enrollments] ([Id], [StudentId], [ClassId]) VALUES (8, 8, 22)
INSERT [dbo].[Enrollments] ([Id], [StudentId], [ClassId]) VALUES (9, 9, 23)
INSERT [dbo].[Enrollments] ([Id], [StudentId], [ClassId]) VALUES (10, 10, 24)
INSERT [dbo].[Enrollments] ([Id], [StudentId], [ClassId]) VALUES (11, 1, 19)
INSERT [dbo].[Enrollments] ([Id], [StudentId], [ClassId]) VALUES (12, 1, 19)
INSERT [dbo].[Enrollments] ([Id], [StudentId], [ClassId]) VALUES (13, 1, 25)
SET IDENTITY_INSERT [dbo].[Enrollments] OFF
GO
SET IDENTITY_INSERT [dbo].[Grades] ON 

INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (1, 1, CAST(3.50 AS Decimal(3, 2)), 1)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (2, 2, CAST(8.00 AS Decimal(3, 2)), 2)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (3, 3, CAST(8.00 AS Decimal(3, 2)), 3)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (4, 4, CAST(3.80 AS Decimal(3, 2)), 4)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (5, 5, CAST(8.00 AS Decimal(3, 2)), 5)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (6, 6, CAST(3.70 AS Decimal(3, 2)), 6)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (7, 7, CAST(3.90 AS Decimal(3, 2)), 7)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (8, 8, CAST(3.10 AS Decimal(3, 2)), 8)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (9, 9, CAST(3.40 AS Decimal(3, 2)), 9)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (10, 10, CAST(3.60 AS Decimal(3, 2)), 10)
SET IDENTITY_INSERT [dbo].[Grades] OFF
GO
SET IDENTITY_INSERT [dbo].[Majors] ON 

INSERT [dbo].[Majors] ([MajorId], [MajorName]) VALUES (1, N'Computer Science')
INSERT [dbo].[Majors] ([MajorId], [MajorName]) VALUES (2, N'Business Administration')
INSERT [dbo].[Majors] ([MajorId], [MajorName]) VALUES (3, N'Mechanical Engineering')
INSERT [dbo].[Majors] ([MajorId], [MajorName]) VALUES (4, N'Electrical Engineering')
INSERT [dbo].[Majors] ([MajorId], [MajorName]) VALUES (5, N'Civil Engineering')
INSERT [dbo].[Majors] ([MajorId], [MajorName]) VALUES (6, N'Biology')
INSERT [dbo].[Majors] ([MajorId], [MajorName]) VALUES (7, N'Chemistry')
INSERT [dbo].[Majors] ([MajorId], [MajorName]) VALUES (8, N'Physics')
INSERT [dbo].[Majors] ([MajorId], [MajorName]) VALUES (9, N'Mathematics')
INSERT [dbo].[Majors] ([MajorId], [MajorName]) VALUES (10, N'English Literature')
SET IDENTITY_INSERT [dbo].[Majors] OFF
GO
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (1, N'Students')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (2, N'Teachers')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (3, N'Admins')
GO
SET IDENTITY_INSERT [dbo].[Rooms] ON 

INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (1, N'101')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (2, N'102')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (3, N'103')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (4, N'104')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (5, N'105')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (6, N'201')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (7, N'202')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (8, N'203')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (9, N'204')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (10, N'205')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (11, N'C306')
SET IDENTITY_INSERT [dbo].[Rooms] OFF
GO
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (1, N'Student1', CAST(N'2024-06-15T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0974764764', N'Student1@gmail.com', 1, N'student1', N'73d93ee19d2a02a866fa2427dd2b394d', N'vQnDw', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (2, N'Student2', CAST(N'2024-06-20T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0845675476', N'Student2@gmail.com', 2, N'student2', N'c92bf951c43bb489ec8c05dfa3300ae5', N'SpH!N', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (3, N'Student3', CAST(N'2024-06-05T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0876427642', N'Student3@gmail.com', 3, N'student3', N'9a10d89956d95f574f39015cb8a23c0c', N'LLQk!', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (4, N'Student4', CAST(N'2024-06-06T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0247454574', N'Student4@gmail.com', 4, N'student4', N'f9c857e37e1c1b67cb29fe4d6d9f7a55', N'kGnju', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (5, N'Student5', CAST(N'2024-06-11T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0976724672', N'Student5@gmail.com', 5, N'student5', N'16bd7bbc24400ef97a08ff673002803e', N'pIEta', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (6, N'Student6', CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0846754724', N'Student6@gmail.com', 6, N'student6', N'5e5a7eb842a6fe277e9f4421143efbfd', N'fIzmH', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (7, N'Student7', CAST(N'2024-06-05T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0965656227', N'Student7@gmail.com', 7, N'student7', N'58d3380468dac8feb8e8f59e08315397', N'isowl', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (8, N'Student8', CAST(N'2024-06-05T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0864576247', N'Student8@gmail.com', 8, N'student8', N'da2cac0bf9fabd47704c31755353ce0e', N'HRnis', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (9, N'Student9', CAST(N'2024-06-13T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0962321213', N'Student9@gmail.com', 9, N'student9', N'fb4cacafc1ed9da28b14e49e48c6103c', N'OjMPR', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (10, N'Student10', CAST(N'2024-06-24T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0244654645', N'Student10@gmail.com', 10, N'student10', N'd0cffc0ac1132705c9cb49f6c87ec7e7', N'tBEjQ', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (11, N'qưqeq', CAST(N'2024-06-06T00:00:00.0000000' AS DateTime2), N'qeqweq', N'0967676767', N'dque@gmail', 1, N'Toan1', N'f3fffd99d5473bc2b652e189fc8303b2', N'x!mdd', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (16, N'qưeiqqwe', CAST(N'2024-06-05T00:00:00.0000000' AS DateTime2), N'Bacnib', N'0964276724', N'zxczx@gmail.com', 1, N'dvt123', N'1859732d1d78bfd915946fba107f0555', N'nVZwX', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (17, N'cxvxcvxv', CAST(N'2024-06-13T00:00:00.0000000' AS DateTime2), N'xcvxcvxv', N'0941411414', N'xvcvxv@gmail.com', 1, N'dvt124', N'02364bf14ccdc3ff99803356ce0d1cb4', N'WnASY', 1)
SET IDENTITY_INSERT [dbo].[Students] OFF
GO
SET IDENTITY_INSERT [dbo].[Teachers] ON 

INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (1, N'Teacher1', N'teacher1', N'a7a66b8c1f92ee594ff85ca682fb18a3', N'Ha noi', N'Teacher1@fpt.edu.vn', N'0987654321', N'MDYRH', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (2, N'Teacher2', N'teacher2', N'7ff8885dd0551d2116e53dace8f470a8', N'Ha noi', N'Teacher2@fpt.edu.vn', N'0987654321', N'DDpbK', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (3, N'Teacher3', N'teacher3', N'1216162799a7525e45d11c755605525d', N'Ha noi', N'Teacher3@fpt.edu.vn', N'0987654321', N'ZaUye', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (4, N'Teacher4', N'teacher4', N'009e89c5d16a1ba8f63650d727070507', N'Ha noi', N'Teacher4@fpt.edu.vn', N'0987654321', N'lfkGw', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (5, N'Teacher5', N'teacher5', N'6934a3694af75cf01992d93af705f5b6', N'Ha noi', N'Teacher5@fpt.edu.vn', N'0987654321', N'GYCJU', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (6, N'Teacher6', N'teacher6', N'045a83738ad43a2524feb0dfaace9168', N'Ha noi', N'Teacher6@fpt.edu.vn', N'0987654321', N'FeHXm', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (7, N'Teacher7', N'teacher7', N'6d0d94370e0aac0a96fce10ed49d0cb1', N'Ha noi', N'Teacher7@fpt.edu.vn', N'0987654321', N'lykjx', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (8, N'Teacher8', N'teacher8', N'1ea48d330fb9ceac6dc27a925047db31', N'Ha noi', N'Teacher8@fpt.edu.vn', N'0987654321', N'RaJCi', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (9, N'Teacher9', N'teacher9', N'c83d7d65b966e2d86fd192096917b9d4', N'Ha noi', N'Teacher9@fpt.edu.vn', N'0987654321', N'CFyPK', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (10, N'Teacher10', N'teacher10', N'19de7e42a2077c29cd9491d56b86e1f0', N'Ha noi', N'Teacher10@fpt.edu.vn', N'0987654321', N'yr!WP', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (13, N'ncznxczm', N'toan222', N'5e089c71b0c5ec361ece086ed51c4cf1', N'zcbmzx', N'cbzzxcz@gmail.com', N'0932215561', N'VQHeD', 2)
SET IDENTITY_INSERT [dbo].[Teachers] OFF
GO
/****** Object:  Index [IX_Classes_CourseId]    Script Date: 6/4/2024 9:52:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_Classes_CourseId] ON [dbo].[Classes]
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_RoomId]    Script Date: 6/4/2024 9:52:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_Classes_RoomId] ON [dbo].[Classes]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_TeacherId]    Script Date: 6/4/2024 9:52:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_Classes_TeacherId] ON [dbo].[Classes]
(
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Courses_MajorId]    Script Date: 6/4/2024 9:52:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_Courses_MajorId] ON [dbo].[Courses]
(
	[MajorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Enrollments_ClassId]    Script Date: 6/4/2024 9:52:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_Enrollments_ClassId] ON [dbo].[Enrollments]
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Enrollments_StudentId]    Script Date: 6/4/2024 9:52:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_Enrollments_StudentId] ON [dbo].[Enrollments]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Lstudent_MajorId]    Script Date: 6/4/2024 9:52:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_Lstudent_MajorId] ON [dbo].[Students]
(
	[MajorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admins] ADD  DEFAULT (N'') FOR [Password]
GO
ALTER TABLE [dbo].[Students] ADD  CONSTRAINT [ConstraintName]  DEFAULT ('') FOR [Username]
GO
ALTER TABLE [dbo].[Teachers] ADD  CONSTRAINT [DF__Lteacher__Username__6E01572D]  DEFAULT ('') FOR [Username]
GO
ALTER TABLE [dbo].[Teachers] ADD  DEFAULT (N'') FOR [Password]
GO
ALTER TABLE [dbo].[Admins]  WITH CHECK ADD  CONSTRAINT [FK_Admins_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Admins] CHECK CONSTRAINT [FK_Admins_Roles]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_Courses_CourseId] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([CourseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_Courses_CourseId]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_Lteacher_TeacherId] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teachers] ([TeacherId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_Lteacher_TeacherId]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_Rooms_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([RoomId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_Rooms_RoomId]
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD  CONSTRAINT [FK_Courses_Majors_MajorId] FOREIGN KEY([MajorId])
REFERENCES [dbo].[Majors] ([MajorId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Courses] CHECK CONSTRAINT [FK_Courses_Majors_MajorId]
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD  CONSTRAINT [FK_Enrollments_Classes_ClassId] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Classes] ([ClassId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Enrollments] CHECK CONSTRAINT [FK_Enrollments_Classes_ClassId]
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD  CONSTRAINT [FK_Enrollments_Lstudent_StudentId] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([StudentId])
GO
ALTER TABLE [dbo].[Enrollments] CHECK CONSTRAINT [FK_Enrollments_Lstudent_StudentId]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Lstudent_Majors_MajorId] FOREIGN KEY([MajorId])
REFERENCES [dbo].[Majors] ([MajorId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Lstudent_Majors_MajorId]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Roles]
GO
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD  CONSTRAINT [FK_Teachers_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Teachers] CHECK CONSTRAINT [FK_Teachers_Roles]
GO
USE [master]
GO
ALTER DATABASE [KSTH] SET  READ_WRITE 
GO
