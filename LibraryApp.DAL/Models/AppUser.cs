using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DAL.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; } 
    }
}
