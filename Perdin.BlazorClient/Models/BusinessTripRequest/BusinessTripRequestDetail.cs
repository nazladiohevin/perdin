using System;

namespace Perdin.BlazorClient.Models
{
    public class BusinessTripRequestDetail
    {
        public int Id { get; set; }
        public string RequestNumber { get; set; } = string.Empty;
        public BusinessTripRequestUser User { get; set; } = new();
        public DateOnly DepartureDate { get; set; }
        public DateOnly ReturnDate { get; set; }
        public int OriginCityId { get; set; }
        public string OriginCityName { get; set; } = string.Empty;
        public int? DestinationCityId { get; set; }
        public string? DestinationCityName { get; set; }
        public int DestinationCountryId { get; set; }
        public string DestinationCountryName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int? ApproverId { get; set; }
        public string? ApproverName { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public int PocketMoney { get; set; }
        public double? Distance { get; set; }
        public int TotalDays { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class BusinessTripRequestUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
