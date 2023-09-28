﻿namespace GraphQL.API.Backend.Models
{
    public class CourseResult
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
        public Guid InstructorId { get; set; }
    }
}