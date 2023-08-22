using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRDay1.Helps;
using SignalRDay1.Models;
using SignalRDay1.ViewModels;

namespace SignalRDay1.Controllers {
    [Authorize]
    public class ChatController : Controller {
        private readonly ChattingDbContext _context;
        private readonly IUserChatHelp _userChat;
        public ChatController(ChattingDbContext context, IUserChatHelp userChat)
        {
            _context = context;
            _userChat = userChat;
        }

        public IActionResult Index()
        {
            var GetAll = _context.Chats.ToList();
            return View(GetAll);
        }

        [HttpGet]
        public async Task<IActionResult> UserChat()
        {
            return View(await _userChat.GetUserChats());
        }
        [HttpGet]
        public JsonResult ChatBetweenUsers(string userId)
        {
            return Json(_userChat.GetUserMessages(userId));
        }
        [HttpPost]
        public JsonResult MSGUser(TextViewModel model)
        {

            return  Json(_userChat.SendMesg(model)) ;
        }

    }
}
