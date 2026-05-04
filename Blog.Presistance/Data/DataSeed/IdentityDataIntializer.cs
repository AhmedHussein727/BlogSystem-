using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Presistance.Data.DataSeed
{
    public class IdentityDataIntializer : IDataIntializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataIntializer> _logger;

        public IdentityDataIntializer(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,ILogger<IdentityDataIntializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task InitializeAsync()
        {
            try
            {
                // Seed Roles
                string[] roles = { "Admin", "Editor", "Reader" };

                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // Seed Admin User
                var existingUser = await _userManager.FindByEmailAsync("AhmedHussein@gmail.com");

                if (existingUser is null)
                {
                    var adminUser = new AppUser
                    {
                        Name = "Ahmed Hussein",
                        UserName = "AhmedHussein@gmail.com",
                        Email = "AhmedHussein@gmail.com",
                        PhoneNumber = "01100982705",
                    };

                    var result = await _userManager.CreateAsync(adminUser, "P@ssw0rd");

                    if (!result.Succeeded)
                    {
                        _logger.LogError("Failed to create admin user: " +
                            string.Join(", ", result.Errors.Select(e => e.Description)));
                        return;
                    }

                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while seeding database");
            }
        }
    }
}
