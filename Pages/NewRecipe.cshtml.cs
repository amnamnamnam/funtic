using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FUNTIK.Models;
using FUNTIK.GRASP;

namespace FUNTIK.Pages
{
    public class NewRecipeModel : PageModel
    {
        public string Message { get; private set; } = "";
        public RecipeMaker RecipeMaker = new RecipeMaker();
        
        public void OnPost(int mass, int sugarpercent, int milkpercent, int fatpercent, int cocoapercent)
        {
            //RecipeMaker = new RecipeMaker();
            RecipeMaker.UpdateRecipeBase(mass, cocoapercent, fatpercent, sugarpercent, milkpercent);
            Message = $"Recipe {RecipeMaker.Recipe.Mass} {RecipeMaker.Recipe.FatPercent} {RecipeMaker.Recipe.CacaoPercent}";
        }

        public void OnGet()
        {
            Message = $"Recipe {RecipeMaker.Recipe.Mass} {RecipeMaker.Recipe.FatPercent} {RecipeMaker.Recipe.CacaoPercent}";
        }
    }
}
