using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var role = await _roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name
                }).ToListAsync();
                return View(role);

            }
            else
            {
                var role = await _roleManager.FindByNameAsync(name);

                if(role is not null)

                { 
                    var Mappedrole = new RoleViewModel()
                    {
                        Id = role.Id,
                        RoleName = role.Name

                    };
                    return View(new List<RoleViewModel>() { Mappedrole });
                }
                return View(Enumerable.Empty<RoleViewModel>());
            }
        }

        public  IActionResult Create()
        {
            return  View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var mappedrole = _mapper.Map<RoleViewModel, IdentityRole>(roleModel);
                await _roleManager.CreateAsync(mappedrole);
                return RedirectToAction(nameof(Index));
            }
            return View(roleModel);

        }

        //role/Details/Guid
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();   // Server Validation
            }
            var role = await _roleManager.FindByIdAsync(id);

            var Mappedrole = _mapper.Map<IdentityRole, RoleViewModel>(role);

            if (role is null)
                return NotFound();

            return View(ViewName, Mappedrole);
        }

        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel Updatedrole)
        {
            if (id != Updatedrole.Id)
                return BadRequest();

            if (ModelState.IsValid)
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = Updatedrole.RoleName;
                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));


                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            return View(Updatedrole);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost(Name = "Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _roleManager.FindByIdAsync(id);
                    await _roleManager.DeleteAsync(user);
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




