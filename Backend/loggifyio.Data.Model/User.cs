using System.Collections.Generic;

namespace Loggifyio.Data.Model
{
    public class User
    {
        public User()
        {
            Roles = new List<UserRoles>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }

        public List<UserRoles> Roles { get; set; }
    }
}