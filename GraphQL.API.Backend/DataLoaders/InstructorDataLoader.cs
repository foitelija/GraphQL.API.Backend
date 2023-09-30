using GraphQL.API.Backend.DTOs;
using GraphQL.API.Backend.Interfaces;

namespace GraphQL.API.Backend.DataLoaders
{
    public class InstructorDataLoader : BatchDataLoader<Guid, InstructorDTO>
    {
        private readonly IInstructorsRepository _instructorsRepository;

        public InstructorDataLoader(IBatchScheduler batchScheduler, IInstructorsRepository instructorsRepository, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _instructorsRepository = instructorsRepository;
        }

        protected override async Task<IReadOnlyDictionary<Guid, InstructorDTO>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            var instructors = await _instructorsRepository.GetAllInstructorsByIDAsync(keys);

            return instructors.ToDictionary(i => i.Id);
        }
    }
}
