using FUNTIK.Authorization;
using FUNTIK.Models;
using FUNTIK.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

// dotnet aspnet-codegenerator razorpage -m Contact -dc ApplicationDbContext -outDir Pages\Contacts --referenceScriptLibraries
namespace FUNTIK.Data
{
    public static class SeedData
    {
#pragma  warning disable CS8602 // Dereference of a possibly null reference.
        #region snippet_Initialize
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            /*var r = new Recipe { Name = "Шоколад С кофе" };
            var bace = new Ingredient { Name = "Какая-то основа", Type = IngredientType.Base };
            var coffee = new Ingredient { Name = "Кофе", Type = IngredientType.Custom };
            var sugar = new Ingredient { Name = "Сахар" };
            r.Ingredients.Add(bace);
            r.Ingredients.Add(coffee);
                r.Ingredients.Add(sugar);
            var recipeRepository = new RecipeRepository(serviceProvider);
            recipeRepository.Create(r);

            var r2 = new Recipe { Name = "Шоколад просто" };
            var bace2 = new Ingredient { Name = "Какая-то основа 2", Type = IngredientType.Base };
            r2.Ingredients.Add(bace2);
            //r2.Ingredients.Add(sugar);
            recipeRepository.Create(r2);
            //var rp = recipeRepository.Find(x => x.Name == "Шоколад С кофе");
            //Debug.WriteLine("This is ingredients"   );
            //recipeRepository.Delete(r);*/
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@contoso.com");
                await EnsureRole(serviceProvider, adminID, Constants.ContactAdministratorsRole);

                // allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@contoso.com");
                await EnsureRole(serviceProvider, managerID, Constants.ContactManagersRole);

                SeedDB(context, adminID);
                //context.Ingredients.Add(new Ingredient { Name = "Test ingredient", Type = IngredientType.Base });
                //Console.WriteLine("Add ingredient");
                //context.SaveChanges();
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            //if (userManager == null)      
            //{
            //    throw new Exception("userManager is null");
            //}

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        #endregion
        #region snippet1
        public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (context.Contact.Any())
            {
                return;   // DB has been seeded
            }

            context.Contact.AddRange(
            #region snippet_Contact
                new Contact
                {
                    Name = "Debra Garcia",
                    Email = "debra@example.com",
                    Status = ContactStatus.Approved,
                    OwnerID = adminID
                },
            #endregion
            #endregion
                new Contact
                {
                    Name = "Lesha",
                    Email = "thorsten@example.com",
                    Status = ContactStatus.Submitted,
                    OwnerID = adminID
                },
                new Contact
                {
                    Name = "Natasha",
                    Email = "yuhong@example.com",
                    Status = ContactStatus.Rejected,
                    OwnerID = adminID
                },
                new Contact
                {
                    Name = "Masha",
                    Email = "jon@example.com",
                    Status = ContactStatus.Submitted,
                    OwnerID = adminID
                }
            );
            context.SaveChanges();
        }
    }
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.