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
                Message = $"�������� ���������� '{ingredient.Name}'";
            }
        else
            Message = $"���������� '{ingredient.Name}' ��� ����������";
        }

        public void OnPostDelete(string[] ingredient)
        {
            Message = "����� ������" + ingredient.Length;
            foreach (var i in ingredient)
            {
                Message += $"����� '{i}'";
                metaIngredientRepository.Delete(Ingredients.FirstOrDefault(x => x.Name == i));
            }
            Ingredients = metaIngredientRepository.GetAll();
        }
    }
}
