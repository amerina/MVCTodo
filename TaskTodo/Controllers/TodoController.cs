using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskTodo.ApplicationCore.Entities;
using TaskTodo.ApplicationCore.Interfaces;
using TaskTodo.ViewModels;

namespace TaskTodo.Controllers
{
    [Route("[controller]/[action]")]
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;
        //private readonly UserManager<ApplicationUser> _userManager;
        //public TodoController(ITodoItemService todoItemService, UserManager<ApplicationUser> userManager)
        //{
        //    ///接口如此有用的原因就在于，因为它们有助于解耦（分离）你程序里的逻辑。
        //    ///既然这个控制器依赖于 ITodoItemService 接口，而不是任何 特定的 类，它就不知道也不必关心实际使用的是哪个具体的类
        //    ///它可以是任意一个实现了此接口的类。
        //    ///只要它符合该接口的要求，控制器就能工作。这使你可以轻而易举地，独立测试程序的各部分
        //    _todoItemService = todoItemService;

        //    _userManager = userManager;
        //}

        public TodoController(ITodoItemService todoItemService)
        {
            ///接口如此有用的原因就在于，因为它们有助于解耦（分离）你程序里的逻辑。
            ///既然这个控制器依赖于 ITodoItemService 接口，而不是任何 特定的 类，它就不知道也不必关心实际使用的是哪个具体的类
            ///它可以是任意一个实现了此接口的类。
            ///只要它符合该接口的要求，控制器就能工作。这使你可以轻而易举地，独立测试程序的各部分
            _todoItemService = todoItemService;
            
        }

        public async Task<IActionResult> IndexAsync()
        {
            //var currentUser = await _userManager.GetUserAsync(User);
            //if (currentUser == null) return Challenge();

            // 从数据库获取 to-do 条目
            ///await 把代码暂停在 异步(async) 操作上，而后，在底层数据库或者网络请求结束时，从暂停的地方恢复执行。
            ///就是说，你的程序并没有卡住或者阻塞住，因为它可以处理其它的请求。
            var items = await _todoItemService.GetIncompleteItemsAsync("0");
            // 把条目置于 model 中
            var model = new TodoViewModel()
            {
                Items = items
            };
            // 使用 model 渲染视图
            return View(model);
        }

        /// <summary>
        /// 模型绑定(model bind)
        /// 模型绑定流程会查看请求内的数据，并试图智能地把输入的字段和模型里的属性匹配起来。
        /// 换句话说，当用户提交这个表单，并且浏览器 POST 到了此 action，ASP.NET Core 会从表单里提取信息，并存放到那个 newItem 变量里。
        /// 
        /// 请求数据绑定到模型后，ASP.NET Core 还进行了模型核验操作（model validation）。
        /// 核验操作检查从传入请求绑定到模型的数据，鉴别其合理性和有效性。你可以在模型中添加属性，告知 ASP.NET Core 以怎样的方式进行核验。
        /// 首个代码块检查 ModelState（模型核验的结果）是否有效
        /// 
        /// 作为复用 TodoItem 模型的替代方案，还可以创建一个独立的模型（比如叫 NewTodoItem），仅用于这个 action 中，
        /// 并仅具有特定的字段（Title），用于添加新的待办事项条目。
        /// 模型绑定流程依然要用到，但现在，你分离了两个模型，一个用于在数据库中存储待办事项条目，另一个用于绑定传入的请求数据。
        /// 这个方案，也被称作 绑定模型（binding model） 或者 数据传输对象（data transfer object）（DTO）。这个模式常见于更大更复杂的项目
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddItem(TodoItem newItem)
        {
            ///Title 字段上的 [Required] 属性告知 ASP.NET Core 的模型核验器，如果标题缺失或为空，则判定其无效
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            //var currentUser = await _userManager.GetUserAsync(User);
            //if (currentUser == null) return Challenge();

            var successful = await _todoItemService.AddItemAsync("0", newItem.Task, newItem.IsDone);
            if (!successful)
            {
                return BadRequest("Could not add item.");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MarkDone(int id)
        {
            //var currentUser = await _userManager.GetUserAsync(User);
            //if (currentUser == null) return Challenge();

            await _todoItemService.MarkDoneAsync(id, "0");
           
            return RedirectToAction("Index");
        }
    }
}