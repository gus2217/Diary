using Microsoft.AspNetCore.Identity;

namespace DiaryApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual UserAccount UserAccount { get; set; }
    }
}
