using Homework_Adform.CommonLibrary.Contracts.Services;
using Homework_Adform.CommonLibrary.Models;
using Homework_Adform.CommonLibrary.Models.DTOs;
using Homework_Adform.TodoAPI.Controllers;
using Homework_Adform.TodoAPI.Controllers.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework_Adform.Tests.ControllersTests
{
    /// <summary>
    /// Base class for controller tests.
    /// </summary>
    public class BaseController : MapperInitiator
    {
        public ControllerContext Context { get; }
        public ApiVersion Version { get; }

        public Mock<ILogger<LabelsController>> LabelLogger { get; }
        public Mock<ILogger<ItemsController>> ItemsLogger { get; }
        public Mock<ILogger<ListsController>> ListLogger { get; }
        public Mock<ILogger<UserController>> UserLogger { get; }
        public Mock<ILabelService> LabelService { get; }
        public Mock<ITodoItemsService> ItemsService { get; }
        public Mock<ITodoListService> ListService { get; }
        public Mock<IUserService> UserService { get; }

        private ItemDto _itemDto = new ItemDto { Id = 1 };
        private LabelDto _labelDto = new LabelDto { Id = 1 };
        private ListDto _listDto = new ListDto { Id = 1 };

        protected BaseController()
        {
            Context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            Context.HttpContext.Request.HttpContext.Items["User"] = new UserModel { UserId = 1 };
            Version = new ApiVersion(1, 0);
            LabelService = new Mock<ILabelService>();
            ItemsService = new Mock<ITodoItemsService>();
            ListService = new Mock<ITodoListService>();
            UserService = new Mock<IUserService>();
            LabelLogger = new Mock<ILogger<LabelsController>>();
            ItemsLogger = new Mock<ILogger<ItemsController>>();
            ListLogger = new Mock<ILogger<ListsController>>();
            UserLogger = new Mock<ILogger<UserController>>();

            //Mock methods
            LabelService.Setup(p => p.Add(It.IsAny<CreateLabelDTO>())).Returns(Task.FromResult(_labelDto));
            LabelService.Setup(p => p.DeleteLabel(It.IsAny<DeleteLabelDto>())).Returns(Task.FromResult(_labelDto));
            ItemsService.Setup(p => p.Add(It.IsAny<CreateItemDto>())).Returns(Task.FromResult(_itemDto));
            ItemsService.Setup(p => p.Update(It.IsAny<UpdateItemDto>())).Returns(Task.FromResult(_itemDto));
            ItemsService.Setup(p => p.Delete(It.IsAny<DeleteItemDto>())).Returns(Task.FromResult(_itemDto));
            ItemsService.Setup(p => p.GetTodoItem(It.IsAny<GetItemDto>())).Returns(Task.FromResult(_itemDto));
            ListService.Setup(p => p.Add(It.IsAny<CreateListDto>())).Returns(Task.FromResult(_listDto));
            ListService.Setup(p => p.Update(It.IsAny<UpdateListDto>())).Returns(Task.FromResult(_listDto));
            ListService.Setup(p => p.Delete(It.IsAny<DeleteListDto>())).Returns(Task.FromResult(_listDto));
            ListService.Setup(p => p.GetTodoList(It.IsAny<GetListDto>())).Returns(Task.FromResult(_listDto));
            UserService.Setup(p => p.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<long?>(1));
        }
    }
}
