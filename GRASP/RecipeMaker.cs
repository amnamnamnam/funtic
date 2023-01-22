using FUNTIK.Models;

namespace FUNTIK.GRASP
{
    public interface IRecipeMaker
    {
        public Recipe Recipe { get; set; }
        public void GetBaseMetaIngredients(List<MetaIngredient> baseMetaIngredients);
        public void UpdateRecipeBase(Dictionary<string, int> baseParams);
        public void AddIngredients(List<Ingredient> ingredients);
        public bool DeleteIngredient(Ingredient ingredient);
        public void CompileRecipe();
    }

    public class KrasnayaTsenaRecipeMaker : IRecipeMaker
    {
        public MetaIngredient sugar;
        public MetaIngredient addedFats;
        public MetaIngredient dryMilk;
        public MetaIngredient cocoaDust;
        public MetaIngredient cocoa;

        public Ingredient sugarIng;
        public Ingredient addedFatsIng;
        public Ingredient dryMilkIng;
        public Ingredient cocoaDustIng;
        public Ingredient cocoaIng;

        public Recipe Recipe { get; set; } = new Recipe();


        public void GetBaseMetaIngredients(List<MetaIngredient> baseMetaIngredients)
        {
            dryMilk = baseMetaIngredients.First(x => x.Name == "Молоко сухое");
            cocoa = baseMetaIngredients.First(x => x.Name == "Какао тёртое");
            addedFats = baseMetaIngredients.First(x => x.Name == "Добавочное масло");
            sugar = baseMetaIngredients.First(y => y.Name == "Сахар");
            cocoaDust = baseMetaIngredients.First(z => z.Name == "Какао-порошок");

            dryMilkIng = new Ingredient { MetaIngredient = dryMilk };
            cocoaIng = new Ingredient { MetaIngredient = cocoa };
            addedFatsIng = new Ingredient { MetaIngredient = addedFats };
            sugarIng = new Ingredient { MetaIngredient = sugar };
            cocoaDustIng = new Ingredient { MetaIngredient = cocoaDust };
        }


        public void UpdateRecipeBase(Dictionary<string, int> baseParams)
        {
            var mass = baseParams["mass"];
            var cocoaP = baseParams["cocoaP"];
            var fatsP = baseParams["fatsP"];
            var sugarP = baseParams["sugarP"];
            var milkP = baseParams["milkP"];
            var cocoaDustP = baseParams["cocoaDustP"];

            Recipe.Mass = mass;
            Recipe.CacaoPercent = cocoaP;
            var addedFatsPercent = fatsP - cocoaP * 53 / 100;
            cocoaDustIng.WeightInGrams = cocoaDustP * mass / 100;
            addedFatsIng.WeightInGrams = addedFatsPercent * mass / 100;
            cocoaIng.WeightInGrams = cocoaP * mass / 100;
            sugarIng.WeightInGrams = sugarP * mass / 100;
            dryMilkIng.WeightInGrams = milkP * mass / 100;
            AddIngredients(new List<Ingredient> { addedFatsIng, cocoaIng, sugarIng, dryMilkIng, cocoaDustIng });

        }

        public void AddIngredients(List<Ingredient> ingredients)
        {
            if (ingredients.Count == 0)
                return;
            var type = ingredients[0].MetaIngredient.Type;
            Recipe.Ingredients = Recipe.Ingredients.Where(x => x.MetaIngredient.Type != type).Concat(ingredients).ToList();
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Recipe.Ingredients.Add(ingredient);
        }

        public bool DeleteIngredient(Ingredient ingredient)
        {
            if (Recipe.Ingredients.Contains(ingredient))
            {
                Recipe.Ingredients.Remove(ingredient);
                return true;
            }
            return false;
        }



        public void CompileRecipe()
        {
            AddIngredients(new List<Ingredient> { addedFatsIng, cocoaIng, sugarIng, dryMilkIng, cocoaDustIng });
        }
    }


    public class RecipeMaker : IRecipeMaker
    {
        public MetaIngredient milk;
        public MetaIngredient cocoa;
        public MetaIngredient addedFats;
        public MetaIngredient sugar;

        public Ingredient milkIng;
        public Ingredient cocoaIng;
        public Ingredient addedFatsIng;
        public Ingredient sugarIng;

        public Recipe Recipe { get; set; } = new();

        public void GetBaseMetaIngredients(List<MetaIngredient> baseMetaIngredients)
        {
            milk = baseMetaIngredients.First(x => x.Name == "Молоко");
            cocoa = baseMetaIngredients.First(x => x.Name == "Какао тёртое/какао-бобы");
            addedFats = baseMetaIngredients.First(x => x.Name == "Какао-масло");
            sugar = baseMetaIngredients.First(y => y.Name == "Сахар");
            
            milkIng = new Ingredient { MetaIngredient = milk };
            cocoaIng = new Ingredient { MetaIngredient = cocoa };
            addedFatsIng = new Ingredient { MetaIngredient = addedFats };
            sugarIng = new Ingredient { MetaIngredient = sugar };

        }


        public void UpdateRecipeBase(Dictionary<string, int> baseParams)
        {
            var mass = baseParams["mass"];
            var cocoaP = baseParams["cocoaP"];
            var fatsP = baseParams["fatsP"];
            var sugarP = baseParams["sugarP"];
            var milkP = baseParams["milkP"];
            Recipe.Mass = mass;
            Recipe.CacaoPercent = cocoaP;
            addedFatsIng.WeightInGrams = fatsP * mass / 100;
            cocoaIng.WeightInGrams = cocoaP * mass / 100;
            sugarIng.WeightInGrams = sugarP * mass / 100;
            milkIng.WeightInGrams = milkP * mass / 100;
            AddIngredients(new List<Ingredient> { addedFatsIng, cocoaIng, sugarIng, milkIng });
        }

        public void AddIngredients(List<Ingredient> ingredients)
        {
            if (ingredients.Count == 0)
                return;
            var type = ingredients[0].MetaIngredient.Type;
            Recipe.Ingredients = Recipe.Ingredients.Where(x => x.MetaIngredient.Type != type).Concat(ingredients).ToList();
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Recipe.Ingredients.Add(ingredient);
        }

        public bool DeleteIngredient(Ingredient ingredient)
        {
            if (Recipe.Ingredients.Contains(ingredient))
            {
                Recipe.Ingredients.Remove(ingredient);
                return true;
            }
            return false;
        }

        public void CompileRecipe()
        {
            return;
            //AddIngredients(new List<Ingredient> { addedFatsIng, cocoaIng, sugarIng, milkIng });
        }
    }
}