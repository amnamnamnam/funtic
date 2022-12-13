using FUNTIK.Data;
using Microsoft.EntityFrameworkCore;

namespace FUNTIK.Models.Repositories
{
    public interface IRecipeRepository
    {
        public void Create(Recipe recipe);
        public void Delete(Recipe recipe);
        public Recipe? Find(Func<Recipe, bool> func);
        public List<Recipe> FindAll(Func<Recipe, bool> func);
        public void Update(Recipe recipe);
    }



    public class RecipeRepository : IRecipeRepository
    {
        private readonly IServiceProvider serviceProvider;
        public RecipeRepository(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Create(Recipe recipe)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Recipes.Add(recipe);
                context.SaveChanges();
            }
        }

        public void Delete(Recipe recipe)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Recipes.Remove(recipe);
                context.SaveChanges();
            }
        }

        public Recipe? Find(Func<Recipe, bool> func)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                return context.Recipes.Include(r => r.Ingredients).FirstOrDefault(func);
            }
        }

        public List<Recipe> FindAll(Func<Recipe, bool> func)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                return context.Recipes.Where(func).ToList();
            }
        }

        public void Update(Recipe recipe)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Recipes.Update(recipe);
                context.SaveChanges();
            }
        }
    }
}
