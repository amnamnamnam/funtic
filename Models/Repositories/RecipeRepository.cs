using FUNTIK.Data;
using Microsoft.EntityFrameworkCore;

namespace FUNTIK.Models.Repositories
{
    public interface IRecipeRepository
    {
        public void Create(Recipe recipe);
        public void Delete(Recipe recipe);
        public Recipe? GetWithUserById(int id);
        public Recipe? Find(Func<Recipe, bool> func);
        public List<Recipe> FindAll(Func<Recipe, bool> func);
        public void Update(Recipe recipe);
    }



    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDbContext context;
        public RecipeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Recipe recipe)
        {
            context.Recipes.Add(recipe);
            context.SaveChanges();
        }

        public void Delete(Recipe recipe)
        {
            context.Recipes.Remove(recipe);
            context.SaveChanges();
        }

        public Recipe? GetWithUserById(int id)
        {
            var tmp_meta = context.MetaIngredients.ToList();
            return context.Recipes.Include(r => r.Ingredients).Include(r => r.User).FirstOrDefault(r => r.Id == id);
        }

        //отдельно лямбды (не func)
        public Recipe? Find(Func<Recipe, bool> func)
        {
            return context.Recipes.Include(r => r.Ingredients).FirstOrDefault(func);
        }

        public List<Recipe> FindAll(Func<Recipe, bool> func)
        {
            return context.Recipes.Where(func).ToList();
        }

        public void Update(Recipe recipe)
        {
            context.Recipes.Update(recipe);
            context.SaveChanges();
        }
    }
}
