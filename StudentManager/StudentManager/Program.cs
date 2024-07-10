using StudentManager.Services;
namespace StudentManager
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var studentManager = new StudentManagerService();
            var students = studentManager.GetStudentsDictionary();

            await studentManager.SimulateConcurrentUpdatesAsync(1000);
            await studentManager.PopulateDataAsync(1000, students);

            var studentsOlderThan = studentManager.GetStudentsOlderThan(20);
            var studentEnrolledInCourse = studentManager.GetStudentEnrolledInCourse("Math");
            var groupedByAge = studentManager.GroupStudentByAge();
            
            Console.WriteLine($"Students older than 20: {studentsOlderThan.Count()}");
            Console.WriteLine($"Students enrolled in Math: {studentEnrolledInCourse.Count()}");
            foreach (var group in groupedByAge)
            {
                Console.WriteLine($"Age{group.Key}: {group.Count()} students");
            }
        }
    }
}