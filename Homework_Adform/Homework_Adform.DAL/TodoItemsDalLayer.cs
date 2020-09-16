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
    /// Implemenation of ITodoItemsDalLayer contract.
    /// </summary>
    public class TodoItemsDalLayer : ITodoItemsDalLayer
    {
        private readonly HomeworkDBContext _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Create new instance of <see cref="TodoItemsDalLayer"/> class.
        /// </summary>
        /// <param name="mapper">Auto mapper.</param>
        /// <param name="dBContext">Db context.</param>
        public TodoItemsDalLayer(IMapper mapper, HomeworkDBContext dBContext)
        {
            _mapper = mapper;
            _dbContext = dBContext;
        }

        /// <summary>
        /// Add item.
        /// </summary>
        /// <param name="item">Item details.</param>
        /// <returns>Return added item.</returns>
        public async Task<ItemDto> Add(CreateItemDto item)
        {
            TodoItemDBModel todoItem = _mapper.Map<TodoItemDBModel>(item);
            _dbContext.TodoItems.Add(todoItem);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ItemDto>(todoItem);
        }

        /// <summary>
        /// Delete item.
        /// </summary>
        /// <param name="item">Item details.</param>
        /// <returns>Returns delete item.</returns>
        public async Task<ItemDto> Delete(DeleteItemDto item)
        {
            var dbItem = await _dbContext.TodoItems.FirstOrDefaultAsync(p => p.Id == item.Id && p.CreatedBy == item.CreatedBy && p.ListId == item.ListId);
            if(null == dbItem)
                return null;

            _dbContext.TodoItems.Remove(dbItem);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ItemDto>(dbItem);
        }

        /// <summary>
        /// Get item.
        /// </summary>
        /// <param name="getItem">Input parameters.</param>
        /// <returns>Returns item.</returns>
        public async Task<ItemDto> GetTodoItem(GetItemDto item)
        {
            var dbItem = await _dbContext.TodoItems.FirstOrDefaultAsync(p => p.Id == item.Id && p.CreatedBy == item.CreatedBy && p.ListId == item.ListId);
            return _mapper.Map<ItemDto>(dbItem);
        }

        /// <summary>
        /// Get all items.
        /// </summary>
        /// <param name="getItems">Input parameters.</param>
        /// <returns>Return all items for particular list.</returns>
        public async Task<List<ItemDto>> Get(GetItemsDto item)
        {
            var dbItems = await _dbContext.TodoItems.Where(p => p.CreatedBy == item.CreatedBy && p.ListId == item.ListId).ToListAsync();
            return _mapper.Map<List<ItemDto>>(dbItems);
        }

        /// <summary>
        /// Update item.
        /// </summary>
        /// <param name="item">Item details.</param>
        /// <returns>Returns updated item.</returns>
        public async Task<ItemDto> Update(UpdateItemDto item)
        {
            var dbItem = await _dbContext.TodoItems.FirstOrDefaultAsync(p => p.Id == item.Id && p.CreatedBy == item.CreatedBy && p.ListId == item.ListId);
            if (null == dbItem)
                return null;

            dbItem = _mapper.Map<TodoItemDBModel>(item);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ItemDto>(dbItem);
        }

        /// <summary>
        /// Get all items created by user.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Returns all items created by user.</returns>
        public async Task<List<ItemDto>> GetByUserId(long userId)
        {
            var dbItem = await _dbContext.TodoItems.Where(p => p.CreatedBy == userId).ToListAsync();
            return _mapper.Map<List<ItemDto>>(dbItem);
        }
    }
}
