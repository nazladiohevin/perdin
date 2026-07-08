namespace Perdin.WebApi.Models
{
    public class BusinessTripRequest
    {
        public int Id { get; set; }
        public string RequestNumber { get; set; } = null!;

        public int UserId { get; set; }

        public DateOnly DepartureDate { get; set; }
        public DateOnly ReturnDate { get; set; }

        public int OriginCityId { get; set; }
        public int? DestinationCityId { get; set; }
        public int DestinationCountryId { get; set; }

        public int DurationInDays { get; set; }

        public string Status { get; set; } = "reviewed";

        public int ApproverId { get; set; }

        public string Purpose { get; set; } = null!;

        public int PocketMoney { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public User Approver { get; set; } = null!;
        public City OriginCity { get; set; } = null!;
        public City? DestinationCity { get; set; }
        public Country DestinationCountry { get; set; } = null!;
    }
}
