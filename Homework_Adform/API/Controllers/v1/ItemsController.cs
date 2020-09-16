using System.Threading.Tasks;
using AutoMapper;
using Homework_Adform.CommonLibrary.Contracts.Services;
using Homework_Adform.CommonLibrary.Models;
using Homework_Adform.CommonLibrary.Models.DTOs;
using Homework_Adform.CommonLibrary.Models.Requests;
using Homework_Adform.CommonLibrary.Models.Responses;
using Homework_Adform.TodoAPI.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Homework_Adform.TodoAPI.Controllers.v1
{
    /// <summary>
    /// TodoItem controller.
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/todo/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly ITodoItemsService _itemsService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Create new instance of <see cref="ItemsController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="itemsService">Todoitems service.</param>
        /// <param name="mapper">Auto mapper.</param>
        public ItemsController(ILogger<ItemsController> logger, ITodoItemsService itemsService, IMapper mapper)
        {
            _logger = logger;
            _itemsService = itemsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get items for particular list based on page size and number.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <returns>Return items for particular list based on page size and number.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetItemsRequest request)
        {
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            GetItemsDto param = _mapper.Map<GetItemsDto>(request);
            param.CreatedBy = user.UserId;
            PagedList<ItemDto> items = await _itemsService.Get(param, request.Pagination);
            var metadata = new
            {
                items.TotalCount,
                items.PageSize,
                items.CurrentPage,
                items.TotalPages,
                items.HasNext,
                items.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(new APIResponse<PagedList<ItemDto>> { IsSucess = true, Result = items });
        }

        /// <summary>
        /// Get item by id.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <returns>Return todoitem.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("getItem")]
        public async Task<IActionResult> GetItem([FromQuery]GetItemRequest request)
        {
            _logger.LogInformation($"inside getitem");
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            GetItemDto param = _mapper.Map<GetItemDto>(request);
            param.CreatedBy = user.UserId;
            ItemDto item = await _itemsService.GetTodoItem(param);
            ItemResponse response = _mapper.Map<ItemResponse>(item);

            return Ok(new APIResponse<ItemResponse> { IsSucess = true, Result = response });
        }

        /// <summary>
        /// Add item.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <param name="version">Api version.</param>
        /// <returns>Returns added items location.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Post(CreateItemRequest request, ApiVersion version)
        {
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            if (null == request || string.IsNullOrWhiteSpace(request.Note))
                return BadRequest();

            CreateItemDto param = _mapper.Map<CreateItemDto>(request);
            param.CreatedBy = user.UserId;

            var addedItem = await _itemsService.Add(param);
            return CreatedAtAction(nameof(GetItem), new { addedItem.Id, addedItem.ListId, version = $"{version}" }, request);
        }

        /// <summary>
        /// Update item.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <returns>Ok if successful.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public async Task<IActionResult> Put(UpdateItemRequest request)
        {
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            if (null == request || string.IsNullOrWhiteSpace(request.Note))
                return BadRequest();

            UpdateItemDto param = _mapper.Map<UpdateItemDto>(request);
            param.CreatedBy = user.UserId;

            var updatedItem = await _itemsService.Update(param);
            if (null == updatedItem)
                return NotFound();

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }

        /// <summary>
        /// Delete item.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <returns>Ok if successful.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteItemRequest request)
        {
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            DeleteItemDto param = _mapper.Map<DeleteItemDto>(request);
            param.CreatedBy = user.UserId;

            var deletedItem = await _itemsService.Delete(param);
            if (null == deletedItem)
                return NotFound();

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }

        /// <summary>
        /// Partial update item.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <param name="listId">List id.</param>
        /// <param name="patchDoc">Partial updated data.</param>
        /// <returns>Returns action result.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(long id, long listId, [FromBody]JsonPatchDocument<UpdateItemRequest> patchDoc)
        {
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            if (patchDoc == null)
            {
                return BadRequest();
            }

            GetItemDto param = new GetItemDto { Id = id, CreatedBy = user.UserId, ListId = listId };
            ItemDto item = await _itemsService.GetTodoItem(param);

            if (item == null)
            {
                return NotFound();
            }

            var updatedItem = _mapper.Map<UpdateItemRequest>(item);
            patchDoc.ApplyTo(updatedItem);
            var isValid = TryValidateModel(item);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            var itemDto = await _itemsService.Update(_mapper.Map<UpdateItemDto>(updatedItem));
            if (null == itemDto)
                return NotFound();

            return NoContent();
        }
    }
}