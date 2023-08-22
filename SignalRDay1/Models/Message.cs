namespace SignalRDay1.Models {
    public class Message {
        public int Id { get; set; }
        public string Text { get; set; }
        public string SenderId { get; set; }
        public string ReciverId { get; set; }
        public string Time { get; set; } = DateTime.Now.ToString("hh:mm tt");
        public string Day { get; set; } = DateTime.Now.ToString("MM/dd");
        public int IsSeen { get;set; }
    }
}
