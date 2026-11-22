using Microsoft.AspNetCore.Identity;

namespace EMall.Data.SeedData;

public static class DemoUserSeeder
{
    public static async Task SeedAsync(
        UserManager<ApplicationUser> userManager,
        string? initialPassword
    )
    {
        var missingCustomers = new List<(string Name, string Email)>();

        foreach (var customer in DemoCustomerSeeds.All)
        {
            if (await userManager.FindByEmailAsync(customer.Email) is null)
            {
                missingCustomers.Add(customer);
            }
        }

        if (missingCustomers.Count == 0)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(initialPassword))
        {
            Console.WriteLine(
                "Demo accounts were not seeded. Configure DemoUsers:InitialPassword using user-secrets or an environment variable."
            );

            return;
        }

        foreach (var customer in missingCustomers)
        {
            var user = new ApplicationUser
            {
                UserName = customer.Email,
                Email = customer.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, initialPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(error => error.Description));

                throw new InvalidOperationException(
                    $"Could not create demo account '{customer.Email}': {errors}"
                );
            }
        }

        Console.WriteLine($"Created {missingCustomers.Count} demo user accounts.");
    }
}
