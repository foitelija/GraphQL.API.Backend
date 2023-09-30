using GraphQL.API.Backend.DTOs;

namespace GraphQL.API.Backend.Interfaces
{
    public interface ICoursesRepository
    {
        Task<CourseDTO> CreateCourseAsync(CourseDTO course);
        Task<CourseDTO> UpdateCourseAsync(CourseDTO course);
        Task<bool> DeleteCourseAsync(Guid id);
        Task<IEnumerable<CourseDTO>> GetAllCourseAsync();
        Task<CourseDTO> GetCourseByIdAsync(Guid id);
    }
}
