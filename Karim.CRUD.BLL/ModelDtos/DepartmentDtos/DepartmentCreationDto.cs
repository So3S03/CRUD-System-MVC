using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.BLL.ModelDtos.DepartmentDtos
{
    public class DepartmentCreationDto
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly DepartmentCreationDate { get; set; }
        public int? ManagerId { get; set; }
    }
}
