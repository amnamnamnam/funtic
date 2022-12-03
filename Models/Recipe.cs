using System.ComponentModel.DataAnnotations;

namespace FUNTIK.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();
        public byte[]? Photo { get; set; }
    }
}
