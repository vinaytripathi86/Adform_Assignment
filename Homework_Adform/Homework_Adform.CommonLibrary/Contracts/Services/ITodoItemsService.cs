using Homework_Adform.CommonLibrary.Models;
using Homework_Adform.CommonLibrary.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework_Adform.CommonLibrary.Contracts.Services
{
    /// <summary>
    /// Contract for todoitem service.
    /// </summary>
    public interface ITodoItemsService
    {
        /// <summary>
        /// Get items for particular list based on page size and number.
        /// </summary>
        /// <param name="item">Input parameters.</param>
        /// <param name="parameters">Paging parameters.</param>
        /// <returns>Return items for particular list based on page size and number.</returns>
        Task<PagedList<ItemDto>> Get(GetItemsDto item, PaginationParameters parameters);

        /// <summary>
        /// Add item.
        /// </summary>
        /// <param name="item">Item details.</param>
        /// <returns>Return added item.</returns>
        Task<ItemDto> Add(CreateItemDto item);

        /// <summary>
        /// Get all items created by user.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Returns all items created by user.</returns>
        Task<List<ItemDto>> GetAll(long userId);

        /// <summary>
        /// Get All Items for given list.
        /// </summary>
        /// <param name="listId">List id.</param>
        /// <param name="userId">User id.</param>
        /// <returns>Returns all items for given list.</returns>
        Task<List<ItemDto>> GetAll(long listId, long userId);

        /// <summary>
        /// Update item.
        /// </summary>
        /// <param name="item">Item details.</param>
        /// <returns>Returns updated item.</returns>
        Task<ItemDto> Update(UpdateItemDto item);

        /// <summary>
        /// Delete item.
        /// </summary>
        /// <param name="item">Item details.</param>
        /// <returns>Returns deleted item.</returns>
        Task<ItemDto> Delete(DeleteItemDto item);

        /// <summary>
        /// Get item.
        /// </summary>
        /// <param name="getItem">Input parameters.</param>
        /// <returns>Returns item.</returns>
        Task<ItemDto> GetTodoItem(GetItemDto getItem);
    }
}
