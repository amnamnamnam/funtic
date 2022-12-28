public class AppFile
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public byte[] Content { get; set; }
}
public class FileViewModel
{
   
    public string FolderFileName { get; set; } 
    public string FolderFilePath { get; set; }    
    public IFormFile FormFile { get; set; }      
}
