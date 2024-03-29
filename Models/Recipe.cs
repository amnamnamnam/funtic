using System.ComponentModel.DataAnnotations;

namespace FUNTIK.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public UserDa User { get; set; }
        public int UserId { get; set; }

        public int Mass { get; set; }
        public int CacaoPercent { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new();
        public byte[]? Photo { get; set; }

        public string ShelfLife  { get; set; }
        
    }
}
