using Karim.CRUD.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.DAL.Persistence.Initializer
{
    public class DbInitializer(ApplicationDbContext dbContext) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            var PendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (PendingMigrations.Any())
                await dbContext.Database.MigrateAsync();
        }

        //public Task SeedAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
