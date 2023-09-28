﻿using GraphQL.API.Backend.Models;

namespace GraphQL.API.Backend.Schema
{
    public class Mutation
    {
        private readonly List<CourseResult> _courses;

        public Mutation()
        {
                _courses = new List<CourseResult>();
        }

        public CourseResult CreateCourse(CourseInputType courseInput)
        {
            var courseType = new CourseResult()
            {
                Id = Guid.NewGuid(),
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId,
            };

            _courses.Add(courseType);

            return courseType;
        }

        public CourseResult UpdateCourse(Guid id, CourseInputType courseInput)
        {
            var course = _courses.FirstOrDefault(c=>c.Id == id);

            if(course == null) 
            {
                throw new GraphQLException(new Error("Course not found.", "COURSE_NOT_FOUND"));
            }

            course.Name = courseInput.Name;
            course.Subject = courseInput.Subject;
            course.InstructorId = courseInput.InstructorId;

            return course;
        }

        public bool DeleteCourse(Guid id)
        {
            return _courses.RemoveAll(c=>c.Id==id) >= 1; 
        }
    }
}