using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNTIK.Pages
{
    public class MainPageModel : PageModel
    {
        private readonly IServiceProvider _ServiceProvider;

        public MainPageModel(IServiceProvider ServiceProvider)
        {
            _ServiceProvider = ServiceProvider;
        }

        //var recipeRepository = new RecipeRepository(serviceProvider);
        public void OnGet()
        {
        }
    }
}
