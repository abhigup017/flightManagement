using System;
using System.Collections.Generic;

#nullable disable

namespace DropdownDataService.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public string EmailId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual Gendertype Gender { get; set; }
        public virtual Roletype Role { get; set; }
    }
}
