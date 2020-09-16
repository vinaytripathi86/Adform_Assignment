using Homework_Adform.CommonLibrary.Models.DTOs;
using HotChocolate.Types;

namespace Homework_Adform.TodoAPI.Graphql.ModelTypes
{
    /// <summary>
    /// Items type mapped to ItemDto types for GraphQl.
    /// </summary>
    public class ItemsType : ObjectType<ItemDto>
    {
        protected override void Configure(IObjectTypeDescriptor<ItemDto> descriptor)
        {
            descriptor.Field(a => a.Id).Type<IdType>();
            descriptor.Field(a => a.ListId).Type<DateTimeType>();
            descriptor.Field(a => a.Notes).Type<StringType>();
            descriptor.Field(a => a.CreatedDate).Type<DateTimeType>();
            descriptor.Field(a => a.UpdatedDate).Type<DateTimeType>();
        }
    }
}
