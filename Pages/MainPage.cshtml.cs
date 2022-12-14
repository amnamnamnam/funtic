using FUNTIK.Data;
using FUNTIK.Models;
using FUNTIK.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNTIK.Pages
{
    public class MainPageModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public MainPageModel(ApplicationDbContext context)
        {
            this.context = context;
            var recipeRepository = new RecipeRepository(context);
            var r = new Recipe { Name = "������� � ����" };
            var bace = new Ingredient { Name = "�����-�� ������", Type = IngredientType.Base };
            var coffee = new Ingredient { Name = "����", Type = IngredientType.Custom };
            var sugar = new Ingredient { Name = "�����" };
            r.Ingredients.Add(bace);
            r.Ingredients.Add(coffee);
            r.Ingredients.Add(sugar);
            recipeRepository.Create(r);
        }

        public void OnGet()
        {
        }
    }
}
