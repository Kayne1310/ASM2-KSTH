CREATE TABLE Schedule_1 (
    ScheduleID INT PRIMARY KEY IDENTITY,
    ClassID INT NOT NULL,
    StudentID INT NOT NULL,
    CourseID INT NOT NULL,
    Day DATE NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    FOREIGN KEY (ClassId) REFERENCES Classes(ClassId),
    FOREIGN KEY (RoomId) REFERENCES Rooms(RoomId),
    FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
);
