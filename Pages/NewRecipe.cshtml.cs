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
                new Ingredient { Name = "Грецкий орех" },
                new Ingredient { Name = "Фундук цельный" },
                new Ingredient { Name = "Фундук не очень цельный" }
            };
        }

        public void OnPostBase(int mass, int sugarpercent, int milkpercent, int fatpercent, int cocoapercent)
        {
            RecipeMaker.UpdateRecipeBase(mass, cocoapercent, fatpercent, sugarpercent, milkpercent);
            Message = $"Масса вашей основы {RecipeMaker.Recipe.Mass} гр, из них: \r\n " +
                    $"{RecipeMaker.milk.WeightInGrams} гр молока; \r\n {RecipeMaker.cocoa.WeightInGrams} гр какао; \r\n {RecipeMaker.sugar.WeightInGrams} гр сахара; \r\n {RecipeMaker.addedFats.WeightInGrams} гр какао-масла.";
            var LM = new LabelMaker(RecipeMaker.Recipe, new User("fuckyou"));
            var lablstr = LM.CreateLabelString();
            var label = LM.CreateLabel(lablstr);
            LM.SaveImage(label);


        }

        public void OnPostSubmit(string[] ingredient)
        {
            string message = "Selected Items:\\n";
            foreach (var i in ingredient)
            {
                message += i;
            }
            Message = message;
        }

        public void OnGet()
        {
            if (RecipeMaker.Recipe.Mass == 0) Message = "Введите состав шоколада";
            else Message = $"Масса вашей основы {RecipeMaker.Recipe.Mass} гр, из них: \r\n " +
                    $"{RecipeMaker.milk.WeightInGrams} гр молока; \r\n {RecipeMaker.cocoa.WeightInGrams} гр какао; \r\n {RecipeMaker.sugar.WeightInGrams} гр сахара; \r\n {RecipeMaker.addedFats.WeightInGrams} гр какао-масла.";

        }
    }
}
