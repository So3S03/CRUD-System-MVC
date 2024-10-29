using Karim.CRUD.DAL.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.DAL.Persistence.Initializer
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
        //Task SeedAsync();
    }
}
