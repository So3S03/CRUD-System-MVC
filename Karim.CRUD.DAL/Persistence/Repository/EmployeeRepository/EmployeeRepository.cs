using Karim.CRUD.DAL.Entities.EmployeeModel;
using Karim.CRUD.DAL.Persistence.Data;
using Karim.CRUD.DAL.Persistence.Repository._GenaricRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.DAL.Persistence.Repository.EmployeeRepository
{
    public class EmployeeRepository : GenaricRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    }
}
