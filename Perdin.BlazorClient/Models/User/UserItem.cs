using System;
using System.Collections.Generic;

namespace Perdin.BlazorClient.Models
{
    public class UserItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<RoleItemModel> Roles { get; set; } = new();
    }
}
