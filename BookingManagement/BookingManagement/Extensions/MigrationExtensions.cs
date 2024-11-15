using BookingManagement.Database;
using Microsoft.EntityFrameworkCore;

namespace BookingManagement.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using BookingManagementDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<BookingManagementDbContext>();

            dbContext.Database.Migrate();
        }

    }
}
