using System.ComponentModel.DataAnnotations;

namespace Perdin.WebApi.Features.Users.Create;

public class CreateUserRequest
{
    [Required(ErrorMessage = "Nama wajib diisi")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Username wajib diisi")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Email wajib diisi")]
    [EmailAddress(ErrorMessage = "Format email tidak valid")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password wajib diisi")]
    public string Password { get; set; } = null!;

    public List<int>? RoleIds { get; set; }
}
