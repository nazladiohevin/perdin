using System.Collections.Generic;

namespace Perdin.WebApi.DTOs.User
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<RoleItem> Roles { get; set; } = new();
    }

    public class RoleItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
