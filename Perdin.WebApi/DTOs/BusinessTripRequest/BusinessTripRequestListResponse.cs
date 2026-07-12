using System;

namespace Perdin.WebApi.DTOs.BusinessTripRequest
{
    public class BusinessTripRequestListResponse
    {
        public int Id { get; set; }
        public string RequestNumber { get; set; } = null!;
        public string? UserName { get; set; }
        public DateOnly DepartureDate { get; set; }
        public DateOnly ReturnDate { get; set; }
        public string OriginCity { get; set; } = null!;
        public string? DestinationCity { get; set; }
        public string DestinationCountry { get; set; } = null!;
        public string Status { get; set; } = null!;
        public bool IsForeign { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
