using Microsoft.AspNetCore.Identity;
using SignalRDay1.Models;
using SignalRDay1.ViewModels;

namespace SignalRDay1.Helps {
    public class UserChatHelp : IUserChatHelp {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ChattingDbContext _chattingDbContext;

        public UserChatHelp(UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, ChattingDbContext chattingDbContext)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _chattingDbContext = chattingDbContext;
        }

        public async Task<List<UserChatViewModel>> GetUserChats()
        {
            var userId = _httpContextAccessor.HttpContext?.Request.Cookies["UserId"];
            // Get All User Expect Me
            var Users = _userManager.Users.ToList().Where(b => b.Id != userId);
            List<UserChatViewModel> result = new List<UserChatViewModel>();
            foreach (var user in Users)
            {
                UserChatViewModel model = new UserChatViewModel()
                {
                    ImgProfile = "/Profile.png",
                    Name = user.UserName?.Substring(0, user.UserName.IndexOf('@')),
                    UserId = user.Id,
                };
                result.Add(model);
            }

            return result;
        }

        public List<MessageViewModel> GetUserMessages(string ReciverId)
        {
            var userId = _httpContextAccessor.HttpContext?.Request.Cookies["UserId"];
            var Msgs = _chattingDbContext.Messages.ToList().Where(user => (user.SenderId == userId && user.ReciverId == ReciverId)|| (user.ReciverId == userId && user.SenderId == ReciverId));
            List<MessageViewModel> messages = new List<MessageViewModel>();
            foreach (var item in Msgs)
            {
                MessageViewModel message = new MessageViewModel()
                {
                    Id = item.Id,
                    ReciverId = item.ReciverId,
                    SenderId = item.SenderId,
                    Text = item.Text,
                    Day = item.Day,
                    Time = item.Time,
                    IsSeen = item.IsSeen,
                };
                messages.Add(message);
            }
            return messages;
        }

        public MessageViewModel SendMesg(TextViewModel model)
        {
            if (model is null)
                return null;

            Message message = new Message()
            {
                Text = model.Text,
                ReciverId = model.ReciverId,
                SenderId = model.SenderId
            };
            _chattingDbContext.Add<Message>(message);
            _chattingDbContext.SaveChanges();
            return new MessageViewModel()
            {
                Day = message.Day,
                Id = message.Id,
                ReciverId = message.ReciverId,
                SenderId = message.SenderId,
                IsSeen = message.IsSeen,
                Text = message.Text   ,
                Time= message.Time
            };
        }
    }
}
