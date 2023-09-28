﻿namespace GraphQL.API.Backend.Schema
{
    public class CourseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public Subject Subject { get; set; }
        [GraphQLNonNullType]
        public InstructorType Instructor { get; set; }
        public IEnumerable<StudentType> Students { get; set; }
    }
}