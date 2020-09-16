using Homework_Adform.CommonLibrary.Models.DTOs;
using HotChocolate.Types;

namespace Homework_Adform.TodoAPI.Graphql.ModelTypes
{
    /// <summary>
    /// Label type mapped to LabelDto types for GraphQl.
    /// </summary>
    public class LabelType : ObjectType<LabelDto>
    {
        protected override void Configure(IObjectTypeDescriptor<LabelDto> descriptor)
        {
            descriptor.Field(a => a.Id).Type<IdType>();
            descriptor.Field(a => a.Description).Type<StringType>();
            descriptor.Field(a => a.CreatedDate).Type<DateTimeType>();
            descriptor.Field(a => a.UpdateDate).Type<DateTimeType>();
        }
    }
}
