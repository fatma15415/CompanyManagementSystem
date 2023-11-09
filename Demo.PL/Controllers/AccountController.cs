using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager ,SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
        #region SignUP
        public IActionResult SignUp()
        {
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid) //server side validation
            {
                var User = await _userManager.FindByNameAsync(model.Username);
                if(User is null)
                {
					User = new ApplicationUser()
					{
						// UserName = model.Email.Split("@")[0], //fatma@gmail.com
						UserName = model.Username,
						Email = model.Email,
						isAgree = model.isAgree,
						Fname = model.Fname,
						Lname = model.Lname,
						//PASSWORD anwer p@ssw0rdA
					};
					var Result =await _userManager.CreateAsync(User, model.Password);
					if(Result.Succeeded) 
					
						return RedirectToAction(nameof(SignIn));
					foreach (var error in Result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
					
				}

				ModelState.AddModelError(string.Empty,"User Name is already exists");
				
            }

            return View(model);  
        }

        #endregion

        #region Sign In

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModelView signInViewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(signInViewModel.Email);
                    if (user is null)
                        ModelState.AddModelError("", "email doesn't exist");

                    var isCorrectPassword = await _userManager.CheckPasswordAsync(user, signInViewModel.Password);
                    if (isCorrectPassword)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, signInViewModel.Password, signInViewModel.RememberMe, false);
                        if (result.Succeeded) return RedirectToAction("Index", "Home");
                    }

                }
                return View(signInViewModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

        public IActionResult SignIn()
		{
			return View();
		}

        #endregion

        #region Sign Out
        public async new Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();    
            return RedirectToAction(nameof(SignIn));
        }



        #endregion

        #region ForgetPassword
        public IActionResult ForgetPassword()
        {
            return View();
        }
        #endregion
        [HttpPost]
        public async Task<IActionResult> SendRestPasswordUrl(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            { 
                var User= await _userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    var token =await _userManager.GeneratePasswordResetTokenAsync(User); //Token>> unique fir this user for one time
                    var RestPasswordurl = Url.Action("RestPassword", "Account", new { email = model.Email, token },Request.Scheme);
                    var email = new Email()
                    {
                        Subject= "Reset your password",
                        Recipients= model.Email   ,
                        Body= "RestPasswordurl"
                    };
                    EmailSettings.SendEmails(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Invalid Email");
               

            }
            return View(model);
        }

        public IActionResult CheckYourInbox()
        {
            return View();

        }

        #region Rest Password
        public IActionResult RestPassword(string email, string token)
        {
			TempData["email"] = email;
            TempData["token"] = token;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> RestPassword(ResetPasswordModelView model)
        {
            if (ModelState.IsValid)  //=> server side validation
            {
				string email = TempData["email"] as string;
				string token = TempData["token"] as string;

				var user = await _userManager.FindByEmailAsync(email);

				var Result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

				if (Result.Succeeded)
					return RedirectToAction(nameof(SignIn));
				foreach (var error in Result.Errors)

					ModelState.AddModelError(string.Empty, error.Description);

			}
            return View(model);

			
        }

		#endregion
	}
}
