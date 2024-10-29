using Karim.CRUD.DAL._Common._Enums;
using System.ComponentModel.DataAnnotations;

namespace Karim.CRUD.PL.Models.EmployeeVM
{
    public class CreateUpdateEmpsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name Is Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last Name Is Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Age Is Required")]
        public int Age { get; set; }

        [Display(Name = "Is Employee Active ?")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Work Type Is Required")]
        [Display(Name = "Work Type")]
        public EmployeeWorkType EmployeeWorkType { get; set; }

        [Required(ErrorMessage = "Gender Is Required")]
        public Gender Gender { get; set; }

        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
    }
}
