using System;
using System.Collections.Generic;

#nullable disable

namespace DropdownDataService.Models
{
    public partial class Roletype
    {
        public Roletype()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string RoleValue { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
