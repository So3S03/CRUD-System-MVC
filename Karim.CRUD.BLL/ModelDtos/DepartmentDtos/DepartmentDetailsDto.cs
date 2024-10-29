using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.BLL.ModelDtos.DepartmentDtos
{
    public class DepartmentDetailsDto
    {
        //Department General Data
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly DateOfCreation { get; set; }
        public string Manager { get; set; } = null!;
        public int? ManagerId { get; set; }

        //Department Adminstration Data
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
