using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FUNTIK.Models;
using FUNTIK.GRASP;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Collections.Generic;
using FUNTIK.Models.Repositories;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace FUNTIK.Pages
{
    [AllowAnonymous]

    public class NewRecipeModel : PageModel
    {
        public string Message { get; set; }
        public int oldRecipeId { get; set; } = -1;
        public Dictionary<IngredientType, string> MessageDict { get; set; }
        public List<MetaIngredient> Base { get; set; }
        public List<MetaIngredient> Nuts { get; set; }
        public List<MetaIngredient> Impregnations { get; set; }
        public List<MetaIngredient> Infusions { get; set; }
        public List<MetaIngredient> CandedFruits { get; set; }
        public List<MetaIngredient> Custom { get; set; }
        public UserDa UserDa { get; set; }
        public Dictionary<IngredientType, Tuple<List<MetaIngredient>, List<Ingredient>>> TypeDict { get; set; }
        private IMetaIngredientRepository metaIngredientRepository;
        private IUserRepository userRepository;
        private readonly ISessionHelper sessionHelper;
        public IRecipeMaker RecipeMaker;
        public IRecipeRepository RecipeRepository;

        public NewRecipeModel(IEnumerable<IRecipeMaker> RecipeMakers, IMetaIngredientRepository metaIngredientRepository, IUserRepository userRepository, ISessionHelper sessionHelper, IRecipeRepository recipeRepository)
        {
            Message = "";
            this.metaIngredientRepository = metaIngredientRepository;
            this.userRepository = userRepository;
            this.sessionHelper = sessionHelper;
            this.RecipeRepository = recipeRepository;
            RecipeMaker = RecipeMakers.First();
        }

        public void RestoreSession()
        {
            var name = User.Identity.Name;
            var tmpRR = RecipeRepository;
            var tmpMR = metaIngredientRepository;
            var tmpUR = userRepository;
            var model = (NewRecipeModel)sessionHelper.GetItem(name);
            if (model != null)
            {
                CopyFields(model, this);
            }
            RecipeRepository = tmpRR;
            metaIngredientRepository = tmpMR;
            userRepository = tmpUR;
        }

        public static void CopyFields<T>(T source, T destination)
        {
            var fields = source.GetType().GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            foreach (var field in fields)
            {
                field.SetValue(destination, field.GetValue(source));
            }

            var props = source.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            foreach (var p in props)
            {
                p.SetValue(destination, p.GetValue(source));
            }

        }

        public void OnPostBase(string baseParam0, List<int> baseParam)
        {
            /*PictureSender sender = new PictureSender();
            sender.Share("hhh", "work");*/

            var baseDict = new Dictionary<string, int>()
            {
                ["mass"] = baseParam[0],
                ["cocoaP"] = baseParam[1],
                ["fatsP"] = baseParam[2] + baseParam[3],
                ["sugarP"] = baseParam[4],
                ["milkP"] = baseParam[5]
            };

            RestoreSession();
            RecipeMaker.UpdateRecipeBase(baseDict);
            MessageDict[IngredientType.Base] = $"{RecipeMaker.Recipe.Mass} " +
                    $"{baseParam[5]} {baseParam[1]} {baseParam[4]} {baseParam[2]+baseParam[3]}";
            //Message = "Вы выбрали: " + String.Join("\n", MessageDict.Select(x => x.Value).ToArray());
            //RewriteMessage();
            MakeCompound();
            var name = User.Identity.Name;
            RecipeMaker.Recipe.Name = baseParam0;
            sessionHelper.AddRenewItem(name, this);
        }

        public void MakeCompound()
        {
            var headers = new List<string> { "Основа:<br/>", "Орехи:<br/>", "Начинки:<br/>", "Пропитки:<br/>", "Цукаты:<br/>", "Другое:<br/>" };
            var message = "";
            for (int i = 0; i <= 5; i++)
            {
                var plusMessage = String.Join("", RecipeMaker.Recipe.Ingredients.Where(ingredient => (int)ingredient.MetaIngredient.Type == i).Select(ing => MakeOneIngredientString(ing)).ToList());
                if (plusMessage != "")
                    message += headers[i] + plusMessage + "<br/>";
            }
            this.Message = message;
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

        public List<MetaIngredient> ParseCheckBox(List<string> selectedItems)
        {
            var type = (IngredientType)int.Parse(selectedItems[0].Split(", ")[1]);

            var metaIngredientList = TypeDict[type].Item1;
            return selectedItems.Select(x => x.Split(", ")[0]).Select(p => metaIngredientList.FirstOrDefault(x => x.Name == p)).ToList();
        }

        

        public void RenewRecipe()
        {
            var MetaIngredients = metaIngredientRepository.GetAll();
            for (var i = 0; i < RecipeMaker.Recipe.Ingredients.Count; i++)
            {
                RecipeMaker.Recipe.Ingredients[i].MetaIngredient = MetaIngredients.Find(x => x.Id == RecipeMaker.Recipe.Ingredients[i].MetaIngredient.Id);
            }
        }

        public void OnPostIngredients(List<string> ingredient)
        {
            if (ingredient.Count == 0)
                return;
            RestoreSession();
            var selectedMetaIngredients = ParseCheckBox(ingredient);
            var chosenIngredients = selectedMetaIngredients.Select(n => new Ingredient { MetaIngredient = n }).ToList();
            RecipeMaker.AddIngredients(chosenIngredients);
            var type = selectedMetaIngredients[0].Type;
            var metaIngredientList = TypeDict[type].Item1;
            TypeDict[type] = Tuple.Create(metaIngredientList, chosenIngredients);
            MessageDict[type] = String.Join(", ", chosenIngredients.Select(x => x.Name).ToArray());
            //Message = String.Join(";", MessageDict.Select(x => x.Value).ToArray());
            //RewriteMessage();
            MakeCompound();
            var name = User.Identity.Name;
            sessionHelper.AddRenewItem(name, this);
        }

        public IActionResult OnPostFinal()
        {
            RestoreSession();
            var name = User.Identity.Name;
            UserDa = userRepository.FindUserByEmail(name);
            RecipeMaker.Recipe.User = UserDa;
            RecipeMaker.Recipe.ShelfLife = "";
            RecipeMaker.CompileRecipe();
            UserDa.Contacts = "";
            RenewRecipe();
            if (oldRecipeId == -1)
                RecipeRepository.Create(RecipeMaker.Recipe);
            else
                RecipeRepository.Update(RecipeMaker.Recipe);
            return Redirect(String.Format("~/LabelRedactor?RecipeId={0}", RecipeMaker.Recipe.Id));
        }

        public void OnGet()
        {
            var name = User.Identity.Name;

            if (name == null)
            {
                Base = new List<MetaIngredient> { };
                Nuts = new List<MetaIngredient> { };
                Impregnations = new List<MetaIngredient> { };
                Infusions = new List<MetaIngredient> { };
                CandedFruits = new List<MetaIngredient> { };
                Custom = new List<MetaIngredient> { };
                Response.Redirect("/MainPage");
            }
            else
            {
                sessionHelper.ClearSession(name);
                Base = metaIngredientRepository.FindBase();
                Nuts = metaIngredientRepository.FindNuts();
                Impregnations = metaIngredientRepository.FindImpregnations();
                Infusions = metaIngredientRepository.FindInfusions();
                CandedFruits = metaIngredientRepository.FindCandedFruits();
                Custom = metaIngredientRepository.FindCustom();

                RecipeMaker.GetBaseMetaIngredients(Base);
                MessageDict = new()
                {
                    [IngredientType.Base] = "",
                    [IngredientType.Nut] = "",
                    [IngredientType.CandedFruit] = "",
                    [IngredientType.Infusion] = "",
                    [IngredientType.Impregnation] = "",
                    [IngredientType.Custom] = ""
                };

                TypeDict = new Dictionary<IngredientType, Tuple<List<MetaIngredient>, List<Ingredient>>>()
                {
                    [IngredientType.Nut] = Tuple.Create(Nuts, new List<Ingredient>()),
                    [IngredientType.Impregnation] = Tuple.Create(Impregnations, new List<Ingredient>()),
                    [IngredientType.Infusion] = Tuple.Create(Infusions, new List<Ingredient>()),
                    [IngredientType.Custom] = Tuple.Create(Custom, new List<Ingredient>()),
                    [IngredientType.CandedFruit] = Tuple.Create(CandedFruits, new List<Ingredient>())
                };

                try
                {
                    oldRecipeId = int.Parse(Request.Query["RecipeId"]);
                    RecipeMaker.Recipe = RecipeRepository.GetWithUserById(oldRecipeId);
                }
                catch(Exception ex)
                {
      
                }
               

                if (RecipeMaker.Recipe.Mass == 0) Message = "Введите состав шоколада";
                else
                    MakeCompound();
                sessionHelper.AddRenewItem(name, this);
            }
        }
    }
}