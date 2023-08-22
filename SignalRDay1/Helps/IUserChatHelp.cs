using SignalRDay1.ViewModels;

namespace SignalRDay1.Helps
{
    public interface IUserChatHelp {
      public Task<List<UserChatViewModel>> GetUserChats();
        public List<MessageViewModel> GetUserMessages(string UserId);
        public MessageViewModel SendMesg(TextViewModel model);
    }
}
