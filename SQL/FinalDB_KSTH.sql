USE [master]
GO
/****** Object:  Database [KSTH]    Script Date: 7/9/2024 12:42:24 AM ******/
CREATE DATABASE [KSTH]
 
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
ALTER DATABASE [KSTH] SET QUERY_STORE = ON
GO
ALTER DATABASE [KSTH] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [KSTH]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/9/2024 12:42:24 AM ******/
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
/****** Object:  Table [dbo].[Admins]    Script Date: 7/9/2024 12:42:24 AM ******/
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
/****** Object:  Table [dbo].[Attendance]    Script Date: 7/9/2024 12:42:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StudentID] [int] NULL,
	[ClassID] [int] NULL,
	[EnrollmentID] [int] NULL,
	[TeacherID] [int] NULL,
	[RoomID] [int] NULL,
	[AttendanceDate] [date] NOT NULL,
	[AttendanceStatus] [varchar](20) NOT NULL,
	[Reason] [varchar](250) NULL,
	[NumId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 7/9/2024 12:42:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[ClassId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[TeacherId] [int] NULL,
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
/****** Object:  Table [dbo].[Courses]    Script Date: 7/9/2024 12:42:24 AM ******/
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
/****** Object:  Table [dbo].[Enrollments]    Script Date: 7/9/2024 12:42:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enrollments](
	[EnrollmentId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[ClassId] [int] NOT NULL,
 CONSTRAINT [PK_Enrollments] PRIMARY KEY CLUSTERED 
(
	[EnrollmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grades]    Script Date: 7/9/2024 12:42:24 AM ******/
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
/****** Object:  Table [dbo].[Majors]    Script Date: 7/9/2024 12:42:24 AM ******/
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
/****** Object:  Table [dbo].[Num_Session]    Script Date: 7/9/2024 12:42:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Num_Session](
	[NumId] [int] IDENTITY(1,1) NOT NULL,
	[Numses] [varchar](255) NOT NULL,
	[CourseId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[NumId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 7/9/2024 12:42:24 AM ******/
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
/****** Object:  Table [dbo].[Rooms]    Script Date: 7/9/2024 12:42:24 AM ******/
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
/****** Object:  Table [dbo].[Schedules]    Script Date: 7/9/2024 12:42:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[ScheduleId] [int] IDENTITY(1,1) NOT NULL,
	[EnrollmentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
	[Day] [date] NOT NULL,
	[SlotId] [int] NOT NULL,
	[TeacherId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slots]    Script Date: 7/9/2024 12:42:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slots](
	[SlotId] [int] IDENTITY(1,1) NOT NULL,
	[SlotName] [nvarchar](max) NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
 CONSTRAINT [PK_Slots] PRIMARY KEY CLUSTERED 
(
	[SlotId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 7/9/2024 12:42:24 AM ******/
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
/****** Object:  Table [dbo].[Teachers]    Script Date: 7/9/2024 12:42:24 AM ******/
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

INSERT [dbo].[Admins] ([Id], [Username], [Password], [RoleId]) VALUES (1, N'Admin', N'123456', 3)
INSERT [dbo].[Admins] ([Id], [Username], [Password], [RoleId]) VALUES (2, N'Admin1', N'123456', 3)
SET IDENTITY_INSERT [dbo].[Admins] OFF
GO
SET IDENTITY_INSERT [dbo].[Attendance] ON 

