
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FUNTIK.Models;
using FUNTIK.GRASP;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Collections.Generic;
using FUNTIK.Models.Repositories;
using System.Reflection;


namespace FUNTIK.Pages
{

    public class KTSNewRecipeModel : PageModel
    {
        public string Message { get; set; }

        public Dictionary<IngredientType, string> MessageDict { get; set; }
        public List<MetaIngredient> Base { get; set; }
        public List<MetaIngredient> Nuts { get; set; }
        public List<MetaIngredient>  Impregnations { get; set; }
        public List<MetaIngredient> Infusions { get; set; }
        public List<MetaIngredient> CandedFruits { get; set; }
        public List<MetaIngredient> Custom { get; set; }
        public UserDa UserDa { get; set; }
        public Dictionary<IngredientType, Tuple<List<MetaIngredient>, List<Ingredient>>> TypeDict { get; set; }
        private readonly IMetaIngredientRepository metaIngredientRepository;
        private readonly IUserRepository userRepository;
        private readonly ISessionHelper sessionHelper;
        public IRecipeMaker RecipeMaker;


        public KTSNewRecipeModel(IEnumerable<IRecipeMaker> RecipeMakers, IMetaIngredientRepository metaIngredientRepository, IUserRepository userRepository, ISessionHelper sessionHelper)
        {
            Message = "";
            this.metaIngredientRepository = metaIngredientRepository;
            this.userRepository = userRepository;
            this.sessionHelper = sessionHelper;
            Base = metaIngredientRepository.FindBase();
            Nuts = metaIngredientRepository.FindNuts();
            Impregnations = metaIngredientRepository.FindImpregnations();
            Infusions = metaIngredientRepository.FindInfusions();
            CandedFruits = metaIngredientRepository.FindCandedFruits();
            Custom = metaIngredientRepository.FindCustom();
            RecipeMaker = RecipeMakers.ElementAt(1);
            RecipeMaker.GetBaseMetaIngredients(Base);
            //UserDa = userRepository.FindUserByEmail(User.Identity.Name);
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
        }

        public void RestoreSession()
        {
            var name = User.Identity.Name;
            var model = (KTSNewRecipeModel)sessionHelper.GetItem(name);
            if (model != null)
            {
                CopyFields(model, this);
            }
            
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

        public void OnPostBase(List<int> baseParam)
        {
            var baseDict = new Dictionary<string, int>()
            {
                ["mass"] = baseParam[0], ["cocoaP"] = baseParam[1],
                ["fatsP"] = baseParam[2], ["sugarP"] = baseParam[3],
                ["milkP"] = baseParam[4], ["cocoaDustP"] = baseParam[5]
            };
            RestoreSession();
            RecipeMaker.UpdateRecipeBase(baseDict);
            MessageDict[IngredientType.Base] = $"{RecipeMaker.Recipe.Mass} " +
                    $"{baseParam[4]} {baseParam[1]} {baseParam[3]} {baseParam[2]} {baseParam[5]}";
            //Message = "Вы выбрали: " + String.Join("\n", MessageDict.Select(x => x.Value).ToArray());
            RewriteMessage();
			var name = User.Identity.Name;
            sessionHelper.AddRenewItem(name, this);
        }

        public List<MetaIngredient> ParseCheckBox(List<string> selectedItems)
        {
            var type = (IngredientType)int.Parse(selectedItems[0].Split(", ")[1]);

            var metaIngredientList = TypeDict[type].Item1;
            return selectedItems.Select(x => x.Split(", ")[0]).Select(p => metaIngredientList.FirstOrDefault(x => x.Name == p)).ToList();
        }

        public void RewriteMessage()
        {
            var TypesDict = new Dictionary<IngredientType, string>()
            {
                [IngredientType.Base] = "Основа",
                [IngredientType.Nut] = "Орехи",
				[IngredientType.CandedFruit] = "Цукаты",
				[IngredientType.Infusion] = "Пропитки",
				[IngredientType.Impregnation] = "Начинки",
				[IngredientType.Custom] = "Другое",
			};

            var choco_base = MessageDict[IngredientType.Base].Split(' ');


            Message = $"Состав:<br/>" +
                    $"Масса вашей основы " +
                    $"{choco_base[0]} гр, из них:<br/>" +
                    $"{choco_base[4]} гр молока;<br/> " +
                    $"{choco_base[1]} гр какао;<br/>" +
                    $"{choco_base[3]} гр сахара;<br/>" +
                    $"{choco_base[2]} гр какао-масла.<br/>" +
                    $"{choco_base[5]} гр какао-масла.<br/>" +
                    $"<br/>";

            foreach (var type in MessageDict.Keys)
            {
                if (MessageDict[type].Length > 0 && type != IngredientType.Base)
					Message += $"{TypesDict[type]}:<br/>" + MessageDict[type] + "<br/><br/>";
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
            RewriteMessage();
            var name = User.Identity.Name;
            sessionHelper.AddRenewItem(name, this);
        }

        public void OnPostFinal()
        {
            RestoreSession();
            RecipeMaker.CompileRecipe();
            var LM = new LabelMaker(RecipeMaker.Recipe, new UserDa("tt"));
            var lablstr = LM.CreateLabelString();
            var label = LM.CreateLabel(lablstr);
            LM.SaveImage(label);
        }

        public void OnGet()
        {
            var name = User.Identity.Name;
            sessionHelper.ClearSession(name);
            if (RecipeMaker.Recipe.Mass == 0) Message = "Введите состав шоколада";
            else
                RewriteMessage();
            //else Message = $"Масса вашей основы {RecipeMaker.Recipe.Mass} гр, из них: \r\n " +
                    //$"{RecipeMaker.milkIng.WeightInGrams} гр молока; \r\n {RecipeMaker.cocoaIng.WeightInGrams} гр какао; \r\n {RecipeMaker.sugarIng.WeightInGrams} гр сахара; \r\n {RecipeMaker.addedFatsIng.WeightInGrams} гр какао-масла.";
        }
    }
}
