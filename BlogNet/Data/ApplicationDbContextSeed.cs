using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNet.Data
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedRolesAndUsers(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            await roleManager.CreateAsync(new IdentityRole("admin"));
            var user = new ApplicationUser()
            {
                Email = "admin@example.com",
                UserName = "admin@example.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, "P@ssword1");
            await userManager.AddToRoleAsync(user, "admin");
        }

        public static async Task SeedCategoriesAndPostsAsync(ApplicationDbContext db)
        {
            var author = await db.Users.FirstOrDefaultAsync(x => x.UserName.Equals("admin@example.com"));

            if (author == null || await db.Categories.AnyAsync())
            {
                return;
            }

            var cat1 = new Category()
            {
                Name = "General",
                Posts = new List<Post>()
                {
                    new Post()
                    {
                        Title = "Welcome to My Blog",
                        Content = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>",
                        PhotoPath = "sample-photo-1.jpg",
                        Slug = "welcome-to-my-blog",
                        Author = author,
                    },
                    new Post()
                    {
                        Title = "A Sunny Day",
                        Content = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>",
                        PhotoPath = "sample-photo-2.jpg",
                        Slug = "a-sunny-day",
                        Author = author,
                    }
                }
            };

            db.Categories.Add(cat1);
            await db.SaveChangesAsync();
        }     
    }
}
