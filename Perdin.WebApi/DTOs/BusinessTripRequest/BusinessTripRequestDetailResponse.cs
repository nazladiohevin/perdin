using System;

namespace Perdin.WebApi.DTOs.BusinessTripRequest
{
    public class BusinessTripRequestDetailResponse
    {
        public int Id { get; set; }
        public string RequestNumber { get; set; } = null!;
        
        public UserInfo User { get; set; } = null!;

        public DateOnly DepartureDate { get; set; }
        public DateOnly ReturnDate { get; set; }

        public int OriginCityId { get; set; }
        public string OriginCityName { get; set; } = null!;
        public int? DestinationCityId { get; set; }
        public string? DestinationCityName { get; set; }
        public int DestinationCountryId { get; set; }
        public string DestinationCountryName { get; set; } = null!;

        public string Status { get; set; } = null!;
        public int? ApproverId { get; set; }
        public string? ApproverName { get; set; }
        public string Purpose { get; set; } = null!;
        public int PocketMoney { get; set; }
        
        public double? Distance { get; set; }
        public int TotalDays { get; set; }

        public DateTime? ApprovedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public class UserInfo
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
        }
    }
}
