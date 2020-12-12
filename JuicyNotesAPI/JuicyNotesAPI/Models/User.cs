using System;
using System.Collections.Generic;

#nullable disable

namespace JuicyNotesAPI.Models
{
    public partial class User
    {
        public User()
        {
            UserCollections = new HashSet<UserCollection>();
        }

        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public virtual ICollection<UserCollection> UserCollections { get; set; }
    }
}
