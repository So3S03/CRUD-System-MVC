using System.ComponentModel.DataAnnotations;

namespace Karim.CRUD.PL.Models.AccountVM
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "In Order To Reset Your Password You Should Provid Your Email")]
		[EmailAddress(ErrorMessage = "Email Address Must Be Like This [Example@mail.com]")]
        public required string Email { get; set; }
    }
}
