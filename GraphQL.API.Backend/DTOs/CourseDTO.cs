using GraphQL.API.Backend.Models;

namespace GraphQL.API.Backend.DTOs
{
    public class CourseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
        Guid InstructodId { get; set; }
        public InstructorDTO Instructor { get; set; }
        public IEnumerable<StudentDTO> Students { get; set; }
    }
}
