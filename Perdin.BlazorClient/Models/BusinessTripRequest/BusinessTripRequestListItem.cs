using System;

namespace Perdin.BlazorClient.Models
{
    public class BusinessTripRequestListItem
    {
        public int Id { get; set; }
        public string RequestNumber { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public DateOnly DepartureDate { get; set; }
        public DateOnly ReturnDate { get; set; }
        public string OriginCity { get; set; } = string.Empty;
        public string? DestinationCity { get; set; }
        public string DestinationCountry { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool IsForeign { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
