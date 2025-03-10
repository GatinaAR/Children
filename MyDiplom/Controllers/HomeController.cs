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
        public IActionResult Index() // ����� ��� ����������
        {
            return View(new User());
        }

        [HttpGet("home/index/{id?}")] // ����� � ���������� id
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

            // �������� �� ������� ID
            if (id.HasValue)
            {
                user = db.Users.Find(id.Value); // �������� ������������ �� ID

                // �������� �� ������������� ������������
                if (user == null)
                {
                    // ���� ������������ �� ������, ����� ������� ������ ��� ������������� �� �������� � ����������
                    return NotFound("������������ �� ������.");
                }
            }
            else
            {
                // ���� ID �� �������, ����� ������� ������ ������ ��� ���������� ��� ���-�� �����
                return BadRequest("ID ������������ �� �������.");
            }

            int userId = GetAuthenticatedUserId(); // �������� UserId �������� ������������
            ViewBag.UserId = userId; // �������� UserId � �������������
            return View(user); // �������� ������������ � �������������
        }



        [HttpGet]
        public IActionResult Main_employee() // ����� ��� ����������
        {
            return View(new User());
        }

        [HttpGet("home/Main_employee/{id?}")] // ����� � ���������� id
        public IActionResult Main_employee(int? id)
        {
            User user = null;

            if (id.HasValue)
            {
                user = db.Users.Find(id.Value); // �������� ������������ �� ID
            }
            int userId = GetAuthenticatedUserId(); // �������� UserId �������� ������������
            ViewBag.UserId = userId; // �������� UserId � �������������
            return View(user); // �������� ������������ � �������������
        }


        [HttpGet]
        public IActionResult Main_admin() // ����� ��� ����������
        {
            return View(new User());
        }

        [HttpGet("home/Main_admin/{id?}")] // ����� � ���������� id
        public IActionResult Main_admin(int? id)
        {
            User user = null;

            if (id.HasValue)
            {
                user = db.Users.Find(id.Value); // �������� ������������ �� ID
            }
            int userId = GetAuthenticatedUserId(); // �������� UserId �������� ������������
            ViewBag.UserId = userId; // �������� UserId � �������������
            return View(user); // �������� ������������ � �������������
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

            //    ViewBag.ErrorMessage = "������������ ������ ��� �����.";
            //    return View();
            // �������� �� ������ ��������
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.ErrorMessage = "����� � ������ �� ����� ���� �������.";
                return View();
            }

            // ��������� ������������ �� ���� ������
            User user = db.GetUser(username, password);

            // �������� �� ������������� ������������
            if (user == null)
            {
                ViewBag.ErrorMessage = "������������ ����� ��� ������.";
                return View();
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            // ��������������� � ����������� �� ���� ������������
            switch (user.RoleId)
            {
                case 3:
                    return RedirectToAction("Main_employee", new { id = user.UserId });
                case 2:
                    return RedirectToAction("Index", new { id = user.UserId });
                case 1:
                    return RedirectToAction("Main_admin", new { id = user.UserId });
                default:
                    ViewBag.ErrorMessage = "����������� ���� ������������.";
                    return View();
            }
        }
  
        public IActionResult Register()
        {
            // �������� ������ ����� �� ���� ������
            var roles = db.GetRoles(); // ��������������, ��� � ��� ���� ����� ��� ��������� �����
            ViewBag.Roles = roles; // �������� ���� � �������������
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password, string selectedRole, string email, string phone)
        {
            int userId = GetAuthenticatedUserId(); // �������� UserId �������� ������������
            ViewBag.UserId = userId; // �������� UserId � �������������������
            // ��������, ���������� �� ��� ������������ � ����� ������
            if (db.GetUser(username, password) != null)
            {
                // ������������ � ����� ������ ��� ����������
                ViewBag.ErrorMessage = "������������ � ����� ������ ��� ����������.";
                var roles = db.GetRoles(); // �������� ���� ��� ���������� �����������
                ViewBag.Roles = roles;
                return View();
            }

            // �������� ID ���� �� ��������
            var role = db.GetRoleByName(selectedRole); // ��������������, ��� � ��� ���� ����� ��� ��������� ���� �� �����

            // �������� ������ ������������
            User newUser = new User
            {
                Username = username,
                PasswordHash = password, // � �������� ���������� ������ ������ ���� ����������
                RoleId = role.RoleId, // ���������� ID ����
                Email = email,
                Phone = phone
            };

            // ���������� ������ ������������ � ���� ������
            db.AddUser(newUser);
           

            //// �������� �����������, ������������� ������������ �� ������ �������� ��� ��������� ������ ��������
            //return RedirectToAction("Login");
            // �������� �����������, ������������� ��������� � TempData
            TempData["SuccessMessage"] = "�� ������� ����������������! ����������, ������� � �������.";

            // ���������� ������������� � ������ �����������
            var rolesList = db.GetRoles(); // �������� ���� ��� ���������� �����������
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