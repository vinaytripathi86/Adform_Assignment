using Homework_Adform.CommonLibrary.Models.DTOs;
using Homework_Adform.TodoAPI.Graphql.Resolvers;
using HotChocolate.Types;

namespace Homework_Adform.TodoAPI.Graphql.ModelTypes
{
    /// <summary>
    /// Lists type mapped to ListDto types for GraphQl.
    /// </summary>
    public class ListsType : ObjectType<ListDto>
    {
        protected override void Configure(IObjectTypeDescriptor<ListDto> descriptor)
        {
            descriptor.Field(a => a.Id).Type<IdType>();
            descriptor.Field(a => a.Description).Type<StringType>();
            descriptor.Field(a => a.CreatedDate).Type<DateTimeType>();
            descriptor.Field(a => a.UpdatedDate).Type<DateTimeType>();
            descriptor.Field<ItemResolver>(t => t.GetItems(default, default, default).Result);
        }
    }
}
