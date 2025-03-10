using com.sun.xml.@internal.bind.v2.model.core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDiplom.Models;
using System.Security.Claims;
using static com.sun.tools.@internal.xjc.reader.xmlschema.bindinfo.BIConversion;

namespace MyDiplom.Controllers
{
    public class listController : Controller
    {
        private readonly ILogger<listController> _logger;
        private ChildrenContext db = new ChildrenContext();
       
        public listController(ILogger<listController> logger)
        {
            _logger = logger;
        }
        [HttpGet]

        public IActionResult List_employee()
        {
            int userId = GetAuthenticatedUserId(); // Получаем UserId текущего пользователя
            ViewBag.UserId = userId; // Передаем UserId в представление
            var employees = db.Employees.Include(e => e.User).ToList(); // Загружаем пользователей вместе с сотрудниками
            return View(employees);

           
        }

        [HttpGet]

        public IActionResult List_employee_emp()
        {
            int userId = GetAuthenticatedUserId(); // Получаем UserId текущего пользователя
            ViewBag.UserId = userId; // Передаем UserId в представление
            var employees = db.Employees.Include(e => e.User).ToList(); // Загружаем пользователей вместе с сотрудниками
            return View(employees);
        }

        [HttpGet]
        public IActionResult List_employee_admin()
        {
            int userId = GetAuthenticatedUserId(); // Получаем UserId текущего пользователя
            ViewBag.UserId = userId; // Передаем UserId в представление
            var employees = db.Employees.Include(e => e.User).ToList(); // Загружаем пользователей вместе с сотрудниками
            return View(employees);
        }

        [HttpGet]
        public IActionResult List_employee_no_auth()
        {
          
            var employees = db.Employees.Include(e => e.User).ToList(); // Загружаем пользователей вместе с сотрудниками
            return View(employees);
        }

        [HttpGet]
        public IActionResult List_Group()
        {
            int userId = GetAuthenticatedUserId(); // Получаем UserId текущего пользователя
            ViewBag.UserId = userId; // Передаем UserId в представление
            var groups = db.Groups.ToList();
            return View(groups);
        }


        [HttpGet]
        public IActionResult List_Group_employee()
        {
            int userId = GetAuthenticatedUserId(); // Получаем UserId текущего пользователя
            ViewBag.UserId = userId; // Передаем UserId в представление
            var groups = db.Groups.ToList();
            return View(groups);
        }

        [HttpGet]
        public IActionResult List_Group_admin()
        {
            int userId = GetAuthenticatedUserId(); // Получаем UserId текущего пользователя
            ViewBag.UserId = userId; // Передаем UserId в представление
            var groups = db.Groups.ToList();
            return View(groups);
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
        [HttpGet]
        public IActionResult List_children(int groupId)
        {
            int userId = GetAuthenticatedUserId(); // Получаем UserId текущего пользователя
            ViewBag.UserId = userId; // Передаем UserId в представление
            var children = db.Children
                .Include(c => c.Group)
                .Where(c => c.GroupId == groupId)
                .ToList();

            return View(children);
        }




      
    }
}
