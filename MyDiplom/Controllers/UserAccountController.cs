using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDiplom.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace MyDiplom.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly ILogger<UserAccountController> _logger;
        private ChildrenContext db = new ChildrenContext();
        public int id_user;
        public User user;
        public UserAccountController(ILogger<UserAccountController> logger)
        {
            _logger = logger;
        }
      

        [HttpGet("UserAccount/Account/{id?}")] // Метод с параметром id
        public IActionResult Account(int id)
        {
            id_user = id;

            if (id == 0)
            {
                return BadRequest();
            }

            var user = db.Users.Include(u => u.Role).FirstOrDefault(e => e.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            int userId = GetAuthenticatedUserId(); // Получаем UserId текущего пользователя
            ViewBag.UserId = userId; // Передаем UserId в представление

            if (user.RoleId == 3)
            {
                var employeeData = db.Employees.FirstOrDefault(e => e.UserId == id);
                return View("Account_employee", employeeData);
            }
            else if (user.RoleId == 2)
            {
                var childrenData = user.Children; 
                return View("Account", user); 
            }
            else if (user.RoleId == 1)
            {
                  return View("Account_admin", user); 
            }
            return View("DefaultView");
            
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

        [HttpGet("UserAccount/Account_employee/{id?}")] // Метод с параметром id
        public IActionResult Account_employee(int id)
        {
            id_user = id;

            if (id == 0)
            {
                return BadRequest();
            }

            int userId = GetAuthenticatedUserId(); // Получаем UserId текущего пользователя
            ViewBag.UserId = userId; // Передаем UserId в представление
            var user = db.Users.Include(u => u.Role).FirstOrDefault(e => e.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            var employeeData = db.Employees.FirstOrDefault(e => e.UserId == id);
            return View("Account_employee", employeeData);
            
           

        }

        [HttpGet("UserAccount/Account_admin/{id?}")] // Метод с параметром id
        public IActionResult Account_admin(int id)
        {
            id_user = id;

            if (id == 0)
            {
                return BadRequest();
            }

            int userId = GetAuthenticatedUserId(); // Получаем UserId текущего пользователя
            ViewBag.UserId = userId; // Передаем UserId в представление
            var user = db.Users.Include(u => u.Role).FirstOrDefault(e => e.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
           
            return View("Account_admin", user);



        }



        [HttpPost]
        public async Task<IActionResult> UploadPhoto(int userId, IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                ModelState.AddModelError("Photo", "Выберите файл для загрузки.");
                return RedirectToAction("Account", new { id = userId });
            }

            var user = await db.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Сохраняем изображение в виде массива байтов
            user.Photo = await GetBytesFromFormFile(photo);

            await db.SaveChangesAsync();

            return RedirectToAction("Account", new { id = userId });
        }

        public IActionResult GetPhoto(int id)
        {
            var user = db.Users.Find(id);
            if (user?.Photo != null)
            {
                return File(user.Photo, "image/jpeg");
            }

            return NotFound();
        }

        private async Task<byte[]> GetBytesFromFormFile(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

     
    }
}



