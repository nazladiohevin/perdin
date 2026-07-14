using System.ComponentModel.DataAnnotations;

namespace Perdin.WebApi.Features.BusinessTripRequests.Create;

public class CreateBusinessTripRequestRequest
{
    [Required(ErrorMessage = "Tanggal keberangkatan wajib diisi dengan format YYYY-MM-DD.")]
    public DateOnly? DepartureDate { get; set; }

    [Required(ErrorMessage = "Tanggal kepulangan wajib diisi dengan format YYYY-MM-DD.")]
    public DateOnly? ReturnDate { get; set; }

    [Required(ErrorMessage = "Kota asal wajib diisi.")]
    public int OriginCityId { get; set; }

    public int? DestinationCityId { get; set; }
    public int? DestinationCountryId { get; set; }

    [Required(ErrorMessage = "Tujuan perjalanan wajib diisi.")]
    public string Purpose { get; set; } = null!;

    public int? UserId { get; set; }
}
