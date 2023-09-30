using GraphQL.API.Backend.DataLoaders;
using GraphQL.API.Backend.Interfaces;

namespace GraphQL.API.Backend.Models
{
    public class CourseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
        public Guid InstructorId  { get; set; }

        [GraphQLNonNullType]
        public async Task<InstructorType> Instructor([Service] InstructorDataLoader instructorDataLoader)
        {
            var instructor = await instructorDataLoader.LoadAsync(InstructorId);
            return new InstructorType 
            { 
                Id = instructor.Id,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Salary = instructor.Salary 
            };
        }

        public IEnumerable<StudentType> Students { get; set; }
    }
}
