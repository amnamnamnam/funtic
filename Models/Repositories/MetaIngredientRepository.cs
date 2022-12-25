using FUNTIK.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;

namespace FUNTIK.Models.Repositories
{
    public interface IMetaIngredientRepository
    {
        public void Create(MetaIngredient ingredient);
        public void Update(MetaIngredient ingredient);
        public void Delete(MetaIngredient ingredient);
        public List<MetaIngredient> FindBase();
        public List<MetaIngredient> FindNuts();
        public List<MetaIngredient> FindImpregnations();
        public List<MetaIngredient> FindInfusions();
        public List<MetaIngredient> FindCandedFruits();
        public List<MetaIngredient> FindCustom();
    }

    public class MetaIngredientRepository : IMetaIngredientRepository
    {
        private readonly ApplicationDbContext context;
        public MetaIngredientRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(MetaIngredient ingredient)
        {
            context.MetaIngredients.Add(ingredient);
            context.SaveChanges();
        }

        public void Delete(MetaIngredient ingredient)
        {
            context.MetaIngredients.Remove(ingredient);
            context.SaveChanges();
        }

        public List<MetaIngredient> FindBase()
        {
            return context.MetaIngredients.Where(i => i.Type == IngredientType.Base).ToList();
        }

        public List<MetaIngredient> FindNuts()
        {
            return context.MetaIngredients.Where(i => i.Type == IngredientType.Nut && i.UserId == null).ToList();
        }

        public List<MetaIngredient> FindImpregnations()
        {
            return context.MetaIngredients.Where(i => i.Type == IngredientType.Impregnation && i.UserId == null).ToList();
        }

        public List<MetaIngredient> FindInfusions()
        {
            return context.MetaIngredients.Where(i => i.Type == IngredientType.Infusion && i.UserId == null).ToList();
        }

        public List<MetaIngredient> FindCandedFruits()
        {
            return context.MetaIngredients.Where(i => i.Type == IngredientType.CandedFruit && i.UserId == null).ToList();
        }

        public List<MetaIngredient> FindCustom()
        {
            return context.MetaIngredients.Where(i => i.Type == IngredientType.Custom && i.UserId == null).ToList();
        }

        public void Update(MetaIngredient ingredient)
        {
            context.MetaIngredients.Update(ingredient);
            context.SaveChanges();
        }
    }
}
