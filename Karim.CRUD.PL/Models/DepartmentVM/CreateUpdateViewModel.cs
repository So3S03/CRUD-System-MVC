using System.ComponentModel.DataAnnotations;

namespace Karim.CRUD.PL.Models.DepartmentVM
{
    public class CreateUpdateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code Is Requierd")]
        public string Code { get; set; } = null!;

        [Required(ErrorMessage = "Name Is Requierd")]
        [Display(Name = "Department Name")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [Required(ErrorMessage = "Creation Date Is Requierd")]
        [Display(Name = "Creation Date")]
        public DateOnly DepartmentCreationDate { get; set; }
        public int? ManagerId { get; set; }
    }
}
