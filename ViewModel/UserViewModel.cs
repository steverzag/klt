using FloraYFaunaAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraYFaunaAPI.ViewModel
{
    public class UserViewModel : BaseDBModel
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserRol Rol { get; set; }
        public bool Enabled { get; set; }
    }
}
