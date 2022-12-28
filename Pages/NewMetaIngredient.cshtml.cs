using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using FUNTIK.Models.Repositories;
using FUNTIK.Models;

namespace FUNTIK.Pages
{
	[AllowAnonymous]
	public class NewMetaIngredientModel : PageModel
    {
        private readonly IMetaIngredientRepository metaIngredientRepository;
        public string Message { get; private set; } = "";
        public List<MetaIngredient> Ingredients;

        public NewMetaIngredientModel(IMetaIngredientRepository metaIngredientRepository)
        {
            this.metaIngredientRepository = metaIngredientRepository;
            Ingredients = metaIngredientRepository.GetAll();
        }

        public void OnGet()
        {
        }

        public void OnPostCreate(MetaIngredient ingredient)
        {
            if (metaIngredientRepository.FindByName(ingredient.Name) == null)
            {
                metaIngredientRepository.Create(ingredient);
                Message = $"Добавлен ингредиент '{ingredient.Name}'";
            }
        else
            Message = $"Ингредиент '{ingredient.Name}' уже сущестпует";
        }

        public void OnPostDelete(string[] ingredient)
        {
            Message = "длина списка" + ingredient.Length;
            foreach (var i in ingredient)
            {
                Message += $"Удалён '{i}'";
                metaIngredientRepository.Delete(Ingredients.FirstOrDefault(x => x.Name == i));
            }
            Ingredients = metaIngredientRepository.GetAll();
        }
    }
}
