using HealthCare_Project.Entites;
using System.Text.Json;

namespace HealthCare_Project.Data
{
    public static class DoctorsSeeding
    {
        public async static Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (!dbContext.Doctors.Any())
            {
                var doctorsData = File.ReadAllText("C:\\Users\\omar2\\OneDrive\\Desktop\\C# Drafts\\HealthCare Project\\HealthCare Project\\Data\\Seeding\\Doctors.json");
                var doctors = JsonSerializer.Deserialize<List<Doctor>>(doctorsData);

                if (doctors is null)
                    return;

                await dbContext.Doctors.AddRangeAsync(doctors);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
