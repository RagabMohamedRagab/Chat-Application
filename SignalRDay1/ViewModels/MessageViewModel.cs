namespace SignalRDay1.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string SenderId { get; set; }
        public string ReciverId { get; set; }
        public string Time { get; set; }
        public string Day { get; set; }
        public int IsSeen { get; set; }
    }
}
