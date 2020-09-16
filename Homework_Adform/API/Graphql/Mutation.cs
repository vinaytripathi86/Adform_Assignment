using AutoMapper;
using Homework_Adform.CommonLibrary.Contracts.Services;
using Homework_Adform.CommonLibrary.Models;
using Homework_Adform.CommonLibrary.Models.DTOs;
using Homework_Adform.CommonLibrary.Models.Requests;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Homework_Adform.TodoAPI.Graphql
{
    /// <summary>
    /// Mutation class for GraphQl.
    /// </summary>
    public class Mutation
    {
        private readonly ILabelService _labelService;
        private readonly ITodoListService _listService;
        private readonly ITodoItemsService _itemService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Create new instance of <see cref="Mutation"/> class.
        /// </summary>
        /// <param name="labelService">Label service.</param>
        /// <param name="itemService">Item service.</param>
        /// <param name="listService">List service.</param>
        /// <param name="mapper">Auto mapper.</param>
        public Mutation([Service]ILabelService labelService, [Service]ITodoItemsService itemService, [Service]ITodoListService listService,
            [Service]IMapper mapper)
        {
            _labelService = labelService;
            _itemService = itemService;
            _listService = listService;
            _mapper = mapper;
        }

        #region Labels

        /// <summary>
        /// Add label.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        /// <param name="request">Create label request.</param>
        /// <returns>Returns added label.</returns>
        [Authorize]
        public async Task<LabelDto> AddLabel([Service] IHttpContextAccessor contextAccessor, CreateLabelRequest request)
        {
            if (!(contextAccessor.HttpContext.Request.HttpContext.Items["User"] is UserModel user))
                throw new UnauthorizedAccessException();

            if (null == request || string.IsNullOrWhiteSpace(request.Description))
                throw new ArgumentNullException();

            CreateLabelDTO createLabel = _mapper.Map<CreateLabelDTO>(request);
            createLabel.CreatedBy = user.UserId;
            var createdLabel = await _labelService.Add(createLabel);
            return createdLabel;
        }

        /// <summary>
        /// Delete label.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        /// <param name="request">Delete label request.</param>
        /// <returns>Deleted item.</returns>
        [Authorize]
        public async Task<LabelDto> DeleteLabel([Service] IHttpContextAccessor contextAccessor, DeleteLabelRequest request)
        {
            if (!(contextAccessor.HttpContext.Request.HttpContext.Items["User"] is UserModel user))
                throw new UnauthorizedAccessException();

            DeleteLabelDto deleteLabel = _mapper.Map<DeleteLabelDto>(request);
            deleteLabel.CreatedBy = user.UserId;

            var deletedItem = await _labelService.DeleteLabel(deleteLabel);
            if (null == deletedItem)
                throw new ArgumentException();

            return deletedItem;
        }

        #endregion
        #region TodoLists

        /// <summary>
        /// Add todolist.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        /// <param name="request">Create list request.</param>
        /// <returns>Returns added list.</returns>
        [Authorize]
        public async Task<ListDto> AddList([Service] IHttpContextAccessor contextAccessor, CreateListRequest request)
        {
            if (!(contextAccessor.HttpContext.Request.HttpContext.Items["User"] is UserModel user))
                throw new UnauthorizedAccessException();

            if (null == request || string.IsNullOrWhiteSpace(request.Description))
                throw new ArgumentNullException();

            CreateListDto param = _mapper.Map<CreateListDto>(request);
            param.CreatedBy = user.UserId;

            var addedItem = await _listService.Add(param);
            return addedItem;
        }

        /// <summary>
        /// Update todolist.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        /// <param name="request">Update list request.</param>
        /// <returns>Returns updated list.</returns>
        [Authorize]
        public async Task<ListDto> UpdateList([Service] IHttpContextAccessor contextAccessor, UpdateListRequest request)
        {
            if (!(contextAccessor.HttpContext.Request.HttpContext.Items["User"] is UserModel user))
                throw new UnauthorizedAccessException();

            if (null == request || string.IsNullOrWhiteSpace(request.Description))
                throw new ArgumentNullException();

            UpdateListDto param = _mapper.Map<UpdateListDto>(request);
            param.CreatedBy = user.UserId;

            var updatedItem = await _listService.Update(param);
            if (null == updatedItem)
                throw new ArgumentException();
            return updatedItem;
        }

        /// <summary>
        /// Delete todolist.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        /// <param name="request">Delete list request.</param>
        /// <returns>Returns deleted list.</returns>
        [Authorize]
        public async Task<ListDto> DeleteList([Service] IHttpContextAccessor contextAccessor, DeleteListRequest request)
        {
            if (!(contextAccessor.HttpContext.Request.HttpContext.Items["User"] is UserModel user))
                throw new UnauthorizedAccessException();

            DeleteListDto param = _mapper.Map<DeleteListDto>(request);
            param.CreatedBy = user.UserId;

            var deletedItem = await _listService.Delete(param);
            if (null == deletedItem)
                throw new ArgumentException();

            return deletedItem;
        }

        #endregion
        #region TodoItems

        /// <summary>
        /// Add todoitem.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        /// <param name="request">Create item request.</param>
        /// <returns>Returns added item.</returns>
        [Authorize]
        public async Task<ItemDto> AddItem([Service] IHttpContextAccessor contextAccessor, CreateItemRequest request)
        {
            if (!(contextAccessor.HttpContext.Request.HttpContext.Items["User"] is UserModel user))
                throw new UnauthorizedAccessException();

            if (null == request || string.IsNullOrWhiteSpace(request.Note))
                throw new ArgumentNullException();

            CreateItemDto param = _mapper.Map<CreateItemDto>(request);
            param.CreatedBy = user.UserId;

            var addedItem = await _itemService.Add(param);
            return addedItem;
        }

        /// <summary>
        /// Update todoitem.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        /// <param name="request">Update item request.</param>
        /// <returns>Returns updated item.</returns>
        [Authorize]
        public async Task<ItemDto> UpdateItem([Service] IHttpContextAccessor contextAccessor, UpdateItemRequest request)
        {
            if (!(contextAccessor.HttpContext.Request.HttpContext.Items["User"] is UserModel user))
                throw new UnauthorizedAccessException();

            if (null == request || string.IsNullOrWhiteSpace(request.Note))
                throw new ArgumentNullException();

            UpdateItemDto param = _mapper.Map<UpdateItemDto>(request);
            param.CreatedBy = user.UserId;

            var updatedItem = await _itemService.Update(param);
            if (null == updatedItem)
                throw new ArgumentException();

            return updatedItem;
        }

        /// <summary>
        /// Delete todoitem.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        /// <param name="request">Delete item request.</param>
        /// <returns>Returns deleted item.</returns>
        [Authorize]
        public async Task<ItemDto> Delete([Service] IHttpContextAccessor contextAccessor, DeleteItemRequest request)
        {
            if (!(contextAccessor.HttpContext.Request.HttpContext.Items["User"] is UserModel user))
                throw new UnauthorizedAccessException();

            DeleteItemDto param = _mapper.Map<DeleteItemDto>(request);
            param.CreatedBy = user.UserId;

            var deletedItem = await _itemService.Delete(param);
            if (null == deletedItem)
                throw new ArgumentException();
            return deletedItem;
        }

        #endregion
    }
}
