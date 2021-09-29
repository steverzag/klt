using System;

namespace FloraYFaunaAPI.ViewModel
{
    public class ContactViewModel : BaseDBModel
    {
        public  Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool MarkAsRead { get; set; }
    }
}
