using Karim.CRUD.BLL.ModelDtos.EmailDtos;
using Karim.CRUD.BLL.ThirdPartyServices.EmailSettings;
using Karim.CRUD.DAL.Entities.Identity;
using Karim.CRUD.PL.Models.AccountVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Karim.CRUD.PL.Controllers
{
    public class AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IEmailSettings emailSettings) : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                ModelState.AddModelError(string.Empty, "The Data You Have Enterd Is Invalid, Please Enter A Valid Data Next Time");

            var User = await userManager.FindByEmailAsync(model.Email);

            if(User is null)
                ModelState.AddModelError(nameof(LoginViewModel.Email), "This Account Doesn't Exit, Please Login With An Exist Email");

            var UserPass = await userManager.CheckPasswordAsync(User!, model.Password);
            if(!UserPass)
                ModelState.AddModelError(nameof(LoginViewModel.Password), "Your Password Is Incorrect");

            var CheckEmailAccessablity = await signInManager.PasswordSignInAsync(User!, model.Password, model.RememberMe, true);
            if (CheckEmailAccessablity.IsNotAllowed) // Mean That The Email Not Verified Yet
                ModelState.AddModelError(string.Empty, "Your Account Isn't Verified Yet !");
            if (CheckEmailAccessablity.IsLockedOut)
                ModelState.AddModelError(string.Empty, "Your Account Has Been Locked Out For The Moment");
            if (CheckEmailAccessablity.Succeeded)
                return RedirectToAction("Index", "Home");

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                ModelState.AddModelError(string.Empty, "The Data You Have Entered Is Not Valid, Please Try Enter A Valid Data");

            var IsExistUser = await userManager.FindByEmailAsync(model.Email);
            if (IsExistUser is not null)
                ModelState.AddModelError(string.Empty, "You Try To Use An Email That Is Already Exist!");

            var MappedUser = new ApplicationUser()
            {
                FisrtName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                IsAgreeOnTerms = model.IsAgreeOnTerms
            };
            var NewUser = await userManager.CreateAsync(MappedUser, model.Password);
            if (!NewUser.Succeeded)
            {
                foreach (var err in NewUser.Errors)
                    ModelState.AddModelError(string.Empty, err.Description);
            }
            else
                return RedirectToAction(nameof(Login));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult CheckYourEmailInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "The Email You Have Entered Is Not Valid, Please Try Send A Valid Email");
                return View(model);
            }
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "This Email Doesn't Exist");
                return View(model);
            }
            var resetPassToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var passwordUrl = Url.Action(nameof(ResetPassword), "Account", new { email = user.Email, token = resetPassToken }, Request.Scheme);
            var Message = "We Have Recived Your Request For Resetting Your Account Password, \n Please Click On The Following Link To Redirect You To The Reset Password Form.";

			var email = new Email()
            {
                To = model.Email,
                Subject = "Reset Password",
                Body = $"{Message} \n \n \n \n {passwordUrl!}"
            };
            emailSettings.SendEmail(email);
            return RedirectToAction(nameof(CheckYourEmailInbox));
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {

            if (!ModelState.IsValid)
                ModelState.AddModelError(string.Empty, "You Have Entered an Invalid Data, Please Try Again");

            string email = TempData["email"] as string ?? string.Empty;
            string token = TempData["token"] as string ?? string.Empty;

            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
                ModelState.AddModelError(string.Empty, "The User You Try To Reset It's Password Isn't Found");

            var result = await userManager.ResetPasswordAsync(user!, token, model.NewPassword);
            if (result.Succeeded) return RedirectToAction(nameof(Login));
            else
                foreach (var err in result.Errors)
                    ModelState.AddModelError(string.Empty, err.Description);

            return View(model);

        }

	}
}
