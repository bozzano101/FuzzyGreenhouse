using Microsoft.AspNetCore.Identity;

namespace AdminBoard.Models.Identity
{
    public class User : IdentityUser
    {
        [PersonalData]
        public virtual string FirstName { get; set; }

        [PersonalData]
        public virtual string LastName { get; set; }

        [PersonalData]
        public virtual string Address { get; set; }

        [PersonalData]
        public virtual string City { get; set; }

        [PersonalData]
        public virtual int PostalCode { get; set; }

        [PersonalData]
        public virtual string Country { get; set; }

        [PersonalData]
        public virtual string Phone { get; set; }
    }
}
