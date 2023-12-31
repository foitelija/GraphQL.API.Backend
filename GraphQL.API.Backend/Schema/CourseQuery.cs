﻿using FirebaseAdminAuthentication.DependencyInjection.Models;
using GraphQL.API.Backend.Filters;
using GraphQL.API.Backend.Interfaces;
using GraphQL.API.Backend.Models;
using GraphQL.API.Backend.Services;
using GraphQL.API.Backend.Sorters;
using HotChocolate.Authorization;
using System.Security.Claims;

namespace GraphQL.API.Backend.Schema
{
    [ExtendObjectType(typeof(Query))]
    public class CourseQuery
    {
        private readonly ICoursesRepository _coursesRepository;
        public CourseQuery(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
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
                CreatorId = course.CreatorId,

            };
        }

        [Authorize]
        public async Task<IEnumerable<CourseType>> GetCoursesAsyncOld(ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.ID);
            var email = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.EMAIL);
            var username = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.USERNAME);

            var courses = await _coursesRepository.GetAllCourseAsync();
            return courses.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId,
            });
        }

        [UseDbContext(typeof(SchoolDbContext))]
        [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering(typeof(CourseFilterType))]
        [UseSorting(typeof(CourseSortType))]
        public IQueryable<CourseType> GetCoursesAsync([ScopedService] SchoolDbContext repository)
        {
            return repository.Courses.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId,
                CreatorId = c.CreatorId,
            });
        }
    }
}
