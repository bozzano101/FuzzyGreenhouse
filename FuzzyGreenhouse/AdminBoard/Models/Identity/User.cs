using Microsoft.AspNetCore.Identity;

namespace AdminBoard.Models.Identity
{
    public class User : IdentityUser
    {
        [PersonalData]
        public virtual string FirstName { get; set; }

        [PersonalData]
        public virtual string LastName { get; set; }
    }
}
