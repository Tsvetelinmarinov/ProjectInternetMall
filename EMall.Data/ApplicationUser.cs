using Microsoft.AspNetCore.Identity;
namespace EMall.Data;

// Add profile data for application users by adding properties to the ApplicationUser class.
// This ApplicationUser should not be here, but if i move it i will brake the app.
public class ApplicationUser : IdentityUser
{
}