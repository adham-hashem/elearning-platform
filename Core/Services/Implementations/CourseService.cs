using Core.RepositoriesContracts;
using Core.Services.Contracts;
using Infrastructure.Models;

namespace Core.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllCoursesAsync();
        }

        public async Task<Course> GetCourseByIdAsync(Guid courseId)
        {
            return await _courseRepository.GetCourseByIdAsync(courseId);
        }

        public async Task AddCourseAsync(Course course)
        {
            await _courseRepository.AddCourseAsync(course);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            await _courseRepository.UpdateCourseAsync(course);
        }

        public async Task DeleteCourseAsync(Guid courseId)
        {
            await _courseRepository.DeleteCourseAsync(courseId);
        }
    }
}
