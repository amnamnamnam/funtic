using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FUNTIK.Models.Repositories;
using FUNTIK.Models;

namespace FUNTIK.Pages;

[AllowAnonymous]
public class IndexModel : PageModel
{
    private readonly IUserRepository _userRepository;

    public string Name;

    public IndexModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void OnGet()
    {
        Name = User.Identity.Name;
        if (Name == null)
            return;
        if (_userRepository.FindUserByEmail(Name) == null)
            _userRepository.Create(new UserDa(Name));
    }
}
