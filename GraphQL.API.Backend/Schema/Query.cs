using Bogus;
using GraphQL.API.Backend.Interfaces;
using GraphQL.API.Backend.Models;

namespace GraphQL.API.Backend.Schema
{
    public class Query
    {
        private readonly ICoursesRepository _coursesRepository;

        public Query(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<IEnumerable<CourseType>> GetCoursesAsync()
        {
            var courses = await _coursesRepository.GetAllCourseAsync();
            return courses.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId,
            });
        }


        public async Task<CourseType> GetCourseByIdAsync(Guid id)
        {
            var course = await _coursesRepository.GetCourseByIdAsync(id);

            return new CourseType()
            {
                Id = course.Id,
                Name = course.Name,
                Subject = course.Subject,
                InstructorId = course.InstructorId,
            };
        }
    }
}
