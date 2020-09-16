using AutoMapper;
using Homework_Adform.CommonLibrary.Contracts.DAL;
using Homework_Adform.CommonLibrary.Models.DBModels;
using Homework_Adform.CommonLibrary.Models.DTOs;
using Homework_Adform.DAL.DBContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_Adform.DAL
{
    /// <summary>
    /// Implemenation of ITodoListDalLayer contract.
    /// </summary>
    public class TodoListDalLayer : ITodoListDalLayer
    {
        private readonly HomeworkDBContext _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Create new instance of <see cref="TodoListDalLayer"/> class.
        /// </summary>
        /// <param name="mapper">Auto mapper.</param>
        /// <param name="dBContext">Db context.</param>
        public TodoListDalLayer(IMapper mapper, HomeworkDBContext dBContext)
        {
            _mapper = mapper;
            _dbContext = dBContext;
        }

        /// <summary>
        /// Add todolist.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <returns>Returns added list.</returns>
        public async Task<ListDto> Add(CreateListDto param)
        {
            TodoListDbModel dbList = _mapper.Map<TodoListDbModel>(param);
            _dbContext.TodoLists.Add(dbList);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ListDto>(dbList);
        }

        /// <summary>
        /// Delete todolist.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <returns>Returns deleted list.</returns>
        public async Task<ListDto> Delete(DeleteListDto param)
        {
            var dbItem = await _dbContext.TodoLists.FirstOrDefaultAsync(p => p.Id == param.Id && p.CreatedBy == param.CreatedBy);
            if (null == dbItem)
                return null;

            _dbContext.TodoLists.Remove(dbItem);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ListDto>(dbItem);
        }

        /// <summary>
        /// Get all todolist.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <returns>Returns all todolists for user.</returns>
        public async Task<List<ListDto>> Get(GetListsDto param)
        {
            var dbItem = await _dbContext.TodoLists.Include(p=> p.Items).Where(p => p.CreatedBy == param.CreatedBy).ToListAsync();
            return _mapper.Map<List<ListDto>>(dbItem);
        }

        /// <summary>
        /// Get todolist.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <returns>Returns todolist.</returns>
        public async Task<ListDto> GetTodoList(GetListDto param)
        {
            var dbItem = await _dbContext.TodoLists.Include(p => p.Items).FirstOrDefaultAsync(p => p.Id == param.Id && p.CreatedBy == param.CreatedBy);
            return _mapper.Map<ListDto>(dbItem);
        }

        /// <summary>
        /// Update todolist.
        /// </summary>
        /// <param name="param">Input parameters.</param>
        /// <returns>Returns udpated list.</returns>
        public async Task<ListDto> Update(UpdateListDto param)
        {
            var dbItem = await _dbContext.TodoLists.FirstOrDefaultAsync(p => p.Id == param.Id && p.CreatedBy == param.CreatedBy);
            if (null == dbItem)
                return null;

            dbItem = _mapper.Map<TodoListDbModel>(param);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ListDto>(dbItem);
        }
    }
}
