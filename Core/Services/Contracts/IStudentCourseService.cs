using Infrastructure.Models;

namespace Core.Services.Contracts
{
    public interface IStudentCourseService
    {
        Task<IEnumerable<StudentCourse>> GetAllStudentCoursesAsync();
        Task<StudentCourse> GetStudentCourseByIdAsync(Guid studentId, Guid courseId);
        Task AddStudentCourseAsync(StudentCourse studentCourse);
        Task UpdateStudentCourseAsync(StudentCourse studentCourse);
        Task DeleteStudentCourseAsync(Guid studentId, Guid courseId);
    }
}
