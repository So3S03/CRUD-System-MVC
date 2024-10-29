using System.ComponentModel.DataAnnotations;

namespace Karim.CRUD.PL.Models.AccountVM
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "You Must Provid A Password")]
		[RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
			ErrorMessage = "Password Must Have At Least 1 Uppercase, 1 Lowercase, 1 Number, 1 Non Alphnumeric And At Least 6 Characters")]
		[DataType(DataType.Password)]
        public required string NewPassword { get; set; }

		[Required(ErrorMessage = "You Must Provid A Password That Match The One You Have Etred Before")]
        [Compare(nameof(NewPassword), ErrorMessage = "The Password You Have Entered Doesn't Match With The Other Password")]
        [DataType(DataType.Password)]
        public required string ConfirmNewPassword { get; set; }
    }
}
