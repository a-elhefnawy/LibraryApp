//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.PAL.ViewModel
{
    public class LogInVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
