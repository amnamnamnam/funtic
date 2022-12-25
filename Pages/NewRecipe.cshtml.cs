using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FUNTIK.Models;
using FUNTIK.GRASP;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace FUNTIK.Pages
{
    public class NewRecipeModel : PageModel
    {
        public string Message { get; private set; } = "";
        public List<Ingredient> Ingredients { get; set; }

        public RecipeMaker RecipeMaker = new RecipeMaker();

        public NewRecipeModel()
        {
            Ingredients = new List<Ingredient>
            {
                new Ingredient { MetaIngredient = new MetaIngredient { Name = "Грецкий орех" } },
                new Ingredient { MetaIngredient = new MetaIngredient { Name = "Фундук цельный" }  },
                new Ingredient { MetaIngredient = new MetaIngredient { Name = "Фундук не очень цельный" } }
            };
        }

        public void OnPostBase(int mass, int sugarpercent, int milkpercent, int fatpercent, int cocoapercent)
        {
            RecipeMaker.UpdateRecipeBase(mass, cocoapercent, fatpercent, sugarpercent, milkpercent);
            Message = $"Масса вашей основы {RecipeMaker.Recipe.Mass} гр, из них: \r\n " +
                    $"{RecipeMaker.milkIng.WeightInGrams} гр молока; \r\n {RecipeMaker.cocoaIng.WeightInGrams} гр какао; \r\n {RecipeMaker.sugarIng.WeightInGrams} гр сахара; \r\n {RecipeMaker.addedFatsIng.WeightInGrams} гр какао-масла.";
            var LM = new LabelMaker(RecipeMaker.Recipe, new User("fuckyou"));
            var lablstr = LM.CreateLabelString();
            var label = LM.CreateLabel(lablstr);
            LM.SaveImage(label);
        }

        public void OnPostNuts(string[] ingredient)
        {
            string message = "Selected Items:\\n";
            foreach (var i in ingredient)
            {
                message += i;
            }
            Message = message;
        }

        public void OnPostFinal()
        {

        }

        public void OnGet()
        {
            if (RecipeMaker.Recipe.Mass == 0) Message = "Введите состав шоколада";
            else Message = $"Масса вашей основы {RecipeMaker.Recipe.Mass} гр, из них: \r\n " +
                    $"{RecipeMaker.milkIng.WeightInGrams} гр молока; \r\n {RecipeMaker.cocoaIng.WeightInGrams} гр какао; \r\n {RecipeMaker.sugarIng.WeightInGrams} гр сахара; \r\n {RecipeMaker.addedFatsIng.WeightInGrams} гр какао-масла.";

        }
    }
}
