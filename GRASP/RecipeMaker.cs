using FUNTIK.Models;

namespace FUNTIK.GRASP
{
    public class RecipeMaker
    {
        public Ingredient milk = new Ingredient { Name = "Молоко" };
        public Ingredient cocoa = new Ingredient { Name = "Какао" };
        public Ingredient addedFats = new Ingredient { Name = "Добавочное масло" };
        public Ingredient sugar = new Ingredient { Name = "Сахар" };

        public Recipe Recipe = new();

        public void UpdateRecipeBase(int mass, int cacaoP, int fatsP, int sugarP, int milkP)
        {
            Recipe.Mass = mass;
            Recipe.CacaoPercent = cacaoP;
            Recipe.FatPercent = fatsP;
            Recipe.SugarPercent = sugarP;
            Recipe.MilkPersent = milkP;
            var addedFatsPercent = fatsP - cacaoP * 53 / 100;
            addedFats.WeightInGrams = addedFatsPercent * mass / 100;
            cocoa.WeightInGrams = cacaoP * mass / 100;
            sugar.WeightInGrams = sugarP * mass / 100;
            milk.WeightInGrams = milkP * mass / 100;
            AddIngredients(new List<Ingredient> { milk, cocoa, addedFats, sugar });

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
