using System;

namespace Perdin.BlazorClient.Models
{
    public class BusinessTripRequestUpdateRequest
    {
        public DateOnly? DepartureDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
        public int? OriginCityId { get; set; }
        public int? DestinationCityId { get; set; }
        public int? DestinationCountryId { get; set; }
        public string? Purpose { get; set; }
        public int? UserId { get; set; }
    }
}
