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
            RecipeMaker.UpdateRecipeBase(mass, cocoapercent, fatpercent, sugarpercent, milkpercent);
            Message = $"Масса вашей основы {RecipeMaker.Recipe.Mass} гр, из них: \r\n " +
                    $"{RecipeMaker.milk.WeightInGrams} гр молока; \r\n {RecipeMaker.cocoa.WeightInGrams} гр какао; \r\n {RecipeMaker.sugar.WeightInGrams} гр сахара; \r\n {RecipeMaker.addedFats.WeightInGrams} гр какао-масла.";
        }

        public void OnGet()
        {
            if (RecipeMaker.Recipe.Mass == 0) Message = "Введите состав шоколада";
            else Message = $"Масса вашей основы {RecipeMaker.Recipe.Mass} гр, из них: \r\n " +
                    $"{RecipeMaker.milk.WeightInGrams} гр молока; \r\n {RecipeMaker.cocoa.WeightInGrams} гр какао; \r\n {RecipeMaker.sugar.WeightInGrams} гр сахара; \r\n {RecipeMaker.addedFats.WeightInGrams} гр какао-масла.";

        }
    }
}
