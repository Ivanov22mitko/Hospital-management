using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HM.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(30)]
        public string? FirstName { get; set; }

        [StringLength(30)]
        public string? LastName { get; set; }
    }
}
