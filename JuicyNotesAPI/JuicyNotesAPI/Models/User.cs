using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Salt { get; set; }

        public virtual ICollection<UserCollection> UserCollections { get; set; }
    }
}
