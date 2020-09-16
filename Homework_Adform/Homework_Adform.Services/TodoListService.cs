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
    /// Implemenation of ITodoListService contract.
    /// </summary>
    public class TodoListService : ITodoListService
    {
        private readonly ITodoListDalLayer _listDalLayer;

        /// <summary>
        /// Create new instance of <see cref="TodoListService"/> class.
        /// </summary>
        /// <param name="listDalLayer">Todolist dal layer.</param>
        public TodoListService(ITodoListDalLayer listDalLayer)
        {
            _listDalLayer = listDalLayer;
        }

        /// <summary>
        /// Add todolist.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <returns>Returns added list.</returns>
        public async Task<ListDto> Add(CreateListDto param) => await _listDalLayer.Add(param);

        /// <summary>
        /// Delete todolist.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <returns>Returns delete list.</returns>
        public async Task<ListDto> Delete(DeleteListDto param) => await _listDalLayer.Delete(param);

        /// <summary>
        /// Get all lists created by user.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Returns all lists created by user.</returns>
        public async Task<List<ListDto>> GetAll(long userId) => await _listDalLayer.Get(param: new GetListsDto { CreatedBy = userId });

        /// <summary>
        /// Get todolist.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <returns>Returns todolist.</returns>
        public async Task<ListDto> GetTodoList(GetListDto param) => await _listDalLayer.GetTodoList(param);

        /// <summary>
        /// Get lists based on page size and number.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <param name="pagination">Paging parameters.</param>
        /// <returns>Return lists based on page size and number.</returns>
        public async Task<PagedList<ListDto>> GetTodoLists(PaginationParameters pagination, GetListsDto param)
        {
            List<ListDto> todoLists = await _listDalLayer.Get(param);
            if(!string.IsNullOrWhiteSpace(pagination.SearchText))
            {
                todoLists = todoLists.Where(p => p.Description.Contains(pagination.SearchText)).ToList();
            }

            return PagedList<ListDto>.ToPagedList(todoLists, pagination.PageNumber, pagination.PageSize);
        }

        /// <summary>
        /// Update todolist.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <returns>Returns updated list.</returns>
        public async Task<ListDto> Update(UpdateListDto param) => await _listDalLayer.Update(param);
    }
}
