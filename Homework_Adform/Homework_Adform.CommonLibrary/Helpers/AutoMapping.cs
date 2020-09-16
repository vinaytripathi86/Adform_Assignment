using AutoMapper;
using Homework_Adform.CommonLibrary.Models;
using Homework_Adform.CommonLibrary.Models.DBModels;
using Homework_Adform.CommonLibrary.Models.DTOs;
using Homework_Adform.CommonLibrary.Models.Requests;
using Homework_Adform.CommonLibrary.Models.Responses;

namespace Homework_Adform.CommonLibrary.Helpers
{
    /// <summary>
    /// Mapping class used by automapper.
    /// </summary>
    public class AutoMapping : Profile
    {
        /// <summary>
        /// Create new instance of <see cref="AutoMapping"/> class.
        /// </summary>
        public AutoMapping()
        {
            CreateMap<CreateLabelRequest, CreateLabelDTO>().ReverseMap();
            CreateMap<AssignLabelRequest, AssignLabelDto>();
            CreateMap<DeleteLabelRequest, DeleteLabelDto>();
            CreateMap<LabelDto, LabelResponses>().ReverseMap();
            CreateMap<CreateLabelDTO, LabelDbModel>();
            CreateMap<LabelDbModel, LabelDto>();

            CreateMap<CreateItemRequest, CreateItemDto>();
            CreateMap<DeleteItemRequest, DeleteItemDto>();
            CreateMap<UpdateItemRequest, UpdateItemDto>();
            CreateMap<GetItemRequest, GetItemDto>();
            CreateMap<GetItemsRequest, GetItemsDto>();
            CreateMap<ItemDto, ItemResponse>();
            CreateMap<ItemDto, UpdateItemRequest>();
            CreateMap<TodoItemDBModel, ItemDto>();
            CreateMap<CreateItemDto, TodoItemDBModel>();
            CreateMap<UpdateItemDto, TodoItemDBModel>();

            CreateMap<CreateListRequest, CreateListDto>();
            CreateMap<DeleteListRequest, DeleteListDto>();
            CreateMap<UpdateListRequest, UpdateListDto>();
            CreateMap<ListDto, UpdateListRequest>();
            CreateMap<ListDto, ListResponse>();
            CreateMap<CreateListDto, TodoListDbModel>();
            CreateMap<CreateListRequest, TodoListDbModel>();
            CreateMap<TodoListDbModel, ListDto>();
            CreateMap<UpdateListDto, TodoListDbModel>();
        }
    }
}
