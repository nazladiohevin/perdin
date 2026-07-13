using System.Collections.Generic;

namespace Perdin.BlazorClient.Models
{
    public class UserUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; }
        public List<int> RoleIds { get; set; } = new();
    }
}
