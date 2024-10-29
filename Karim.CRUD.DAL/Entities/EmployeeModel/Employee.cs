using Karim.CRUD.DAL._Common._Enums;
using Karim.CRUD.DAL.Entities.DepartmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.DAL.Entities.EmployeeModel
{
    public class Employee : BaseModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Age { get; set; } //Company Only Recrutes Employees That There Age Is Between [19 - 37]
        public bool IsActive { get; set; } // For Knowing If He Start Work Or Not And Having Training
        public EmployeeWorkType EmployeeWorkType { get; set; }
        public Gender Gender { get; set; }




        //Navigational Properties

        //Work Relationship
        public virtual Department Department { get; set; }
        public int? DepartmentId { get; set; }

        //Manage Relationship
        public virtual Department ManagedDepartment { get; set; }
    }
}
