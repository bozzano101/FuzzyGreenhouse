using System.ComponentModel.DataAnnotations;

namespace AdminBoard.Models
{
    public class UserViewModel
    {
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "UserName")]
        public string Username { get; set; }

        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }
    }
}
