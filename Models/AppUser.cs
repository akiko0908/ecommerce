using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        // ForeignKey
        public ICollection<Order> Orders { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
