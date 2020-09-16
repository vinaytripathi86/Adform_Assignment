using AutoMapper;
using Homework_Adform.CommonLibrary.Contracts.Services;
using Homework_Adform.CommonLibrary.Models;
using Homework_Adform.CommonLibrary.Models.DTOs;
using Homework_Adform.CommonLibrary.Models.Requests;
using Homework_Adform.CommonLibrary.Models.Responses;
using Homework_Adform.TodoAPI.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework_Adform.TodoAPI.Controllers.v1
{
    /// <summary>
    /// Label controller.
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        private readonly ILogger<LabelsController> _logger;
        private readonly ILabelService _labelService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Create new instance of <see cref="LabelsController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="labelService">Label service.</param>
        /// <param name="mapper">Auto mapper.</param>
        public LabelsController(ILogger<LabelsController> logger, ILabelService labelService, IMapper mapper)
        {
            _logger = logger;
            _labelService = labelService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get labels.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Returns labels.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Get(long? id)
        {
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            dynamic result;
            if (!id.HasValue)
            {
                var labels = await _labelService.GetLabels(user.UserId);
                var mappedData = _mapper.Map<List<LabelResponses>>(labels);
                result = new APIResponse<List<LabelResponses>> { IsSucess = true, Result = mappedData };
            }
            else
            {
                var label = await _labelService.GetLabel(id.Value, user.UserId);
                result = new APIResponse<LabelResponses> { IsSucess = true, Result = _mapper.Map<LabelResponses>(label) };
            }

            return Ok(result);
        }

        /// <summary>
        /// Add label.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <param name="version">Api version.</param>
        /// <returns>Returns location of added label.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Post(CreateLabelRequest request, ApiVersion version)
        {
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            if (null == request || string.IsNullOrWhiteSpace(request.Description))
                return BadRequest();

            CreateLabelDTO createLabel = _mapper.Map<CreateLabelDTO>(request);
            createLabel.CreatedBy = user.UserId;

            var addedItem = await _labelService.Add(createLabel);
            return CreatedAtAction(nameof(Get), new { id = addedItem.Id, version = $"{version}" }, request);
        }

        /// <summary>
        /// Delete label.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <returns>Ok if successful.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteLabelRequest request)
        {
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            DeleteLabelDto deleteLabel = _mapper.Map<DeleteLabelDto>(request);
            deleteLabel.CreatedBy = user.UserId;

            var deletedItem = await _labelService.DeleteLabel(deleteLabel);
            if (null == deletedItem)
                return NotFound();

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }

        /// <summary>
        /// Assign label to todolist.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <returns>Ok if successful.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<IActionResult> AssignLabelToList(AssignLabelRequest request)
        {
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            AssignLabelDto assignLabel = _mapper.Map<AssignLabelDto>(request);
            assignLabel.CreatedBy = user.UserId;

            bool isAssigned = await _labelService.AssignLabelToList(assignLabel);
            return Ok(new APIResponse<object> { IsSucess = isAssigned, Result = null });
        }

        /// <summary>
        /// Assign label to todoitem.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <returns>Ok if successful.</returns>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<IActionResult> AssignLabelToItem(AssignLabelRequest request)
        {
            if (!(Request.HttpContext.Items["User"] is UserModel user))
                return Unauthorized();

            AssignLabelDto assignLabel = _mapper.Map<AssignLabelDto>(request);
            assignLabel.CreatedBy = user.UserId;

            bool isAssigned = await _labelService.AssignLabelToItem(assignLabel);
            return Ok(new APIResponse<object> { IsSucess = isAssigned, Result = null });
        }
    }
}