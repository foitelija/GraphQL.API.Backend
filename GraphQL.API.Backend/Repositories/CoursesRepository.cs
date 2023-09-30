using GraphQL.API.Backend.DTOs;
using GraphQL.API.Backend.Interfaces;
using GraphQL.API.Backend.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace GraphQL.API.Backend.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly IDbContextFactory<SchoolDbContext> _context;

        public CoursesRepository(IDbContextFactory<SchoolDbContext> context)
        {
            _context = context;
        }

        public async Task<CourseDTO> CreateCourseAsync(CourseDTO course)
        {
            using(SchoolDbContext context = _context.CreateDbContext())
            {
                context.Courses.Add(course);
                await context.SaveChangesAsync();

                return course;
            }
        }

        public async Task<CourseDTO> UpdateCourseAsync(CourseDTO course)
        {
            using (SchoolDbContext context = _context.CreateDbContext())
            {
                context.Courses.Update(course);
                await context.SaveChangesAsync();

                return course;
            }
        }

        public async Task<bool> DeleteCourseAsync(Guid id)
        {
            using (SchoolDbContext context = _context.CreateDbContext())
            {
                CourseDTO course = new CourseDTO()
                {
                    Id = id
                };

                context.Courses.Remove(course);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<IEnumerable<CourseDTO>> GetAllCourseAsync()
        {
            using (SchoolDbContext context = _context.CreateDbContext())
            {
                return await context.Courses
                    .Include(c=>c.Instructor)
                    .Include(c=>c.Students)
                    .ToListAsync();
            }
        }

        public async Task<CourseDTO> GetCourseByIdAsync(Guid id)
        {
            using (SchoolDbContext context = _context.CreateDbContext())
            {
                return await context.Courses
                    .Include(c=>c.Instructor)
                    .Include(c => c.Students)
                    .FirstOrDefaultAsync(c=>c.Id == id);
            }
        }
    }
}
