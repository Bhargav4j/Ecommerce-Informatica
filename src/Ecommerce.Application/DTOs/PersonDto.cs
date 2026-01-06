namespace Ecommerce.Application.DTOs;

public class PersonDto
{
    public int Id { get; set; }
    public string Dni { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
}

public class PersonCreateDto
{
    public string Dni { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsAdmin { get; set; } = false;
}

public class PersonUpdateDto
{
    public int Id { get; set; }
    public string Dni { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string Email { get; set; } = string.Empty;
}

public class LoginDto
{
    public string Dni { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
