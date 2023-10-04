using AppAny.HotChocolate.FluentValidation;
using FirebaseAdminAuthentication.DependencyInjection.Models;
using GraphQL.API.Backend.DTOs;
using GraphQL.API.Backend.Interfaces;
using GraphQL.API.Backend.Middlewares;
using GraphQL.API.Backend.Models;
using GraphQL.API.Backend.Validators;
using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Subscriptions;
using System.Security.Claims;

namespace GraphQL.API.Backend.Schema
{
    public class Mutation
    {
        private readonly List<CourseResult> _courses;
        private readonly ICoursesRepository _coursesRepository;

        public Mutation(ICoursesRepository coursesRepository)
        {
            _courses = new List<CourseResult>();
            _coursesRepository = coursesRepository;
        }

        //[Authorize]
        [UseUser]
        public async Task<CourseResult> CreateCourse(
            [UseFluentValidation, UseValidator<CourseTypeInputValidator>]CourseInputType courseInput,
            [Service] ITopicEventSender topicEventSender,
            [GlobalState("User")]User user)
        {
            //await Validate(courseInput); - мануальная валидация, есть пакет для этого - AppAny.HotChocolate.FluentValidation 

            var courseDTO = new CourseDTO()
            {
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId,
                CreatorId = user.Id
            };

            courseDTO = await _coursesRepository.CreateCourseAsync(courseDTO);

            var course = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId = courseDTO.InstructorId,
            };

            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);

            return course;
        }

        

        [Authorize]
        public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType courseInput, [Service] ITopicEventSender topicEventSender, ClaimsPrincipal claimsPrincipal)
        {

            //await Validate(courseInput);

            var currentCourseDto = await _coursesRepository.GetCourseByIdAsync(id);

            if(currentCourseDto == null)
            {
                throw new GraphQLException(new Error("Курс не найден", "COURSE_NOT_FOUND"));
            }

            if(currentCourseDto.CreatorId != claimsPrincipal.FindFirstValue(FirebaseUserClaimType.ID))
            {
                throw new GraphQLException(new Error("Не достаточно прав для обновления данного курса", "INVALID_PERMISSION"));
            }


            currentCourseDto.Name = courseInput.Name;
            currentCourseDto.Subject = courseInput.Subject;
            currentCourseDto.InstructorId = courseInput.InstructorId;


            currentCourseDto = await _coursesRepository.UpdateCourseAsync(currentCourseDto);


            var course = new CourseResult()
            {
                Id = currentCourseDto.Id,
                Name = currentCourseDto.Name,
                Subject = currentCourseDto.Subject,
                InstructorId = currentCourseDto.InstructorId,  
            };

            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
            await topicEventSender.SendAsync(updateCourseTopic, course);

            return course;
        }

        [Authorize(Policy = "IsAdmin")]
        public async Task<bool> DeleteCourse(Guid id)
        {
            try
            {
                return await _coursesRepository.DeleteCourseAsync(id);
            }
            catch (Exception)
            {
                return false;
            }
        }


        private async Task Validate(CourseInputType courseInput)
        {
            var validation = new CourseTypeInputValidator();
            
            var validationResult = await validation.ValidateAsync(courseInput);

            if (!validationResult.IsValid)
            {
                throw new GraphQLException("Ошибка валидации данных");
            }
        }
    }
}
