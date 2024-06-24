-- Sử dụng cơ sở dữ liệu KSTH
USE [KSTH];
GO

-- Thêm cột CourseID vào bảng Attendance
ALTER TABLE Attendance
ADD CourseID INT;
GO

-- Thêm ràng buộc khóa ngoại cho cột CourseID
ALTER TABLE Attendance
ADD CONSTRAINT FK_CourseID
FOREIGN KEY (CourseID) REFERENCES Courses(CoursesId);
GO

CREATE TABLE Num_Session (
    NumId INT IDENTITY(1,1) PRIMARY KEY,
    Numses VARCHAR(255) NOT NULL,
    CourseId INT,
    FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
);

ALTER TABLE Attendance
DROP COLUMN CourseID;
GO

-- Thêm cột NumId vào bảng Attendance
ALTER TABLE Attendance
ADD NumId INT;
GO

CREATE TABLE Attendance (
    ID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT NOT NULL,
    ClassID INT NOT NULL,
    EnrollmentID INT NOT NULL,
    TeacherID INT NOT NULL,
    RoomID INT NOT NULL,
    AttendanceDate DATE NOT NULL,
    AttendanceStatus VARCHAR(20) NOT NULL,
    Reason TEXT,
    NumID INT NOT NULL,
    CONSTRAINT FK_Student FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    CONSTRAINT FK_Class FOREIGN KEY (ClassID) REFERENCES Classes(ClassID),
    CONSTRAINT FK_Enrollment FOREIGN KEY (EnrollmentID) REFERENCES Enrollments(EnrollmentID),
    CONSTRAINT FK_Teacher FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID),
    CONSTRAINT FK_Room FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),
    CONSTRAINT FK_Number FOREIGN KEY (NumID) REFERENCES Numbers(NumID)
);
