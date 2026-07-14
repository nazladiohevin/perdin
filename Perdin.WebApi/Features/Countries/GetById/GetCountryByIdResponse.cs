namespace Perdin.WebApi.Features.Countries.GetById;

public class GetCountryByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsForeign { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
