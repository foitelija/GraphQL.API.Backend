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
                Instructor = new InstructorType()
                {
                    Id = c.Instructor.Id,
                    FirstName = c.Instructor.FirstName,
                    LastName = c.Instructor.LastName,
                    Salary = c.Instructor.Salary,
                }
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
                Instructor = new InstructorType()
                {
                    Id = course.Instructor.Id,
                    FirstName = course.Instructor.FirstName,
                    LastName = course.Instructor.LastName,
                    Salary = course.Instructor.Salary,
                }
            };
        }
    }
}
