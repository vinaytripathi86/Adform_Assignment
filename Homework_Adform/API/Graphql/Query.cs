using Homework_Adform.CommonLibrary.Contracts.Services;
using Homework_Adform.CommonLibrary.Models;
using Homework_Adform.CommonLibrary.Models.DTOs;
using Homework_Adform.TodoAPI.Graphql.ModelTypes;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_Adform.TodoAPI.Graphql
{
    /// <summary>
    /// Query class for GraphQl.
    /// </summary>
    public class Query
    {
        private readonly ILabelService _labelService;
        private readonly ITodoListService _listService;
        private readonly ITodoItemsService _itemService;

        /// <summary>
        /// Create new instance of <see cref="Query"/> class.
        /// </summary>
        /// <param name="labelService">Label service.</param>
        /// <param name="itemService">Items service.</param>
        /// <param name="listService">List service.</param>
        public Query([Service]ILabelService labelService, [Service]ITodoItemsService itemService, [Service]ITodoListService listService)
        {
            _labelService = labelService;
            _itemService = itemService;
            _listService = listService;
        }

        #region Labels

        /// <summary>
        /// Get labels.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        /// <returns>Returns labels.</returns>
        [Authorize]
        [UsePaging(SchemaType = typeof(LabelType))]
        [UseFiltering]
        public async Task<IQueryable<LabelDto>> Labels([Service] IHttpContextAccessor contextAccessor)
        {
            if (!(contextAccessor.HttpContext.Request.HttpContext.Items["User"] is UserModel user))
                return null;

            var labels = (await _labelService.GetLabels(user.UserId)).AsQueryable();
            return labels;
        }

        #endregion

        #region TodoItems

        /// <summary>
        /// Get todoitems.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        /// <returns>Returns todoitems.</returns>
        [Authorize]
        [UsePaging(SchemaType = typeof(ItemsType))]
        [UseFiltering]
        public async Task<IQueryable<ItemDto>> Items([Service] IHttpContextAccessor contextAccessor)
        {
            if (!(contextAccessor.HttpContext.Request.HttpContext.Items["User"] is UserModel user))
                return null;

            var lists = (await _itemService.GetAll(user.UserId)).AsQueryable();
            return lists;
        }

        #endregion
        #region Todolists

        /// <summary>
        /// Get todolists.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        /// <returns>Returns todolists.</returns>
        [Authorize]
        [UsePaging(SchemaType = typeof(ListsType))]
        [UseFiltering]
        public async Task<IQueryable<ListDto>> Lists([Service] IHttpContextAccessor contextAccessor)
        {
            if (!(contextAccessor.HttpContext.Request.HttpContext.Items["User"] is UserModel user))
                return null;

            var lists = (await _listService.GetAll(user.UserId)).AsQueryable();
            return lists;
        }

        #endregion
    }
}
