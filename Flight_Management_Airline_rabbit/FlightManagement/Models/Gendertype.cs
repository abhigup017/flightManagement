using System;
using System.Collections.Generic;

#nullable disable

namespace AirlineService.Models
{
    public partial class Gendertype
    {
        public Gendertype()
        {
            Users = new HashSet<User>();
        }

        public int GenderId { get; set; }
        public string GenderValue { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
