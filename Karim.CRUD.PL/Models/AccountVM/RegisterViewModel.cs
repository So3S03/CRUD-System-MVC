using System.ComponentModel.DataAnnotations;

namespace Karim.CRUD.PL.Models.AccountVM
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "You Must Provid Your First Name")]
        [Display(Name = "First Name")]
        [MaxLength(70)]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "You Must Provid Your Last Name")]
        [Display(Name = "Last Name")]
        [MaxLength(70)]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "You Must Provid Your User Name")]
        [Display(Name = "User Name")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "You Must Provid An Email")]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "You Must Provid A Password")]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
            ErrorMessage = "Password Must Have At Least 1 Uppercase, 1 Lowercase, 1 Number, 1 Non Alphnumeric And At Least 6 Characters")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Re-Password")]
        [Compare(nameof(Password), ErrorMessage = "You Must Enter The Same Password You Have Enterd Before")]
        public required string RePassword { get; set; }
        public bool IsAgreeOnTerms { get; set; }

    }
}
