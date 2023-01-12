using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FUNTIK.Models
{
    [Index("Email", IsUnique = true)]
    public class UserDa
    {
        public int  Id { get; set; }
        public string Email { get; set; }
 
        public List<Recipe> Recipes { get; set; } = new();

        public UserDa(string email)
        {
            Email = email;
        }

        public List<MetaIngredient> CustomIngredients { get; set; } = new();

        public string? Contacts { get; set; }
   }
}
