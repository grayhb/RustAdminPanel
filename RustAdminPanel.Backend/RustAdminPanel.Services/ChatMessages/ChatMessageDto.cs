namespace RustAdminPanel.Services.ChatMessages
{
    public class ChatMessageDto
    {
        public long Channel { get; set; }

        public string Message { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public string Color { get; set; }

        public long Time { get; set; }
    }
}
