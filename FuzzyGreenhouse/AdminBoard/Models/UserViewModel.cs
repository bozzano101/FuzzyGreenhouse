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

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "PostalCode")]
        public int PostalCode { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }
    }
}
