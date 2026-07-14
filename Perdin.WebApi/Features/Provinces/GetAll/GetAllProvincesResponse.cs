namespace Perdin.WebApi.Features.Provinces.GetAll;

public class GetAllProvincesResponse
{
    public int Id { get; set; }
    public int CountryId { get; set; }
    public string Name { get; set; } = null!;
    public string? Island { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
