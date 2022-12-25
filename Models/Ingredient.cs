using System.ComponentModel.DataAnnotations;

namespace FUNTIK.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string? Name { get => MetaIngredient.Name; }
        public MetaIngredient MetaIngredient { get; set; }
        public int MetaIngredientId { get; set; }
        public int WeightInGrams { get; set; }
        public Recipe Recipe { get; set; }
        public int RecipeId { get; set; }
    }       
}
