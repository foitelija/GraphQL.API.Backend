using GraphQL.API.Backend.Interfaces;
using GraphQL.API.Backend.Models;

namespace GraphQL.API.Backend.Schema
{
    [ExtendObjectType(typeof(Query))]
    public class InstructorQuery
    {
        private readonly IInstructorsRepository _instructorsRepository;

        public InstructorQuery(IInstructorsRepository instructorsRepository)
        {
            _instructorsRepository = instructorsRepository;
        }


        public async Task<InstructorType> GetInstructorAsyncById(Guid id)
        {
            var instructor = await _instructorsRepository.GetInstructorByIdAsync(id);

            return new InstructorType()
            {
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Id = id,
                Salary = instructor.Salary,
            };
        }
    }
}
