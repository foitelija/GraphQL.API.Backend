﻿using GraphQL.API.Backend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.API.Backend.Services
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
        }

        public DbSet<CourseDTO> Courses { get; set; }
        public DbSet<InstructorDTO> Instructors { get; set; }
        public DbSet<StudentDTO> Students { get; set; }
    }
}