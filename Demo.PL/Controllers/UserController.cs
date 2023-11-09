using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager
			,IMapper mapper)
        {
			_userManager = userManager;
			_signInManager = signInManager;
            _mapper = mapper;
        }
		public async Task<IActionResult> Index(string email)
		{
			if (string.IsNullOrEmpty(email)) 
			{
				var Users =await _userManager.Users.Select(U => new UsersViewModel()
				{
					Id = U.Id,
					Fname = U.Fname,
					Lname = U.Lname,
					Email = U.Email,
					Phonenumber = U.PhoneNumber,
					Roles =_userManager.GetRolesAsync(U).Result

				}).ToListAsync() ;
				return View(Users);	

			}
            else
            {
				var user =await _userManager.FindByEmailAsync(email);
				var Mappeduser= new UsersViewModel()
				{
					Id = user.Id,
					Fname = user.Fname,
					Lname = user.Lname,
					Email = user.Email,
					Phonenumber = user.PhoneNumber,
					Roles = _userManager.GetRolesAsync(user).Result

				};
				return View(new List<UsersViewModel>() {Mappeduser });
			}
        }

		//User/Details/Guid
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();   // Server Validation
            }
            var user =await _userManager.FindByIdAsync(id);

            var Mappeduser = _mapper.Map<ApplicationUser, UsersViewModel>(user);

            if (user is null)
                return NotFound();

            return View(ViewName, Mappeduser);
        }

        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UsersViewModel Updateduser)
        {
            if (id != Updateduser.Id)
                return  BadRequest();

            if (ModelState.IsValid)
                try
                {
                    var user=await _userManager.FindByIdAsync(id);
                    user.Fname = Updateduser.Fname;
                    user.Lname= Updateduser.Lname;
                    user.PhoneNumber = Updateduser.Phonenumber;
                    await _userManager.UpdateAsync(user); 
                    return RedirectToAction(nameof(Index));


                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            return View(Updateduser);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost (Name ="Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete( string id)
        {
           try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByIdAsync(id);
                    await _userManager.DeleteAsync(user);
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
               
            }

            return RedirectToAction("Error", "Home");
        }


    }
}
