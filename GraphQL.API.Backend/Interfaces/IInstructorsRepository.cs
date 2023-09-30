using GraphQL.API.Backend.DTOs;

namespace GraphQL.API.Backend.Interfaces
{
    public interface IInstructorsRepository
    {
        Task<InstructorDTO> GetInstructorByIdAsync(Guid id);
        Task<IEnumerable<InstructorDTO>> GetAllInstructorsByIDAsync(IReadOnlyList<Guid> keys);
    }
}
