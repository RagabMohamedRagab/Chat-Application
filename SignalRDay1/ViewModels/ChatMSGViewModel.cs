namespace SignalRDay1.ViewModels {
    public class ChatMSGViewModel {
        public string Text { get; set; }
        public string Time { get; set; } = DateTime.Now.ToString("hh:mm tt");
        public string Day { get; set; } = DateTime.Now.ToString("MM/dd");
        public int IsSeen { get; set; }
    }
}
