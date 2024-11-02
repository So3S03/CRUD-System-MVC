using Karim.CRUD.DAL._Common._Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.BLL.ModelDtos.EmployeeDtos
{
    public class EmployeeDetailsDto
    {
        //General Data
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public EmployeeWorkType EmployeeWorkType { get; set; }
        public Gender Gender { get; set; }
        public string? Department { get; set; }
        public int? DepartmentId { get; set; }

        //Adminstration Data
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } 
        public int CreatedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public string? PictureUrl { get; set; }

    }
}
