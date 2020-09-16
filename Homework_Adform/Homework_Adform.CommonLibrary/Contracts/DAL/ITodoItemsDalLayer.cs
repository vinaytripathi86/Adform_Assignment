using Homework_Adform.CommonLibrary.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework_Adform.CommonLibrary.Contracts.DAL
{
    /// <summary>
    /// Contract for todoitem data layer.
    /// </summary>
    public interface ITodoItemsDalLayer
    {
        /// <summary>
        /// Add item.
        /// </summary>
        /// <param name="item">Item details.</param>
        /// <returns>Return added item.</returns>
        Task<ItemDto> Add(CreateItemDto item);

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
        /// Get all items.
        /// </summary>
        /// <param name="getItems">Input parameters.</param>
        /// <returns>Return all items for particular list.</returns>
        Task<List<ItemDto>> Get(GetItemsDto getItems);

        /// <summary>
        /// Get item.
        /// </summary>
        /// <param name="getItem">Input parameters.</param>
        /// <returns>Returns item.</returns>
        Task<ItemDto> GetTodoItem(GetItemDto getItem);

        /// <summary>
        /// Get all items created by user.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Returns all items created by user.</returns>
        Task<List<ItemDto>> GetByUserId(long userId);
    }
}
