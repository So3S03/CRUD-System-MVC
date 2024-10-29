using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.BLL.ModelDtos.DepartmentDtos
{
    public class GetAllDepartmentDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        [Display(Name = "Description")]
        public string? SmallDescription { get; set; }

        [Display(Name = "Date Of Creation")]
        public DateOnly DepartmentCreationDate { get; set; }
        public string? Manager { get; set; }
    }
}
