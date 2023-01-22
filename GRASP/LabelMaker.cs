using FUNTIK.Models;
using System;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats.Png;
using FUNTIK.Models.Repositories;

namespace FUNTIK.GRASP
{
    public class LabelMaker
    {
        private Recipe recipe; 
        private UserDa user;
        private IRecipeRepository recipeRepository;

        public LabelMaker(Recipe recipe, UserDa user, IRecipeRepository recipeReposirory)
        {
            this.recipe = recipe;
            this.user = user;
            this.recipeRepository = recipeReposirory;

        }

        public string CreateLabelString()
        {
            Random rnd = new Random();
            return $"{ recipe.Name } \n\rСодержание какао-продуктов не менее { recipe.CacaoPercent} %. \n\rСостав: { String.Join(", ", recipe.Ingredients.Where(x => x.MetaIngredient.Type == IngredientType.Base).Select(x => x.Name).ToArray()) } \n\r{ String.Join(", ", recipe.Ingredients.Where(x => x.MetaIngredient.Type != IngredientType.Base).Select(x => x.Name).ToArray()) }\n\rКалорийность на 100 гр продукта: {598 + rnd.Next(-50, 50)} кКал. Срок хранения { recipe.ShelfLife }. Хранить в прохладном, сухом месте, вдали от чрезмерной жары или влажности. Идеальная температура для хранения шоколада составляет 18-20 градусов. Контакты: { user.Contacts }. Масса нетто: { recipe.Mass } гр.";
        }

        public string AddCookingDate(string labelString, string cookingDate)
        {
            return $"{labelString} Дата изготовления: {cookingDate}";
        }


        public Image CreateLabel(string labelString)
        {
            var img = new Image<Rgba32>(Configuration.Default, 1000, 500, new Rgba32(255, 255, 255));

            Font font = SystemFonts.CreateFont("Arial", 10);

            return img.Clone(ctx => ctx.ApplyScalingWaterMark(font, labelString, Color.Black, 5, true));
            
        }

        public void SaveImage(Image image)
        {
            /*var path = "/Labels";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            image.Save($"{path}/succsess1.png");*/
            using (var ms = new MemoryStream())
            {
                image.Save(ms, new PngEncoder());
                recipe.Photo = ms.ToArray();
                recipeRepository.Update(recipe);
            }
        }


    }

 
}
