using Homework_Adform.CommonLibrary.Contracts.DAL;
using Homework_Adform.CommonLibrary.Contracts.Services;
using Homework_Adform.CommonLibrary.Models;
using Homework_Adform.CommonLibrary.Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_Adform.Services
{
    /// <summary>
    /// Implemenation of ITodoItemsService contract.
    /// </summary>
    public class TodoItemsService : ITodoItemsService
    {
        private readonly ITodoItemsDalLayer _itemDalLayer;

        /// <summary>
        /// Create new instance of <see cref="TodoItemsService"/> class.
        /// </summary>
        /// <param name="itemDalLayer">Item dal layer.</param>
        public TodoItemsService(ITodoItemsDalLayer itemDalLayer)
        {
            _itemDalLayer = itemDalLayer;
        }

        /// <summary>
        /// Add item.
        /// </summary>
        /// <param name="item">Item details.</param>
        /// <returns>Return added item.</returns>
        public async Task<ItemDto> Add(CreateItemDto item) => await _itemDalLayer.Add(item);

        /// <summary>
        /// Delete item.
        /// </summary>
        /// <param name="item">Item details.</param>
        /// <returns>Returns deleted item.</returns>
        public async Task<ItemDto> Delete(DeleteItemDto item) => await _itemDalLayer.Delete(item);

        /// <summary>
        /// Get items for particular list based on page size and number.
        /// </summary>
        /// <param name="item">Input parameters.</param>
        /// <param name="parameters">Paging parameters.</param>
        /// <returns>Return items for particular list based on page size and number.</returns>
        public async Task<PagedList<ItemDto>> Get(GetItemsDto item, PaginationParameters pagination)
        {
            List<ItemDto> todoItems = await _itemDalLayer.Get(item);
            if (!string.IsNullOrWhiteSpace(pagination.SearchText))
            {
                todoItems = todoItems.Where(p => p.Notes.Contains(pagination.SearchText)).ToList();
            }

            return PagedList<ItemDto>.ToPagedList(todoItems, pagination.PageNumber, pagination.PageSize);
        }

        /// <summary>
        /// Get All Items for given list.
        /// </summary>
        /// <param name="listId">List id.</param>
        /// <param name="userId">User id.</param>
        /// <returns>Returns all items for given list.</returns>
        public async Task<List<ItemDto>> GetAll(long listId, long userId) => await _itemDalLayer.Get(getItems: new GetItemsDto { ListId = listId, CreatedBy = userId });

        /// <summary>
        /// Get all items created by user.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Returns all items created by user.</returns>
        public async Task<List<ItemDto>> GetAll(long userId) => await _itemDalLayer.GetByUserId(userId);

        /// <summary>
        /// Get item.
        /// </summary>
        /// <param name="getItem">Input parameters.</param>
        /// <returns>Returns item.</returns>
        public async Task<ItemDto> GetTodoItem(GetItemDto getItem) => await _itemDalLayer.GetTodoItem(getItem);

        /// <summary>
        /// Update item.
        /// </summary>
        /// <param name="item">Item details.</param>
        /// <returns>Returns updated item.</returns>
        public async Task<ItemDto> Update(UpdateItemDto item) => await _itemDalLayer.Update(item);
    }
}
