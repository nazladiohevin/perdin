namespace Perdin.WebApi.Data.Seeders
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // Seed static lookup/config tables first (no dependencies)
            RoleSeeding.Seed(context);
            CountrySeeding.Seed(context);

            // Seed tables with dependencies
            ProvinceSeeding.Seed(context);
            CitySeeding.Seed(context);
            UserSeeding.Seed(context);
            
            // Seed tables dependent on users and cities
            BusinessTripRequestSeeding.Seed(context);
        }
    }
}