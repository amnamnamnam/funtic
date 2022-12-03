using FUNTIK.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace FUNTIK.Models.Repositories
{
    public interface IIngredientRepository
    {
        public void Create(Ingredient ingredient);
        public void Update(Ingredient ingredient);
        public Ingredient? Find(Func<Ingredient, bool> func);
        public List<Ingredient> FindAll(Func<Ingredient, bool> func);
        public void Delete(Ingredient ingredient);
    }

    public class IngredientRepository : IIngredientRepository
    {
        private readonly IServiceProvider serviceProvider;
        public IngredientRepository(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Create(Ingredient ingredient)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Ingredients.Add(ingredient);
                context.SaveChanges();
            }
        }

        public void Delete(Ingredient ingredient)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Ingredients.Remove(ingredient);
                context.SaveChanges();
            }
        }

        public Ingredient? Find(Func<Ingredient, bool> func)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                return context.Ingredients.Where(func).FirstOrDefault();
            }
        }

        public List<Ingredient> FindAll(Func<Ingredient, bool> func)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                return context.Ingredients.Where(func).Where(func).ToList();
            }
        }

        public void Update(Ingredient ingredient)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Ingredients.Update(ingredient);
                context.SaveChanges();
            }
        }
    }
}
