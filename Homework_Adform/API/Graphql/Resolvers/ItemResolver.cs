using Homework_Adform.CommonLibrary.Contracts.Services;
using Homework_Adform.CommonLibrary.Models;
using Homework_Adform.CommonLibrary.Models.DTOs;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Resolvers;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework_Adform.TodoAPI.Graphql.Resolvers
{
    /// <summary>
    /// Todoitems resolver.
    /// </summary>
    public class ItemResolver
    {
        private readonly ITodoItemsService _itemsService;

        /// <summary>
        /// Create new instance of <see cref="ItemResolver"/> class.
        /// </summary>
        /// <param name="itemsService">Items service.</param>
        public ItemResolver([Service]ITodoItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        /// <summary>
        /// Get todoitems.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        /// <param name="todoList">Todolist.</param>
        /// <param name="ctx">Resolver context.</param>
        /// <returns>Returns todoitems.</returns>
        [Authorize]
        public async Task<List<ItemDto>> GetItems([Service] IHttpContextAccessor contextAccessor, ListDto todoList, IResolverContext ctx)
        {
            if (!(contextAccessor.HttpContext.Request.HttpContext.Items["User"] is UserModel user))
                return null;

            return await _itemsService.GetAll(todoList.Id, user.UserId);
        }
    }
}
