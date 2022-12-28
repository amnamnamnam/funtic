using FUNTIK.Models;
using System;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace FUNTIK.GRASP
{
    public class LabelMaker
    {
        private Recipe recipe; 
        private UserDa user;

        public LabelMaker(Recipe recipe, UserDa user)
        {
            this.recipe = recipe;
            this.user = user;
        }

        public string CreateLabelString()
        {
            return $"Содержание какао-продуктов не менее { recipe.CacaoPercent} % \n Состав: { String.Join(", ", recipe.Ingredients.Select(x => x.MetaIngredient.Name).ToArray()) } \n Пищевая ценность в 100 гр продукта: 6666 \n Энергеническая ценность в 100 гр продукта: 1488 \n Срок хранения { recipe.ShelfLife } \n Хранить при температуре +12/+18 С и относительной влажности воздуха не более 75%, не подвергать воздействию прямых солнечных лучей. Контакты: { user.Contacts }. \n Масса нетто: { recipe.Mass }";
        }

        public string AddCookingDate(string labelString, string cookingDate)
        {
            return $"{labelString} Дата изготовления: {cookingDate}";
        }


        public Image CreateLabel(string labelString)
        {
            var img = new Image<Rgba32>(Configuration.Default, 1000, 1000, new Rgba32(0, 0, 255));

            Font font = SystemFonts.CreateFont("Arial", 10);

            return img.Clone(ctx => ctx.ApplyScalingWaterMark(font, labelString, Color.Black, 5, true));
            
        }

        public void SaveImage(Image image)
        {
            image.Save("Labels/succsess1.png");
        }


    }

 
}
