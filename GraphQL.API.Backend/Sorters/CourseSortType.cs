using GraphQL.API.Backend.Models;
using HotChocolate.Data.Sorting;

namespace GraphQL.API.Backend.Sorters
{
    public class CourseSortType : SortInputType<CourseType>
    {
        protected override void Configure(ISortInputTypeDescriptor<CourseType> descriptor)
        {
            descriptor.Ignore(x => x.Id);
            descriptor.Ignore(x => x.InstructorId);
            
            //descriptor.Field(c => c.Name).Name("CourseName:"); //переименовывает при создании запроса, добавил чисто побаловаться. 

            base.Configure(descriptor);
        }
    }
}
