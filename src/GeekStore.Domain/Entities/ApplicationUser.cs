using Microsoft.AspNetCore.Identity;

namespace GeekStore.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Address? Address { get; set; }
}
