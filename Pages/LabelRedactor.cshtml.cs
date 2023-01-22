using FUNTIK.GRASP;
using FUNTIK.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNTIK.Pages
{
    public class LabelRedactorModel : PageModel
    {
        public int recipeId;

        private readonly IWebHostEnvironment _hostenvironment;
        private IRecipeRepository recipeRepository;
        public LabelRedactorModel(IWebHostEnvironment webHostEnvironment, IRecipeRepository recipeRepository)
        {
            _hostenvironment = webHostEnvironment;
            this.recipeRepository = recipeRepository;
        }

        public void OnGet()
        {
            recipeId = int.Parse(Request.Query["RecipeId"]);
        }

        public IActionResult OnPost()
        {
            recipeId = int.Parse(Request.Query["RecipeId"]);
            var r = recipeRepository.GetWithUserById(recipeId);
            var LM = new LabelMaker(r, r.User, recipeRepository);
                var lablstr = LM.CreateLabelString();
            var label = LM.CreateLabel(lablstr);
            LM.SaveImage(label);
            return Redirect(String.Format("~/EditFile?RecipeId={0}", recipeId));
        }
    }
}
