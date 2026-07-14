using System.ComponentModel.DataAnnotations;

namespace Perdin.WebApi.Features.Users.Update;

public class UpdateUserRequest
{
    [Required(ErrorMessage = "Nama wajib diisi")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Username wajib diisi")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Email wajib diisi")]
    [EmailAddress(ErrorMessage = "Format email tidak valid")]
    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public List<int>? RoleIds { get; set; }
}
