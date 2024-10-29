using Karim.CRUD.DAL.Entities.DepartmentModel;
using Karim.CRUD.DAL.Persistence.Data;
using Karim.CRUD.DAL.Persistence.Repository._GenaricRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.DAL.Persistence.Repository.DepartmentRepository
{
    public class DepartmentRepository : GenaricRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext){ }
    }
}
