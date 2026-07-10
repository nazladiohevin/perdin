using System.ComponentModel.DataAnnotations;

namespace Perdin.WebApi.DTOs.User
{
    public class UserCreateRequest
    {
        [Required(ErrorMessage = "Name wajib diisi.")]
        [MinLength(3, ErrorMessage = "Name minimal 3 karakter.")]
        [MaxLength(100, ErrorMessage = "Name maksimal 100 karakter.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Username wajib diisi.")]
        [MinLength(3, ErrorMessage = "Username minimal 3 karakter.")]
        [MaxLength(30, ErrorMessage = "Username maksimal 30 karakter.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Email wajib diisi.")]
        [EmailAddress(ErrorMessage = "Email tidak valid.")]
        [MaxLength(30, ErrorMessage = "Email maksimal 30 karakter.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password wajib diisi.")]
        [MinLength(6, ErrorMessage = "Password minimal 6 karakter.")]
        [MaxLength(30, ErrorMessage = "Password maksimal 30 karakter.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "RoleIds wajib diisi.")]
        [MinLength(1, ErrorMessage = "RoleIds harus memiliki minimal 1 role.")]
        public List<int> RoleIds { get; set; } = null!;
    }
}
