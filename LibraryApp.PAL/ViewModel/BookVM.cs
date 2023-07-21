using LibraryApp.DAL.Data;
using LibraryApp.DAL.Models;
using Microsoft.AspNetCore.Authentication;
//using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.PAL.ViewModel
{
    public class BookVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Stock { get; set; }

        public int? Borrowed { get; set; }
    }


}
