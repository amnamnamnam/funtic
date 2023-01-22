using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FUNTIK.Models;
using FUNTIK.Models.Repositories;
using FUNTIK.GRASP;

namespace FUNTIK.Pages
{
    public class RecipeModel : PageModel
    {
        public Recipe Recipe { get; set; } 
        public int RecipeId;
        private IRecipeRepository recipeRepository;
        public string Compound;

        public RecipeModel(IRecipeRepository recipeRepository)
        {
            this.recipeRepository = recipeRepository;
        }

        public void OnGet()
        {
            RecipeId = int.Parse(Request.Query["RecipeId"]);
            if (RecipeId == null)
                Response.Redirect("/Identity/Account/AccessDenied0");
            Recipe = recipeRepository.GetWithUserById(RecipeId);
            if (!(Recipe != null && Recipe.User.Email == User.Identity.Name))
                Response.Redirect("/Identity/Account/AccessDenied0");
            MakeCompound();
        }

        public IActionResult OnPostDelete(string[] recipe)
        {
            var r = recipeRepository.Find(x => x.Id == int.Parse(recipe[0]));
            recipeRepository.Delete(r);
            return Redirect("~/MainPage");
        }

        public IActionResult OnPostEditRecipe(string[] recipe)
        {
            return Redirect(String.Format("~/NewRecipe?RecipeId={0}", int.Parse(recipe[0])));
        }

        public IActionResult OnPostEditLabel(string[] recipe)
        {
            return Redirect(String.Format("~/LabelRedactor?RecipeId={0}", int.Parse(recipe[0])));
        }

        public FileResult OnGetDownloadFile(int recipeId)
        {
            byte[] bytes = recipeRepository.Find(x => x.Id == recipeId).Photo;
            return File(bytes, "application/octet-stream", "ShockoRecipe.png");
        }

        public void MakeCompound()  
        {
            var headers = new List<string> { "Основа:<br/>", "Орехи:<br/>", "Начинки:<br/>", "Пропитки:<br/>", "Цукаты:<br/>", "Другое:<br/>" };
            var message = "";
			for (int i = 0; i <= 5; i++)
            {
                var plusMessage = String.Join("", this.Recipe.Ingredients.Where(ingredient => (int)ingredient.MetaIngredient.Type == i).Select(ing => MakeOneIngredientString(ing)).ToList());
                message += headers[i] + plusMessage + "<br/>";
            }
            this.Compound = message;
        }

        public string MakeOneIngredientString(Ingredient ing)
        {
            var result = "";
            if ((int)ing.MetaIngredient.Type == 0)
                result += ing.Name + ": " + ing.WeightInGrams.ToString() + "гр.<br/>";
            else
				result += ing.Name + "<br/>";
			return result;
        }
    }
}
