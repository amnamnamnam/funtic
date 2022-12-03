using System.ComponentModel.DataAnnotations;

namespace FUNTIK.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IngredientType Type { get; set; }
        public int WeightInGrams { get; set; }
        public Recipe? Recipe { get; set; }
        public int? RecipeId { get; set; }
    }

    public enum IngredientType
    {
        Base,
        Nut,
        Impregnation,
        Custom
    }
}
