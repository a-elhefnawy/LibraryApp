using System.ComponentModel.DataAnnotations;

namespace LibraryApp.PAL.ViewModel
{
    public class UserVM
    {
        public string? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+=]).{8,}$", ErrorMessage = " Password should contain an UpperCase and LowerCase and Special Character and at least 8 chars")]
        public string? Password { get; set; }
    }
}
