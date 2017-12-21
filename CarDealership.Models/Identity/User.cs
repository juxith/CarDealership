using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace CarDealership.Models.Identity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
    }
}
