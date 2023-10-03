using FirebaseAdminAuthentication.DependencyInjection.Models;
using GraphQL.API.Backend.Filters;
using GraphQL.API.Backend.Interfaces;
using GraphQL.API.Backend.Models;
using GraphQL.API.Backend.Services;
using GraphQL.API.Backend.Sorters;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace GraphQL.API.Backend.Schema
{
    public class Query
    {
        private readonly ICoursesRepository _coursesRepository;

        public Query(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        [UseDbContext(typeof(SchoolDbContext))]
        public async Task<List<ISearchResultType>> Search(string term, [ScopedService] SchoolDbContext context)
        {
            var courses = await context.Courses
                .Where(c => c.Name.ToLower().Contains(term.ToLower()))
                .Select(c => new CourseType()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subject = c.Subject,
                    InstructorId = c.InstructorId,
                    CreatorId = c.CreatorId,
                }).ToListAsync();

            var instructors = await context.Instructors
                .Where(i => i.FirstName.ToLower().Contains(term.ToLower()) || i.LastName.ToLower().Contains(term.ToLower()))
                .Select(i => new InstructorType()
                {
                    Id = i.Id,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Salary = i.Salary
                }).ToListAsync();

            var result = new List<ISearchResultType>().Concat(courses).Concat(instructors).ToList();

            return result;
        }

    }
}
