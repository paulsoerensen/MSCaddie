using Microsoft.AspNetCore.Identity;

namespace MSCaddie.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int MemberNo { get; set; }
    }

}
