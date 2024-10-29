using Karim.CRUD.DAL._Common._Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.BLL.ModelDtos.EmployeeDtos
{
    public class EmployeeUpdateDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public EmployeeWorkType EmployeeWorkType { get; set; }
        public Gender Gender { get; set; }
        public int? DepartmentId { get; set; }
    }
}