INSERT [dbo].[Attendance] ([ID], [StudentID], [ClassID], [EnrollmentID], [TeacherID], [RoomID], [AttendanceDate], [AttendanceStatus], [Reason], [NumId]) VALUES (1, 17, 19, 20, 5, 5, CAST(N'2024-07-08' AS Date), N'present', N'', 1)
INSERT [dbo].[Attendance] ([ID], [StudentID], [ClassID], [EnrollmentID], [TeacherID], [RoomID], [AttendanceDate], [AttendanceStatus], [Reason], [NumId]) VALUES (2, 11, 19, 18, 5, 5, CAST(N'2024-07-08' AS Date), N'present', N'', 1)
INSERT [dbo].[Attendance] ([ID], [StudentID], [ClassID], [EnrollmentID], [TeacherID], [RoomID], [AttendanceDate], [AttendanceStatus], [Reason], [NumId]) VALUES (3, 1, 19, 17, 5, 5, CAST(N'2024-07-08' AS Date), N'present', N'', 1)
INSERT [dbo].[Attendance] ([ID], [StudentID], [ClassID], [EnrollmentID], [TeacherID], [RoomID], [AttendanceDate], [AttendanceStatus], [Reason], [NumId]) VALUES (4, 5, 19, 5, 5, 5, CAST(N'2024-07-08' AS Date), N'present', N'', 1)
INSERT [dbo].[Attendance] ([ID], [StudentID], [ClassID], [EnrollmentID], [TeacherID], [RoomID], [AttendanceDate], [AttendanceStatus], [Reason], [NumId]) VALUES (5, 1, 15, 1, 1, 1, CAST(N'2024-07-09' AS Date), N'present', N'abc', 17)
INSERT [dbo].[Attendance] ([ID], [StudentID], [ClassID], [EnrollmentID], [TeacherID], [RoomID], [AttendanceDate], [AttendanceStatus], [Reason], [NumId]) VALUES (6, 18, 15, 21, 1, 1, CAST(N'2024-07-09' AS Date), N'present', N'', 16)
INSERT [dbo].[Attendance] ([ID], [StudentID], [ClassID], [EnrollmentID], [TeacherID], [RoomID], [AttendanceDate], [AttendanceStatus], [Reason], [NumId]) VALUES (7, 19, 15, 22, 1, 1, CAST(N'2024-07-09' AS Date), N'present', N'', 16)
SET IDENTITY_INSERT [dbo].[Attendance] OFF
GO
SET IDENTITY_INSERT [dbo].[Classes] ON 

INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (15, 2, 1, N'Fall', 2024, 1, N'SE06255')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (16, 3, 2, N'Summer', 2024, 2, N'SE06202')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (17, 4, 3, N'Winter', 2024, 3, N'SE06203')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (18, 5, 4, N'Spring', 2024, 11, N'SE06204')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (19, 6, 5, N'Fall', 2024, 5, N'SE06205')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (20, 7, 6, N'Summer', 2024, 6, N'SE06206')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (21, 8, 7, N'Winter', 2024, 7, N'SE06207')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (22, 9, 8, N'Spring', 2024, 8, N'SE06208')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (23, 10, 9, N'Fall', 2024, 9, N'SE06209')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (24, 11, 10, N'Spring', 2024, 10, N'SE06301')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (25, 7, 6, N'Fall', 2024, 1, N'SE06302')
INSERT [dbo].[Classes] ([ClassId], [CourseId], [TeacherId], [Semester], [Year], [RoomId], [ClassName]) VALUES (26, 2, 1, N'Fall', 2004, 1, N'hhh')
SET IDENTITY_INSERT [dbo].[Classes] OFF
GO
SET IDENTITY_INSERT [dbo].[Courses] ON 

INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (2, N'Math', N'Basic Mathematics', 3, 1)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (3, N'English', N'Basic English', 3, 2)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (4, N'History', N'World History', 3, 3)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (5, N'Science', N'Basic Science', 3, 4)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (6, N'Art', N'Introduction to Art', 3, 5)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (7, N'Music', N'Introduction to Music', 3, 6)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (8, N'PE', N'Physical Education', 3, 7)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (9, N'Math', N'Advanced Mathematics', 3, 1)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (10, N'English', N'Advanced English', 3, 2)
INSERT [dbo].[Courses] ([CourseId], [CourseName], [CourseDescription], [Credits], [MajorId]) VALUES (11, N'History', N'Advanced History', 3, 3)
SET IDENTITY_INSERT [dbo].[Courses] OFF
GO
SET IDENTITY_INSERT [dbo].[Enrollments] ON 

INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (1, 1, 15)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (2, 2, 16)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (3, 3, 17)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (4, 4, 18)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (5, 5, 19)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (6, 6, 20)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (7, 7, 21)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (8, 8, 22)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (9, 9, 23)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (10, 10, 24)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (13, 1, 25)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (16, 1, 18)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (17, 1, 19)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (18, 11, 19)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (19, 16, 25)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (20, 17, 19)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (21, 18, 15)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (22, 19, 15)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (23, 20, 23)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (24, 21, 21)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (25, 22, 25)
INSERT [dbo].[Enrollments] ([EnrollmentId], [StudentId], [ClassId]) VALUES (26, 23, 25)
SET IDENTITY_INSERT [dbo].[Enrollments] OFF
GO
SET IDENTITY_INSERT [dbo].[Grades] ON 

INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (45, 1, CAST(9.00 AS Decimal(3, 2)), 2)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (46, 21, CAST(2.00 AS Decimal(3, 2)), 2)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (3, 3, CAST(9.00 AS Decimal(3, 2)), 3)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (4, 4, CAST(3.80 AS Decimal(3, 2)), 4)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (5, 5, CAST(9.00 AS Decimal(3, 2)), 5)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (6, 6, CAST(3.70 AS Decimal(3, 2)), 6)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (7, 7, CAST(3.90 AS Decimal(3, 2)), 7)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (8, 8, CAST(3.10 AS Decimal(3, 2)), 8)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (9, 9, CAST(3.40 AS Decimal(3, 2)), 9)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (10, 10, CAST(3.60 AS Decimal(3, 2)), 10)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (15, 19, CAST(8.00 AS Decimal(3, 2)), 7)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (27, 16, CAST(9.00 AS Decimal(3, 2)), 5)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (22, 20, CAST(8.00 AS Decimal(3, 2)), 6)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (23, 17, CAST(9.00 AS Decimal(3, 2)), 6)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (25, 18, CAST(6.00 AS Decimal(3, 2)), 6)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (40, 25, CAST(5.00 AS Decimal(3, 2)), 7)
INSERT [dbo].[Grades] ([GradeId], [EnrollmentId], [Grade1], [CourseId]) VALUES (42, 22, CAST(8.00 AS Decimal(3, 2)), 2)
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
SET IDENTITY_INSERT [dbo].[Num_Session] ON 

INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (1, N'1', 6)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (2, N'2', 6)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (3, N'3', 6)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (4, N'4', 6)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (5, N'5', 6)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (6, N'6', 6)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (7, N'7', 6)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (8, N'8', 6)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (9, N'9', 6)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (10, N'10', 6)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (11, N'11', 6)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (12, N'12', 6)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (13, N'13', 6)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (14, N'14', 6)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (15, N'15', 6)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (16, N'1', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (17, N'2', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (18, N'3', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (19, N'4', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (20, N'5', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (21, N'6', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (22, N'7', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (23, N'8', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (24, N'9', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (25, N'10', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (26, N'11', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (27, N'12', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (28, N'13', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (29, N'14', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (30, N'15', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (31, N'16', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (32, N'17', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (33, N'18', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (34, N'19', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (35, N'20', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (36, N'21', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (37, N'22', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (38, N'23', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (39, N'24', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (40, N'25', 2)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (41, N'1', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (42, N'2', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (43, N'3', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (44, N'4', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (45, N'5', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (46, N'6', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (47, N'7', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (48, N'8', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (49, N'9', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (50, N'10', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (51, N'11', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (52, N'12', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (53, N'13', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (54, N'14', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (55, N'15', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (56, N'16', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (57, N'17', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (58, N'18', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (59, N'19', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (60, N'20', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (61, N'21', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (62, N'22', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (63, N'23', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (64, N'24', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (65, N'25', 3)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (66, N'1', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (67, N'2', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (68, N'3', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (69, N'4', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (70, N'5', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (71, N'6', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (72, N'7', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (73, N'8', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (74, N'9', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (75, N'10', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (76, N'11', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (77, N'12', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (78, N'13', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (79, N'14', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (80, N'15', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (81, N'16', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (82, N'17', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (83, N'18', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (84, N'19', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (85, N'20', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (86, N'21', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (87, N'22', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (88, N'23', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (89, N'24', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (90, N'25', 4)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (91, N'1', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (92, N'2', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (93, N'3', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (94, N'4', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (95, N'5', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (96, N'6', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (97, N'7', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (98, N'8', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (99, N'9', 5)
GO
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (100, N'10', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (101, N'11', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (102, N'12', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (103, N'13', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (104, N'14', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (105, N'15', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (106, N'16', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (107, N'17', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (108, N'18', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (109, N'19', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (110, N'20', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (111, N'21', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (112, N'22', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (113, N'23', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (114, N'24', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (115, N'25', 5)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (116, N'1', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (117, N'2', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (118, N'3', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (119, N'4', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (120, N'5', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (121, N'6', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (122, N'7', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (123, N'8', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (124, N'9', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (125, N'10', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (126, N'11', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (127, N'12', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (128, N'13', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (129, N'14', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (130, N'15', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (131, N'16', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (132, N'17', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (133, N'18', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (134, N'19', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (135, N'20', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (136, N'21', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (137, N'22', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (138, N'23', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (139, N'24', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (140, N'25', 7)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (141, N'1', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (142, N'2', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (143, N'3', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (144, N'4', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (145, N'5', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (146, N'6', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (147, N'7', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (148, N'8', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (149, N'9', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (150, N'10', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (151, N'11', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (152, N'12', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (153, N'13', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (154, N'14', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (155, N'15', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (156, N'16', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (157, N'17', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (158, N'18', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (159, N'19', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (160, N'20', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (161, N'21', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (162, N'22', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (163, N'23', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (164, N'24', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (165, N'25', 8)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (166, N'1', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (167, N'2', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (168, N'3', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (169, N'4', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (170, N'5', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (171, N'6', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (172, N'7', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (173, N'8', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (174, N'9', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (175, N'10', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (176, N'11', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (177, N'12', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (178, N'13', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (179, N'14', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (180, N'15', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (181, N'16', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (182, N'17', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (183, N'18', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (184, N'19', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (185, N'20', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (186, N'21', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (187, N'22', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (188, N'23', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (189, N'24', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (190, N'25', 10)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (191, N'1', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (192, N'2', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (193, N'3', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (194, N'4', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (195, N'5', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (196, N'6', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (197, N'7', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (198, N'8', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (199, N'9', 11)
GO
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (200, N'10', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (201, N'11', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (202, N'12', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (203, N'13', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (204, N'14', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (205, N'15', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (206, N'16', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (207, N'17', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (208, N'18', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (209, N'19', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (210, N'20', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (211, N'21', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (212, N'22', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (213, N'23', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (214, N'24', 11)
INSERT [dbo].[Num_Session] ([NumId], [Numses], [CourseId]) VALUES (215, N'25', 11)
SET IDENTITY_INSERT [dbo].[Num_Session] OFF
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
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (6, N'200')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (7, N'107')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (8, N'108')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (9, N'109')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (10, N'201')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (11, N'202')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (12, N'203')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (13, N'700')
INSERT [dbo].[Rooms] ([RoomId], [RoomNumber]) VALUES (14, N'abc123')
SET IDENTITY_INSERT [dbo].[Rooms] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedules] ON 

INSERT [dbo].[Schedules] ([ScheduleId], [EnrollmentId], [CourseId], [RoomId], [Day], [SlotId], [TeacherId]) VALUES (13, 1, 2, 1, CAST(N'2024-07-07' AS Date), 1, 1)
INSERT [dbo].[Schedules] ([ScheduleId], [EnrollmentId], [CourseId], [RoomId], [Day], [SlotId], [TeacherId]) VALUES (15, 1, 2, 1, CAST(N'2024-07-07' AS Date), 2, 1)
INSERT [dbo].[Schedules] ([ScheduleId], [EnrollmentId], [CourseId], [RoomId], [Day], [SlotId], [TeacherId]) VALUES (16, 1, 2, 1, CAST(N'2024-07-14' AS Date), 1, 1)
INSERT [dbo].[Schedules] ([ScheduleId], [EnrollmentId], [CourseId], [RoomId], [Day], [SlotId], [TeacherId]) VALUES (17, 1, 2, 1, CAST(N'2024-07-20' AS Date), 1, 1)
INSERT [dbo].[Schedules] ([ScheduleId], [EnrollmentId], [CourseId], [RoomId], [Day], [SlotId], [TeacherId]) VALUES (18, 18, 2, 1, CAST(N'2024-07-07' AS Date), 1, 1)
INSERT [dbo].[Schedules] ([ScheduleId], [EnrollmentId], [CourseId], [RoomId], [Day], [SlotId], [TeacherId]) VALUES (21, 13, 3, 2, CAST(N'2024-07-11' AS Date), 2, 3)
INSERT [dbo].[Schedules] ([ScheduleId], [EnrollmentId], [CourseId], [RoomId], [Day], [SlotId], [TeacherId]) VALUES (22, 19, 3, 2, CAST(N'2024-07-11' AS Date), 2, 3)
INSERT [dbo].[Schedules] ([ScheduleId], [EnrollmentId], [CourseId], [RoomId], [Day], [SlotId], [TeacherId]) VALUES (23, 25, 3, 2, CAST(N'2024-07-11' AS Date), 2, 3)
INSERT [dbo].[Schedules] ([ScheduleId], [EnrollmentId], [CourseId], [RoomId], [Day], [SlotId], [TeacherId]) VALUES (24, 26, 3, 2, CAST(N'2024-07-11' AS Date), 2, 3)
INSERT [dbo].[Schedules] ([ScheduleId], [EnrollmentId], [CourseId], [RoomId], [Day], [SlotId], [TeacherId]) VALUES (25, 1, 2, 1, CAST(N'0001-01-01' AS Date), 1, 1)
INSERT [dbo].[Schedules] ([ScheduleId], [EnrollmentId], [CourseId], [RoomId], [Day], [SlotId], [TeacherId]) VALUES (26, 21, 2, 1, CAST(N'0001-01-01' AS Date), 1, 1)
INSERT [dbo].[Schedules] ([ScheduleId], [EnrollmentId], [CourseId], [RoomId], [Day], [SlotId], [TeacherId]) VALUES (27, 22, 2, 1, CAST(N'0001-01-01' AS Date), 1, 1)
INSERT [dbo].[Schedules] ([ScheduleId], [EnrollmentId], [CourseId], [RoomId], [Day], [SlotId], [TeacherId]) VALUES (28, 1, 2, 1, CAST(N'2024-07-05' AS Date), 1, 1)
INSERT [dbo].[Schedules] ([ScheduleId], [EnrollmentId], [CourseId], [RoomId], [Day], [SlotId], [TeacherId]) VALUES (29, 21, 2, 1, CAST(N'2024-07-05' AS Date), 1, 1)
INSERT [dbo].[Schedules] ([ScheduleId], [EnrollmentId], [CourseId], [RoomId], [Day], [SlotId], [TeacherId]) VALUES (30, 22, 2, 1, CAST(N'2024-07-05' AS Date), 1, 1)
SET IDENTITY_INSERT [dbo].[Schedules] OFF
GO
SET IDENTITY_INSERT [dbo].[Slots] ON 

INSERT [dbo].[Slots] ([SlotId], [SlotName], [StartTime], [EndTime]) VALUES (1, N'Slot 1', CAST(N'07:00:00' AS Time), CAST(N'09:20:00' AS Time))
INSERT [dbo].[Slots] ([SlotId], [SlotName], [StartTime], [EndTime]) VALUES (2, N'Slot 2', CAST(N'09:20:00' AS Time), CAST(N'11:30:00' AS Time))
INSERT [dbo].[Slots] ([SlotId], [SlotName], [StartTime], [EndTime]) VALUES (3, N'Slot 3', CAST(N'12:00:00' AS Time), CAST(N'14:20:00' AS Time))
INSERT [dbo].[Slots] ([SlotId], [SlotName], [StartTime], [EndTime]) VALUES (4, N'Slot 4', CAST(N'14:30:00' AS Time), CAST(N'16:20:00' AS Time))
INSERT [dbo].[Slots] ([SlotId], [SlotName], [StartTime], [EndTime]) VALUES (5, N'Slot 5', CAST(N'16:30:00' AS Time), CAST(N'18:20:00' AS Time))
INSERT [dbo].[Slots] ([SlotId], [SlotName], [StartTime], [EndTime]) VALUES (6, N'Slot 6', CAST(N'18:30:00' AS Time), CAST(N'20:30:00' AS Time))
SET IDENTITY_INSERT [dbo].[Slots] OFF
GO
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (1, N'Student1', CAST(N'2024-06-15T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0974764764', N'Student10@gmail.com', 1, N'student1', N'73d93ee19d2a02a866fa2427dd2b394d', N'vQnDw', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (2, N'Student2', CAST(N'2024-06-20T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0845675476', N'Student2@gmail.com', 2, N'student2', N'c92bf951c43bb489ec8c05dfa3300ae5', N'SpH!N', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (3, N'Student3', CAST(N'2024-06-05T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0876427642', N'Student3@gmail.com', 3, N'student3', N'9a10d89956d95f574f39015cb8a23c0c', N'LLQk!', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (4, N'Student4', CAST(N'2024-06-06T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0247454574', N'Student4@gmail.com', 4, N'student4', N'f9c857e37e1c1b67cb29fe4d6d9f7a55', N'kGnju', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (5, N'Student5', CAST(N'2024-06-11T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0976724672', N'Student5@gmail.com', 5, N'student5', N'16bd7bbc24400ef97a08ff673002803e', N'pIEta', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (6, N'Student6', CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0846754724', N'Student6@gmail.com', 6, N'student6', N'5e5a7eb842a6fe277e9f4421143efbfd', N'fIzmH', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (7, N'Student7', CAST(N'2024-06-05T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0965656227', N'Student7@gmail.com', 7, N'student7', N'58d3380468dac8feb8e8f59e08315397', N'isowl', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (8, N'Student8', CAST(N'2024-06-05T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0864576247', N'Student8@gmail.com', 8, N'student8', N'da2cac0bf9fabd47704c31755353ce0e', N'HRnis', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (9, N'Student9', CAST(N'2024-06-13T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0962321213', N'Student9@gmail.com', 9, N'student9', N'fb4cacafc1ed9da28b14e49e48c6103c', N'OjMPR', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (10, N'Student10', CAST(N'2024-06-24T00:00:00.0000000' AS DateTime2), N'Bac ninh', N'0244654645', N'Student10@gmail.com', 10, N'student10', N'd0cffc0ac1132705c9cb49f6c87ec7e7', N'tBEjQ', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (11, N'qưqeq', CAST(N'2024-06-06T00:00:00.0000000' AS DateTime2), N'qeqweq', N'0967676767', N'dque@gmai', 1, N'Toan1', N'f3fffd99d5473bc2b652e189fc8303b2', N'x!mdd', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (16, N'anbatocom', CAST(N'2024-06-05T00:00:00.0000000' AS DateTime2), N'Bacnib', N'0964276724', N'zxczx@gmail.com', 1, N'dvt123', N'1859732d1d78bfd915946fba107f0555', N'nVZwX', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (17, N'cxvxcvxv', CAST(N'2024-06-13T00:00:00.0000000' AS DateTime2), N'xcvxcvxv', N'0941411414', N'xvcvxv@gmail.com', 1, N'dvt124', N'02364bf14ccdc3ff99803356ce0d1cb4', N'WnASY', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (18, N'toandv', CAST(N'2024-06-07T00:00:00.0000000' AS DateTime2), N'bac ninh', N'0967647246', N'bczxbmcbm@gmail.com', 8, N'toan444', N'a7eadf75069e2f049336aa51622da982', N'KssKf', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (19, N'đfsdfsdfdf', CAST(N'2024-07-04T00:00:00.0000000' AS DateTime2), N'ccxczcxzcx', N'0912331213', N'czxcz@gmail.com', 1, N'toanll', N'8f3d19194b4dd6025cd0b7db20fbdb43', N'YtBiu', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (20, N'văn toàn ', CAST(N'2024-07-11T00:00:00.0000000' AS DateTime2), N'xcmzbcn', N'0966564646', N'toan@gmail.com', 1, N'Student20', N'c963320dadcb198bb56a4ce4390236a6', N'deKnh', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (21, N'toàn', CAST(N'2024-07-18T00:00:00.0000000' AS DateTime2), N'xmbczc', N'0967276267', N'abacac@gmail.com', 1, N'Student30', N'716b998a919f8f5f0cb0ca0025afe48b', N'G!xbZ', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (22, N'abatocom', CAST(N'2024-07-18T00:00:00.0000000' AS DateTime2), N'abcxyz', N'0921610330', N'bacmc@gmmds', 1, N'abc1', N'b93c0c229e8120da998c7219d2485a64', N'SIJtC', 1)
INSERT [dbo].[Students] ([StudentId], [Name], [DateOfBirth], [Address], [PhoneNumber], [Email], [MajorId], [Username], [Password], [RandomKey], [RoleId]) VALUES (23, N'vcxvxcv', CAST(N'2024-07-18T00:00:00.0000000' AS DateTime2), N'vcvxcvxcv', N'0964762742', N'bacninh@gmail.com', 2, N'abc2', N'7cb35f7e76e6927591fcf825c39799f5', N'avVWp', 1)
SET IDENTITY_INSERT [dbo].[Students] OFF
GO
SET IDENTITY_INSERT [dbo].[Teachers] ON 

INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (1, N'T1709', N'teacher1', N'a7a66b8c1f92ee594ff85ca682fb18a3', N'Ha noi', N'Teacher1@fpt.edu.vn', N'0987654324', N'MDYRH', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (2, N'Teacher2', N'teacher2', N'7ff8885dd0551d2116e53dace8f470a8', N'Ha noi', N'Teacher2@fpt.edu.vn', N'0987654321', N'DDpbK', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (3, N'van toan', N'teacher3', N'36c8fce171bbb01b464f3ac77d160f5d', N'Ha noi', N'Teacher3@fpt.edu.vn', N'0987654321', N'ZaUye', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (4, N'Teacher4', N'teacher4', N'009e89c5d16a1ba8f63650d727070507', N'Ha noi', N'Teacher4@fpt.edu.vn', N'0987654321', N'lfkGw', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (5, N'Teacher5', N'teacher5', N'6934a3694af75cf01992d93af705f5b6', N'Ha noi', N'Teacher5@fpt.edu.vn', N'0987654321', N'GYCJU', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (6, N'Teacher6', N'teacher6', N'045a83738ad43a2524feb0dfaace9168', N'Ha noi', N'Teacher6@fpt.edu.vn', N'0987654321', N'FeHXm', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (7, N'Teacher7', N'teacher7', N'6d0d94370e0aac0a96fce10ed49d0cb1', N'Ha noi', N'Teacher7@fpt.edu.vn', N'0987654321', N'lykjx', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (8, N'Teacher8', N'teacher8', N'd024b89acb889c8d694fff6f8fbb3bbe', N'Hi', N'Teacher8@fpt.edu.vn', N'0987654321', N'RaJCi', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (9, N'Teacher9', N'teacher9', N'c83d7d65b966e2d86fd192096917b9d4', N'Ha noi', N'Teacher9@fpt.edu.vn', N'0987654321', N'CFyPK', 2)
INSERT [dbo].[Teachers] ([TeacherId], [Name], [Username], [Password], [Address], [Email], [PhoneNumber], [RandomKey], [RoleId]) VALUES (10, N'Teacher10', N'teacher10', N'19de7e42a2077c29cd9491d56b86e1f0', N'Ha noi', N'Teacher10@fpt.edu.vn', N'0987654321', N'yr!WP', 2)
SET IDENTITY_INSERT [dbo].[Teachers] OFF
GO
/****** Object:  Index [IX_Classes_CourseId]    Script Date: 7/9/2024 12:42:25 AM ******/
CREATE NONCLUSTERED INDEX [IX_Classes_CourseId] ON [dbo].[Classes]
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_RoomId]    Script Date: 7/9/2024 12:42:25 AM ******/
CREATE NONCLUSTERED INDEX [IX_Classes_RoomId] ON [dbo].[Classes]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_TeacherId]    Script Date: 7/9/2024 12:42:25 AM ******/
CREATE NONCLUSTERED INDEX [IX_Classes_TeacherId] ON [dbo].[Classes]
(
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Courses_MajorId]    Script Date: 7/9/2024 12:42:25 AM ******/
CREATE NONCLUSTERED INDEX [IX_Courses_MajorId] ON [dbo].[Courses]
(
	[MajorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Enrollments_ClassId]    Script Date: 7/9/2024 12:42:25 AM ******/
CREATE NONCLUSTERED INDEX [IX_Enrollments_ClassId] ON [dbo].[Enrollments]
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Enrollments_StudentId]    Script Date: 7/9/2024 12:42:25 AM ******/
CREATE NONCLUSTERED INDEX [IX_Enrollments_StudentId] ON [dbo].[Enrollments]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Lstudent_MajorId]    Script Date: 7/9/2024 12:42:25 AM ******/
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
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD FOREIGN KEY([ClassID])
REFERENCES [dbo].[Classes] ([ClassId])
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD FOREIGN KEY([ClassID])
REFERENCES [dbo].[Classes] ([ClassId])
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD FOREIGN KEY([EnrollmentID])
REFERENCES [dbo].[Enrollments] ([EnrollmentId])
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD FOREIGN KEY([RoomID])
REFERENCES [dbo].[Rooms] ([RoomId])
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD FOREIGN KEY([RoomID])
REFERENCES [dbo].[Rooms] ([RoomId])
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD FOREIGN KEY([StudentID])
REFERENCES [dbo].[Students] ([StudentId])
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD FOREIGN KEY([StudentID])
REFERENCES [dbo].[Students] ([StudentId])
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD FOREIGN KEY([TeacherID])
REFERENCES [dbo].[Teachers] ([TeacherId])
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD FOREIGN KEY([TeacherID])
REFERENCES [dbo].[Teachers] ([TeacherId])
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
ALTER TABLE [dbo].[Grades]  WITH CHECK ADD  CONSTRAINT [FK_Grades_Courses] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([CourseId])
GO
ALTER TABLE [dbo].[Grades] CHECK CONSTRAINT [FK_Grades_Courses]
GO
ALTER TABLE [dbo].[Grades]  WITH CHECK ADD  CONSTRAINT [FK_Grades_Enrollments] FOREIGN KEY([EnrollmentId])
REFERENCES [dbo].[Enrollments] ([EnrollmentId])
GO
ALTER TABLE [dbo].[Grades] CHECK CONSTRAINT [FK_Grades_Enrollments]
GO
ALTER TABLE [dbo].[Num_Session]  WITH CHECK ADD FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([CourseId])
GO
ALTER TABLE [dbo].[Num_Session]  WITH CHECK ADD FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([CourseId])
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([CourseId])
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD FOREIGN KEY([EnrollmentId])
REFERENCES [dbo].[Enrollments] ([EnrollmentId])
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([RoomId])
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD FOREIGN KEY([SlotId])
REFERENCES [dbo].[Slots] ([SlotId])
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teachers] ([TeacherId])
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
