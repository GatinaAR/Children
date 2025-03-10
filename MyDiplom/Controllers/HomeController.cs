using Azure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyDiplom.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace MyDiplom.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ChildrenContext db = new ChildrenContext();
        public int id_user;
        public User user;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index() // Метод без параметров
        {
            return View(new User());
        }

        [HttpGet("home/index/{id?}")] // Метод с параметром id
        public IActionResult Index(int? id)
        {
            //User user = null;

            //if (id.HasValue)
            //{
            //    user = db.Users.Find(id.Value); 
            //}
            //int userId = GetAuthenticatedUserId(); 
            //ViewBag.UserId = userId; 
            //return View(user); 
            User user = null;

            // Проверка на наличие ID
            if (id.HasValue)
            {
                user = db.Users.Find(id.Value); // Получаем пользователя по ID

                // Проверка на существование пользователя
                if (user == null)
                {
                    // Если пользователь не найден, можно вернуть ошибку или перенаправить на страницу с сообщением
                    return NotFound("Пользователь не найден.");
                }
            }
            else
            {
                // Если ID не передан, можно вернуть пустую модель или обработать это как-то иначе
                return BadRequest("ID пользователя не передан.");
            }

            int userId = GetAuthenticatedUserId(); // Получаем UserId текущего пользователя
            ViewBag.UserId = userId; // Передаем UserId в представление
            return View(user); // Передаем пользователя в представление
        }



        [HttpGet]
        public IActionResult Main_employee() // Метод без параметров
        {
            return View(new User());
        }

        [HttpGet("home/Main_employee/{id?}")] // Метод с параметром id
        public IActionResult Main_employee(int? id)
        {
            User user = null;

            if (id.HasValue)
            {
                user = db.Users.Find(id.Value); // Получаем пользователя по ID
            }
            int userId = GetAuthenticatedUserId(); // Получаем UserId текущего пользователя
            ViewBag.UserId = userId; // Передаем UserId в представление
            return View(user); // Передаем пользователя в представление
        }


        [HttpGet]
        public IActionResult Main_admin() // Метод без параметров
        {
            return View(new User());
        }

        [HttpGet("home/Main_admin/{id?}")] // Метод с параметром id
        public IActionResult Main_admin(int? id)
        {
            User user = null;

            if (id.HasValue)
            {
                user = db.Users.Find(id.Value); // Получаем пользователя по ID
            }
            int userId = GetAuthenticatedUserId(); // Получаем UserId текущего пользователя
            ViewBag.UserId = userId; // Передаем UserId в представление
            return View(user); // Передаем пользователя в представление
        }




        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Main_no_auth()
        {
            return View();
        }

    
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            //    User user = db.GetUser(username, password);
            //    if (user != null)
            //    {
            //        var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, username),
            //    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            //};

            //        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            //        if (user.RoleId == 3)
            //        {
            //            return RedirectToAction("Main_employee", new { id = user.UserId });
            //        }
            //        else if (user.RoleId == 2)
            //        {
            //            return RedirectToAction("Index", new { id = user.UserId });
            //        }
            //        else if (user.RoleId == 1)
            //        {
            //            return RedirectToAction("Main_admin", new { id = user.UserId });
            //        }
            //    }

            //    ViewBag.ErrorMessage = "Неправильный пароль или логин.";
            //    return View();
            // Проверка на пустые значения
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.ErrorMessage = "Логин и пароль не могут быть пустыми.";
                return View();
            }

            // Получение пользователя из базы данных
            User user = db.GetUser(username, password);

            // Проверка на существование пользователя
            if (user == null)
            {
                ViewBag.ErrorMessage = "Неправильный логин или пароль.";
                return View();
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            // Перенаправление в зависимости от роли пользователя
            switch (user.RoleId)
            {
                case 3:
                    return RedirectToAction("Main_employee", new { id = user.UserId });
                case 2:
                    return RedirectToAction("Index", new { id = user.UserId });
                case 1:
                    return RedirectToAction("Main_admin", new { id = user.UserId });
                default:
                    ViewBag.ErrorMessage = "Неизвестная роль пользователя.";
                    return View();
            }
        }
  
        public IActionResult Register()
        {
            // Получаем список ролей из базы данных
            var roles = db.GetRoles(); // Предполагается, что у вас есть метод для получения ролей
            ViewBag.Roles = roles; // Передаем роли в представление
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password, string selectedRole, string email, string phone)
        {
            int userId = GetAuthenticatedUserId(); // Получаем UserId текущего пользователя
            ViewBag.UserId = userId; // Передаем UserId в представлениетлнглр
            // Проверка, существует ли уже пользователь с таким именем
            if (db.GetUser(username, password) != null)
            {
                // Пользователь с таким именем уже существует
                ViewBag.ErrorMessage = "Пользователь с таким именем уже существует.";
                var roles = db.GetRoles(); // Получаем роли для повторного отображения
                ViewBag.Roles = roles;
                return View();
            }

            // Получаем ID роли по названию
            var role = db.GetRoleByName(selectedRole); // Предполагается, что у вас есть метод для получения роли по имени

            // Создание нового пользователя
            User newUser = new User
            {
                Username = username,
                PasswordHash = password, // В реальном приложении пароль должен быть зашифрован
                RoleId = role.RoleId, // Используем ID роли
                Email = email,
                Phone = phone
            };

            // Сохранение нового пользователя в базе данных
            db.AddUser(newUser);
           

            //// Успешная регистрация, перенаправьте пользователя на другую страницу или выполните другие действия
            //return RedirectToAction("Login");
            // Успешная регистрация, устанавливаем сообщение в TempData
            TempData["SuccessMessage"] = "Вы успешно зарегистрированы! Пожалуйста, войдите в систему.";

            // Возвращаем представление с формой регистрации
            var rolesList = db.GetRoles(); // Получаем роли для повторного отображения
            ViewBag.Roles = rolesList;
            return View();
        }

        
        private int GetAuthenticatedUserId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
            }
            return 0;
        }


    }
}