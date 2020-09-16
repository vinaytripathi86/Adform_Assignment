using Homework_Adform.CommonLibrary.Models;
using Homework_Adform.CommonLibrary.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework_Adform.CommonLibrary.Contracts.Services
{
    /// <summary>
    /// Contract for todolist service.
    /// </summary>
    public interface ITodoListService
    {
        /// <summary>
        /// Get lists based on page size and number.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <param name="pagination">Paging parameters.</param>
        /// <returns>Return lists based on page size and number.</returns>
        Task<PagedList<ListDto>> GetTodoLists(PaginationParameters pagination, GetListsDto param);

        /// <summary>
        /// Get todolist.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <returns>Returns todolist.</returns>
        Task<ListDto> GetTodoList(GetListDto param);

        /// <summary>
        /// Get all lists created by user.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Returns all lists created by user.</returns>
        Task<List<ListDto>> GetAll(long userId);

        /// <summary>
        /// Add todolist.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <returns>Returns added list.</returns>
        Task<ListDto> Add(CreateListDto param);

        /// <summary>
        /// Update todolist.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <returns>Returns updated list.</returns>
        Task<ListDto> Update(UpdateListDto param);

        /// <summary>
        /// Delete todolist.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <returns>Returns deleted list.</returns>
        Task<ListDto> Delete(DeleteListDto param);
    }
}
