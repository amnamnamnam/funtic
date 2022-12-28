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
            var r = new Recipe { Name = "Шоколад С кофе" };
        }

        public void OnGet()
        {
        }
    }
}
