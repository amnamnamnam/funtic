using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FUNTIK.Models
{
    [Index("Email", IsUnique = true)]
    public class User
    {
        public int  Id { get; set; }
        public string Email { get; set; }
        public List<Recipe> Recipes { get; set; } = new();

        public User(string email)
        {
            Email = email;
        }
    }
}
