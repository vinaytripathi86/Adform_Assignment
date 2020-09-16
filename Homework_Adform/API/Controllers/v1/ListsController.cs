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
using System.Threading.Tasks;

namespace Homework_Adform.TodoAPI.Controllers.v1
{
    /// <summary>
    /// Todolists controller.
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/todo/[controller]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly ILogger<ListsController> _logger;
        private readonly ITodoListService _listService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Create new instance of <see cref="ListsController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="listService">Todolist service.</param>
        /// <param name="mapper">Auto mapper.</param>
        public ListsController(ILogger<ListsController> logger, ITodoListService listService, IMapper mapper)
        {
            _logger = logger;
            _listService = listService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get paginating todolists.
        /// </summary>
        /// <param name="parameters">Pagination parameters.</param>
        /// <returns>Returns pagination todolists.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PaginationParameters parameters)
        {
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            GetListsDto param = new GetListsDto { CreatedBy = user.UserId };
            PagedList<ListDto> items = await _listService.GetTodoLists(parameters, param);
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
            return Ok(new APIResponse<PagedList<ListDto>> { IsSucess = true, Result = items });
        }

        /// <summary>
        /// Get todolist.
        /// </summary>
        /// <param name="Id">List id.</param>
        /// <returns>Returns todolist.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("getList")]
        public async Task<IActionResult> GetList(long Id)
        {
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            GetListDto param = new GetListDto { Id = Id, CreatedBy = user.UserId };
            ListDto item = await _listService.GetTodoList(param);
                if (null == item)
                    return NotFound();

            ListResponse listResponse = _mapper.Map<ListResponse>(item);

            return Ok(new APIResponse<ListResponse> { IsSucess = true, Result = listResponse });
        }

        /// <summary>
        /// Add todolist.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <param name="version">Api version.</param>
        /// <returns>Returns added todolist location.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Post(CreateListRequest request, ApiVersion version)
        {
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            if (null == request || string.IsNullOrWhiteSpace(request.Description))
                return BadRequest();

            CreateListDto param = _mapper.Map<CreateListDto>(request);
            param.CreatedBy = user.UserId;

            var addedItem = await _listService.Add(param);
            return CreatedAtAction(nameof(GetList), new { addedItem.Id, version = $"{version}" }, request);
        }

        /// <summary>
        /// Update todolist.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <returns>Ok if successful.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public async Task<IActionResult> Put(UpdateListRequest request)
        {
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            if (null == request || string.IsNullOrWhiteSpace(request.Description))
                return BadRequest();

            UpdateListDto param = _mapper.Map<UpdateListDto>(request);
            param.CreatedBy = user.UserId;

            var updatedItem = await _listService.Update(param);
            if (null == updatedItem)
                return NotFound();

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }

        /// <summary>
        /// Delete todolist.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <returns>Ok if successful.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteListRequest request)
        {
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            DeleteListDto param = _mapper.Map<DeleteListDto>(request);
            param.CreatedBy = user.UserId;

            var deletedItem = await _listService.Delete(param);
            if (null == deletedItem)
                return NotFound();

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }

        /// <summary>
        /// Partial update list.
        /// </summary>
        /// <param name="id">List id.</param>
        /// <param name="patchDoc">Partial updated data.</param>
        /// <returns>Returns action result.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(long id, [FromBody]JsonPatchDocument<UpdateListRequest> patchDoc)
        {
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            if (patchDoc == null)
            {
                return BadRequest();
            }

            GetListDto param = new GetListDto { Id = id, CreatedBy = user.UserId };
            ListDto item = await _listService.GetTodoList(param);

            if (item == null)
            {
                return NotFound();
            }

            var updatedList = _mapper.Map<UpdateListRequest>(item);
            patchDoc.ApplyTo(updatedList);
            var isValid = TryValidateModel(item);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            var listDto = await _listService.Update(_mapper.Map<UpdateListDto>(updatedList));
            if (null == listDto)
                return NotFound();

            return NoContent();
        }
    }
}