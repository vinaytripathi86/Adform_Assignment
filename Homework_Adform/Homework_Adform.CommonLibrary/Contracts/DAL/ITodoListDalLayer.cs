using Homework_Adform.CommonLibrary.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework_Adform.CommonLibrary.Contracts.DAL
{
    /// <summary>
    /// Contract for todolist data layer.
    /// </summary>
    public interface ITodoListDalLayer
    {
        /// <summary>
        /// Get all todolist.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <returns>Returns all todolists for user.</returns>
        Task<List<ListDto>> Get(GetListsDto param);

        /// <summary>
        /// Get todolist.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <returns>Returns todolist.</returns>
        Task<ListDto> GetTodoList(GetListDto param);

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
