using FUNTIK.Data;
using FUNTIK.Models;
using FUNTIK.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNTIK.Pages
{
    public class MainPageModel : PageModel
    {
        private IUserRepository userRepository;
        private IRecipeRepository recipeRepository;
        public string Message { get; private set; } = "";
        public List<Recipe> Recipes;

        public MainPageModel(IUserRepository userRepository, IRecipeRepository recipeRepository)
        {
            this.userRepository = userRepository;
            this.recipeRepository = recipeRepository;
            //Recipes = recipeRepository.FindAll(x => true);
        }


        public void OnGet()
        {
            var name = User.Identity.Name;
            this.Recipes = userRepository.FindUserFullInfoByEmail(name).Recipes.ToList();
            var a = 9;
        }

        public IActionResult OnPost(string[] recipe)
        {
            var name = User.Identity.Name;
            Recipes = userRepository.FindUserFullInfoByEmail(name).Recipes.ToList();

            recipeRepository.Delete(Recipes.FirstOrDefault(x => x.Id == int.Parse(recipe[0])));
            var a = 8;
            Recipes = userRepository.FindUserFullInfoByEmail(name).Recipes.ToList();
            return Redirect("~/MainPage");
        }
    }
}       
