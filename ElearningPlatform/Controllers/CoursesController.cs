namespace ElearningPlatformAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(Guid id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCourse(CourseDto courseDto)
        {
            var course = new Course
            {
                CourseId = Guid.NewGuid(),
                CourseCode = courseDto.CourseCode,
                CourseName = courseDto.CourseName,
                CreditHours = courseDto.CreditHours,
                DayOfWeek = courseDto.DayOfWeek,
                StartTime = courseDto.StartTime,
                EndTime = courseDto.EndTime,
                Location = courseDto.Location,
                TermId = courseDto.TermId
            };
            await _courseService.AddCourseAsync(course);
            return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, course);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourse(Guid id, CourseDto courseDto)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            course.CourseCode = courseDto.CourseCode;
            course.CourseName = courseDto.CourseName;
            course.CreditHours = courseDto.CreditHours;
            course.DayOfWeek = courseDto.DayOfWeek;
            course.StartTime = courseDto.StartTime;
            course.EndTime = courseDto.EndTime;
            course.Location = courseDto.Location;
            course.TermId = courseDto.TermId;

            await _courseService.UpdateCourseAsync(course);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(Guid id)
        {
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }
    }
}
