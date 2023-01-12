using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FUNTIK.Models.Repositories;
using FUNTIK.Models;
using Microsoft.AspNetCore.Mvc;

namespace FUNTIK.Pages;

public class EditFileModel : PageModel
{
    public int recipeId;
    
    private readonly IWebHostEnvironment _hostenvironment;
    private IRecipeRepository recipeRepository;
    public EditFileModel(IWebHostEnvironment webHostEnvironment, IRecipeRepository recipeRepository)
    {
        _hostenvironment = webHostEnvironment;
        this.recipeRepository = recipeRepository;
    }

    [BindProperty]
    public FileViewModel FileUpload { get; set; }
    public void OnGet()
    {
        FileUpload = new FileViewModel();
        FileUpload.FolderFileName = "success1.png";
        FileUpload.FolderFilePath = "succsess1.png";
        recipeId = int.Parse(Request.Query["RecipeId"]);
        //v ar a = OnGetDownloadFileFromFolder();
    }


    public FileResult OnGetDownloadFileFromFolder(int recipeId)
    {
        byte[] bytes = recipeRepository.Find(x => x.Id == recipeId).Photo;
        return File(bytes, "application/octet-stream", "ShockoRecipe.png");
    }
}

