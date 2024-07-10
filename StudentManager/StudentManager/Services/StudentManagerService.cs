using System.Collections.Concurrent;
using StudentManager.Models;
using StudentManager.Utilities;

namespace StudentManager.Services;

public class StudentManagerService
{
    private readonly ConcurrentDictionary<int, Student> _students = new();
    private static SemaphoreSlim _semaphoreSlim = new(1, 1);
    private FileManager _fileManager = new();

    public async Task PopulateDataAsync(int numberOfStudents, ConcurrentDictionary<int, Student> students)
    {
        await _semaphoreSlim.WaitAsync();
        try
        {
            var random = new Random();
            var courseNames = new[] { "Math", "English", "Science", "History", "Art", "Music" };
            for (var i = 1; i <= numberOfStudents; i++)
            {
                var student = new Student
                {
                    Id = i,
                    Name = $"Student {i}",
                    Age = random.Next(18, 50),
                    Courses =
                    [
                        new Course
                        {
                            CourseId = 1,
                            CourseName = courseNames[random.Next(0, courseNames.Length)],
                            Credits = random.Next(1, 5)
                        },

                        new Course
                        {
                            CourseId = 2,
                            CourseName = courseNames[random.Next(0, courseNames.Length)],
                            Credits = random.Next(1, 5)
                        }
                    ]
                };
                students.TryAdd(student.Id, student);
            }
            await _fileManager.SaveDataToFileAsync(students.Values, "data.json");
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }
    
    public async Task SimulateConcurrentUpdatesAsync(int numberOfUpdates)
    {
        var tasks = new List<Task>();
        var random = new Random();

        for (var i = 0; i < numberOfUpdates; i++)
        {
            tasks.Add(Task.Run(async () =>
            {
                var studentId = random.Next(_students.Count);
                if (_students.TryGetValue(studentId, out var student))
                {
                    await _semaphoreSlim.WaitAsync();
                    try
                    {
                        student.Age = random.Next(18, 25);
                        student.Courses.Add(new Course
                        {
                            CourseId = random.Next(100, 200),
                            CourseName = $"Course {random.Next(1, 10)}",
                            Credits = random.Next(1, 5)
                        });
                    }
                    finally
                    {
                        _semaphoreSlim.Release();
                    }
                }
            }));
        }
        await Task.WhenAll(tasks);
        await _fileManager.SaveDataToFileAsync(tasks, "simulated.json");
    }
    
    public IEnumerable<Student> GetStudentsOlderThan(int age)
    {
        return GetStudentsDictionary()
            .Where(student => student.Value.Age > age)
            .Select(student => student.Value);
    }

    public IEnumerable<Student> GetStudentEnrolledInCourse(string courseName)
    {
        return GetStudentsDictionary()
            .Where(student => student.Value.Courses
                .Any(course => course.CourseName == courseName))
            .Select(student => student.Value);
    }
    
    public IEnumerable<IGrouping<int, Student>> GroupStudentByAge()
    {
        return GetStudentsDictionary().Values.GroupBy(student => student.Age);
    }
    
    public ConcurrentDictionary<int, Student> GetStudentsDictionary()
    {
        return _students;
    }
}