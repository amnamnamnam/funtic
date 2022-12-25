using FUNTIK.Models;

namespace FUNTIK.GRASP
{
    public class RecipeMaker
    {   
        public static MetaIngredient milk = new MetaIngredient { Name = "Молоко" };
        public static MetaIngredient cocoa = new MetaIngredient { Name = "Какао" };
        public static MetaIngredient addedFats = new MetaIngredient { Name = "Добавочное масло" };
        public static MetaIngredient sugar = new MetaIngredient { Name = "Сахар" };

        public Ingredient milkIng = new Ingredient { MetaIngredient = milk };
        public Ingredient cocoaIng = new Ingredient { MetaIngredient = cocoa };
        public Ingredient addedFatsIng = new Ingredient { MetaIngredient = addedFats };
        public Ingredient sugarIng = new Ingredient { MetaIngredient = sugar };

        public Recipe Recipe = new();

        public void UpdateRecipeBase(int mass, int cacaoP, int fatsP, int sugarP, int milkP)
        {
            Recipe.Mass = mass;
            Recipe.CacaoPercent = cacaoP;
            Recipe.FatPercent = fatsP;
            Recipe.SugarPercent = sugarP;
            Recipe.MilkPersent = milkP;
            var addedFatsPercent = fatsP - cacaoP * 53 / 100;
            
            addedFatsIng.WeightInGrams = addedFatsPercent * mass / 100;
            cocoaIng.WeightInGrams = cacaoP * mass / 100;
            sugarIng.WeightInGrams = sugarP * mass / 100;
            milkIng.WeightInGrams = milkP * mass / 100;
            AddIngredients(new List<Ingredient> { addedFatsIng, cocoaIng, sugarIng, milkIng });

        }
        //TODO: 
        
        public void AddIngredients(List<Ingredient> ingredients)
        {
            Recipe.Ingredients = Recipe.Ingredients.Concat(ingredients).ToList();
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
    }

 
}
