using System.ComponentModel.DataAnnotations;

namespace FUNTIK.Models
{
    public class MetaIngredient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IngredientType Type { get; set; }
        public User? User { get; set; }
        public int? UserId { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();

    }       

    public enum IngredientType
    {
        Base,
        Nut,
        Impregnation,
        Infusion,
        CandedFruit,
        Custom
    }
}
