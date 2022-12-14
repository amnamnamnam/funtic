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
        private readonly ApplicationDbContext context;
        public IngredientRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Ingredient ingredient)
        {
            context.Ingredients.Add(ingredient);
            context.SaveChanges();
        }

        public void Delete(Ingredient ingredient)
        {
            context.Ingredients.Remove(ingredient);
            context.SaveChanges();
        }

        public Ingredient? Find(Func<Ingredient, bool> func)
        {
            return context.Ingredients.FirstOrDefault(func);
        }

        public List<Ingredient> FindAll(Func<Ingredient, bool> func)
        {
            return context.Ingredients.Where(func).ToList();
        }

        public void Update(Ingredient ingredient)
        {
            context.Ingredients.Update(ingredient);
            context.SaveChanges();
        }
    }
}
