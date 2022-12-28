using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FUNTIK.Models.Repositories;
using FUNTIK.Models;
using Microsoft.AspNetCore.Mvc;

namespace FUNTIK.Pages;

public class EditFileModel : PageModel
{
    
    private readonly IWebHostEnvironment _hostenvironment;
    public EditFileModel(IWebHostEnvironment webHostEnvironment)
    {
        _hostenvironment = webHostEnvironment;
    }

    [BindProperty]
    public FileViewModel FileUpload { get; set; }
    public void OnGet()
    {

        FileUpload = new FileViewModel();
        FileUpload.FolderFileName = "success1.png";
        FileUpload.FolderFilePath = "succsess1.png";
    }
    public FileResult OnGetDownloadFileFromFolder(string fileName)
    {
        //Build the File Path.
        string path = "Labels/" + fileName;
        //Read the File data into Byte Array.
        byte[] bytes = System.IO.File.ReadAllBytes(@"D:\Funticcc\Labels\succsess1.png");
        //D:\Funticcc\Labels\succsess1.png
        //Send the File to Download.
        return File(bytes, "application/octet-stream", fileName);
    }
}

