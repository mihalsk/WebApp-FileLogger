using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using webapp.Models;
using webapp.ViewModels;

namespace webapp.Controllers
{
    public class HomeController : Controller
    {
        // логгер
        private readonly ILogger<HomeController> _logger;
        // контекст БД
        readonly ApplicationContext _db;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="context"></param>
        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger; // получаем логгер
            _db = context; // получаем контекст БД для работы
        } 

        /// <summary>
        /// action для стартовой страницы со списком работников
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel() { Employees = _db.Employees }; // можно прямо передать во вьюху список db.Employees вместо IndexViewModel, но так сделано в статье по asp.net core на metanit.com
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString(); // получаем ip клиента
            _logger.LogInformation($"Клиент {ip}: список работников в количестве {_db.Employees.Count()} человек");
            return View(viewModel);
        }

        /// <summary>
        /// action для формы создания работника
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EmployeeCreate()
        {
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _logger.LogInformation($"Клиент {ip}: запрос на создание работника");
            return View();
        } 

        /// <summary>
        /// action для обработки отправленных данных при создании работника
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EmployeeCreate(Employee employee)
        {
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _logger.LogWarning($"Клиент {ip}: создание работника - {employee.Name}");
            _db.Employees.Add(employee); // добавляем работника
            _db.SaveChanges(); // сохраняем в бд все изменения
            return RedirectToAction("Index"); // редиректим на список работников
        }

        /// <summary>
        /// детализация данных о работнике
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EmployeeDetail(int? id)
        {
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _logger.LogInformation($"Клиент {ip}: запрос детализации по работнику - {id}");
            if (id == null) return RedirectToAction("Index"); // если id не передан редиректим на список работников
            return View(_db.Employees.First(e => e.Id == id)); // возвращаем вьюшку с выбранным работником
        }

        /// <summary>
        /// редактирование данных о работнике
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EmployeeEdit(int? id)
        {
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _logger.LogInformation($"Клиент {ip}: запрос на редактирование данных по работнику - {id}");
            if (id == null) return RedirectToAction("Index"); // если id не передан редиректим на список работников
            return View(_db.Employees.First(e => e.Id == id)); // возвращаем вьюшку с выбранным работником
        }

        /// <summary>
        /// сохранение отредактированых данных о работнике в БД
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EmployeeEdit(Employee employee)
        {
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _logger.LogWarning($"Клиент {ip}: обновление данных по работнику - {employee.Id}");
            _db.Employees.Update(employee); // обновляем данные работника
            _db.SaveChanges(); // сохраняем в бд все изменения
            return RedirectToAction("Index"); // редиректим на список работников
        }

        /// <summary>
        /// Удаление работника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EmployeeDelete(int? id)
        {
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _logger.LogInformation($"Клиент {ip}: запрос на удаление данных по работнику - {id}");
            if (id == null) return RedirectToAction("Index"); // если id не передан редиректим на список работников
            return View(_db.Employees.First(e => e.Id == id)); // показываем данные работника с запросом подтверждения
        }

        /// <summary>
        /// Удаление выбранного работника
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EmployeeDelete(Employee employee)
        {
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _logger.LogWarning($"Клиент {ip}: удаление данных по работнику - {employee.Id}");
            _db.Employees.Remove(employee); // удаляем работника
            _db.SaveChanges(); // сохраняем в бд все изменения
            return RedirectToAction("Index"); // редиректим на список работников
        }

        /// <summary>
        /// Отображение ошибки
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
