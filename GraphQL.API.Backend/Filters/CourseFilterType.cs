using GraphQL.API.Backend.Models;
using HotChocolate.Data.Filters;

namespace GraphQL.API.Backend.Filters
{
    public class CourseFilterType : FilterInputType<CourseType>
    {
        protected override void Configure(IFilterInputTypeDescriptor<CourseType> descriptor)
        { 
            descriptor.Ignore(c => c.Students);
            base.Configure(descriptor);
        }
    }
}
