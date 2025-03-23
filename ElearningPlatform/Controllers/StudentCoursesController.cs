using Core.Services.Contracts;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElearningPlatformAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentCoursesController : ControllerBase
    {
        private readonly IStudentCourseService _studentCourseService;

        public StudentCoursesController(IStudentCourseService studentCourseService)
        {
            _studentCourseService = studentCourseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCourse>>> GetStudentCourses()
        {
            var studentCourses = await _studentCourseService.GetAllStudentCoursesAsync();
            return Ok(studentCourses);
        }

        [HttpGet("{studentId}/{courseId}")]
        public async Task<ActionResult<StudentCourse>> GetStudentCourse(Guid studentId, Guid courseId)
        {
            var studentCourse = await _studentCourseService.GetStudentCourseByIdAsync(studentId, courseId);
            if (studentCourse == null) return NotFound();
            return Ok(studentCourse);
        }

        [HttpPost]
        public async Task<ActionResult> CreateStudentCourse(StudentCourse studentCourse)
        {
            await _studentCourseService.AddStudentCourseAsync(studentCourse);
            return CreatedAtAction(nameof(GetStudentCourse), new { studentId = studentCourse.StudentId, courseId = studentCourse.CourseId }, studentCourse);
        }

        [HttpPut("{studentId}/{courseId}")]
        public async Task<ActionResult> UpdateStudentCourse(Guid studentId, Guid courseId, StudentCourse studentCourse)
        {
            var existingStudentCourse = await _studentCourseService.GetStudentCourseByIdAsync(studentId, courseId);
            if (existingStudentCourse == null) return NotFound();

            existingStudentCourse.Degree = studentCourse.Degree;
            existingStudentCourse.GradePoints = studentCourse.GradePoints;
            existingStudentCourse.PassStatus = studentCourse.PassStatus;

            await _studentCourseService.UpdateStudentCourseAsync(existingStudentCourse);
            return NoContent();
        }

        [HttpDelete("{studentId}/{courseId}")]
        public async Task<ActionResult> DeleteStudentCourse(Guid studentId, Guid courseId)
        {
            await _studentCourseService.DeleteStudentCourseAsync(studentId, courseId);
            return NoContent();
        }
    }
}
