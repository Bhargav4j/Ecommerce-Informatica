using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Web.Pages.Account;

public class LoginModel : PageModel
{
    private readonly IPersonService _personService;
    private readonly ILogger<LoginModel> _logger;

    public LoginModel(IPersonService personService, ILogger<LoginModel> logger)
    {
        _personService = personService ?? throw new ArgumentNullException(nameof(personService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [BindProperty]
    public InputModel Input { get; set; } = new InputModel();

    public string? ErrorMessage { get; set; }

    public class InputModel
    {
        [Required(ErrorMessage = "DNI is required")]
        public string Dni { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }

    public void OnGet()
    {
        _logger.LogInformation("Login page accessed");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            _logger.LogInformation("Login attempt for DNI: {Dni}", Input.Dni);

            var loginDto = new LoginDto
            {
                Dni = Input.Dni,
                Password = Input.Password
            };

            var person = await _personService.AuthenticateAsync(loginDto);

            if (person == null)
            {
                ErrorMessage = "Invalid DNI or password";
                _logger.LogWarning("Login failed for DNI: {Dni}", Input.Dni);
                return Page();
            }

            HttpContext.Session.SetString("UserId", person.Id.ToString());
            HttpContext.Session.SetString("UserName", $"{person.FirstName} {person.LastName}");
            HttpContext.Session.SetString("IsAdmin", person.IsAdmin.ToString());

            _logger.LogInformation("Login successful for DNI: {Dni}", Input.Dni);

            if (person.IsAdmin)
            {
                return RedirectToPage("/Admin/Dashboard");
            }

            return RedirectToPage("/Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            ErrorMessage = "An error occurred during login. Please try again.";
            return Page();
        }
    }
}
