using Karim.CRUD.DAL._Common._Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.BLL.ModelDtos.EmployeeDtos
{
    public class GetAllEmployeeDto
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Age { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Work Type")]
        public string EmployeeWorkType { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? Department { get; set; }
    }
}
