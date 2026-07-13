using System;

namespace Perdin.BlazorClient.Models.Country
{
    public class CountryListItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsForeign { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
