namespace Perdin.BlazorClient.Models
{
    public class CityItem
    {
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
