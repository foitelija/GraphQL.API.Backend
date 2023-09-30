using GraphQL.API.Backend.DTOs;
using GraphQL.API.Backend.Interfaces;
using GraphQL.API.Backend.Models;
using HotChocolate.Subscriptions;

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

        public async Task<CourseResult> CreateCourse(CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
        {
            var courseDTO = new CourseDTO()
            {
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId
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

        public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
        {
            var courseDTO = new CourseDTO()
            {
                Id = id,
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId
            };

            courseDTO = await _coursesRepository.UpdateCourseAsync(courseDTO);


            var course = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId = courseDTO.InstructorId,  
            };

            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
            await topicEventSender.SendAsync(updateCourseTopic, course);

            return course;
        }

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
    }
}
