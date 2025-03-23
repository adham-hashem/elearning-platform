using Core.RepositoriesContracts;
using Core.Services.Contracts;
using Infrastructure.Models;

namespace Core.Services.Implementations
{
    public class StudentCourseService : IStudentCourseService
    {
        private readonly IStudentCourseRepository _studentCourseRepository;

        public StudentCourseService(IStudentCourseRepository studentCourseRepository)
        {
            _studentCourseRepository = studentCourseRepository;
        }

        public async Task<IEnumerable<StudentCourse>> GetAllStudentCoursesAsync()
        {
            return await _studentCourseRepository.GetAllStudentCoursesAsync();
        }

        public async Task<StudentCourse> GetStudentCourseByIdAsync(Guid studentId, Guid courseId)
        {
            return await _studentCourseRepository.GetStudentCourseByIdAsync(studentId, courseId);
        }

        public async Task AddStudentCourseAsync(StudentCourse studentCourse)
        {
            await _studentCourseRepository.AddStudentCourseAsync(studentCourse);
        }

        public async Task UpdateStudentCourseAsync(StudentCourse studentCourse)
        {
            await _studentCourseRepository.UpdateStudentCourseAsync(studentCourse);
        }

        public async Task DeleteStudentCourseAsync(Guid studentId, Guid courseId)
        {
            await _studentCourseRepository.DeleteStudentCourseAsync(studentId, courseId);
        }
    }
}
