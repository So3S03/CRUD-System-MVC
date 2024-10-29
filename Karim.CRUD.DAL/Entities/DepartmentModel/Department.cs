using Karim.CRUD.DAL.Entities.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.DAL.Entities.DepartmentModel
{
    public class Department : BaseModel
    {
        public string Code { get; set; } = null!; //Department Uniqe Code
        public string Name { get; set; } = null!;
        public string? Description { get; set; } //Department Work Description
        public DateOnly DateOfCreation { get; set; } //When The Department is Created By Only Date [Even If It's Created Before The System Exist]



        //Navigational Property [Many]
        
        //Work Relationship
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

        //Manage Relationship
        public virtual Employee Manager { get; set; }
        public int? ManagerId { get; set; }

    }
}
