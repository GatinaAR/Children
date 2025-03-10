using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDiplom.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyDiplom.Controllers
{
    public class ChatController : Controller
    {
        private readonly ChildrenContext _db;
        private readonly ILogger<ChatController> _logger;

        public ChatController(ILogger<ChatController> logger, ChildrenContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public IActionResult Messenger()
        {
            int userId = GetAuthenticatedUserId();
            ViewBag.UserId = userId;
            var userMessages = _db.Messages
                .Where(m => m.SenderId == userId || m.RecipientId == userId)
                .Include(m => m.Sender)
                .Include(m => m.Recipient)
                .ToList();

            var uniqueRecipients = userMessages
                .Select(m => m.SenderId == userId ? m.Recipient : m.Sender)
                .Distinct()
                .ToList();

            return View(uniqueRecipients);
        }
        [HttpGet]
        public IActionResult Messenger_emp()
        {
            int userId = GetAuthenticatedUserId();
            ViewBag.UserId = userId;
            var userMessages = _db.Messages
                .Where(m => m.SenderId == userId || m.RecipientId == userId)
                .Include(m => m.Sender)
                .Include(m => m.Recipient)
                .ToList();

            var uniqueRecipients = userMessages
                .Select(m => m.SenderId == userId ? m.Recipient : m.Sender)
                .Distinct()
                .ToList();

            return View(uniqueRecipients);
        }



        //[HttpPost]
        //public IActionResult Messenger(int recipientId)
        //{
        //    int userId = GetAuthenticatedUserId();
        //    ViewBag.UserId = userId;

        //    var messages = _db.Messages
        //        .Where(m => (m.SenderId == userId && m.RecipientId == recipientId) || (m.SenderId == recipientId && m.RecipientId == userId))
        //        .Include(m => m.Sender) // Загружаем отправителя
        //        .Include(m => m.Recipient) // Загружаем получателя
        //        .OrderBy(m => m.Timestamp)
        //        .ToList();

        //    ViewBag.RecipientName = messages.FirstOrDefault()?.Recipient.Username; // Получаем имя получателя
        //    ViewBag.RecipientId = recipientId; // Сохраняем ID получателя

        //    return View("Chat", messages);
        //}
        [HttpPost]
        public IActionResult Messenger(int recipientId)
        {
            int userId = GetAuthenticatedUserId();
            ViewBag.UserId = userId;

            var messages = _db.Messages
                .Where(m => (m.SenderId == userId && m.RecipientId == recipientId) ||
                             (m.SenderId == recipientId && m.RecipientId == userId))
                .Include(m => m.Sender) // Загружаем отправителя
                .Include(m => m.Recipient) // Загружаем получателя
                .OrderBy(m => m.Timestamp)
                .ToList();

            ViewBag.RecipientName = messages.FirstOrDefault()?.Recipient.Username; // Получаем имя получателя
            ViewBag.RecipientId = recipientId; // Сохраняем ID получателя

            return View("Chat", messages); // Возвращаем представление Chat с обновленными сообщениями
        }

        [HttpPost]
        public IActionResult Messenger_emp(int recipientId)
        {
            int userId = GetAuthenticatedUserId();
            ViewBag.UserId = userId;

            var messages = _db.Messages
                .Where(m => (m.SenderId == userId && m.RecipientId == recipientId) || (m.SenderId == recipientId && m.RecipientId == userId))
                .Include(m => m.Sender) // Загружаем отправителя
                .Include(m => m.Recipient) // Загружаем получателя
                .OrderBy(m => m.Timestamp)
                .ToList();

            ViewBag.RecipientName = messages.FirstOrDefault()?.Recipient.Username; // Получаем имя получателя
            ViewBag.RecipientId = recipientId; // Сохраняем ID получателя

            return View("Chat_emp", messages);
        }


        [HttpPost]
        public async Task<IActionResult> SendMessage(int recipientId, string messageContent)
        {
            int userId = GetAuthenticatedUserId();

            // Проверка на то, что отправитель не может отправить сообщение самому себе
            if (recipientId == userId)
            {
                // Можно вернуть ошибку или просто игнорировать
                ModelState.AddModelError("", "Вы не можете отправить сообщение самому себе.");
                return RedirectToAction("Messenger");
            }

            var newMessage = new Message
            {
                SenderId = userId,
                RecipientId = recipientId,
                Content = messageContent,
                Timestamp = DateTime.Now.ToString()
            };

            _db.Messages.Add(newMessage);
            await _db.SaveChangesAsync();
            return RedirectToAction("Messenger", new { recipientId = recipientId });



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


