using AutoMapper;
using Demo.BAL.Interfaces;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Demo.PL.Controllers
{
    // Inheritance : DepartmentController is a Controller
    // Composition : DepartmentController has a IDepartmentRepository

    // Association include 
    // 1. Aggregation (Optional)          2. Compostion (Required)
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
      

        public DepartmentController(IMapper mapper, IUnitOfWork unitOfWork)  // Ask CLR For Creating Object From Inteface IDepartmentRepositry
        {
            _mapper = mapper;
            _unitOfWork= unitOfWork;
        }
        public IActionResult Index()
        {
            //Another Binding Through Dictionary : Transfer Data From Action To View => [One Way]
            // 1. ViewData => Dictionary Typed (introduced in Asp .Net Core 3.5) => it Helps us to Transfer data from Controller [Index] to View
            ViewData["Message"] = "Hello ViewData";
            // 2. ViewBag => Dynamic Typed (introduced in Asp .Net core in 4.0 ) => it Helps us to Transfer data from Controller [Index] to View 
            ViewBag.Message = "Hello ViewBag";

            var departments = _unitOfWork.DepartmentRepositry.GetAll();
            var mappeddepartment = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(mappeddepartment);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        // this action  for submit form  
        public IActionResult Create(DepartmentViewModel departmentVM)
        {


            if (ModelState.IsValid)
            {
                var mappeddepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                
                _unitOfWork.DepartmentRepositry.Add(mappeddepartment);
                var count = _unitOfWork.Complete();

                if (count > 0)
                {
                    TempData["Message"] = "Departement Created Succesufully";

                }
                else
                {
                    TempData["Error"] = "Department Not Created ";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }
        [HttpGet]



        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();   // Server Validation
            }
            var department = _unitOfWork.DepartmentRepositry.Get(id.Value);
            var _mappeddepartment = _mapper.Map<Department, DepartmentViewModel>(department);
            if (department is null)
            {
                return NotFound();
            }
            return View(ViewName, _mappeddepartment);
        }

        public IActionResult Edit(int? id)
        {

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
          if (id != departmentVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try

                {
                    var mappeddepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    _unitOfWork.DepartmentRepositry.Update(mappeddepartment);
                    _unitOfWork.Complete(); 
                    //return RedirectToAction(nameof(Index));

                }

                catch (Exception ex)
                {
                    // 1. Log Exception
                    // 2. Friendly Messages

                    ModelState.AddModelError(string.Empty, ex.Message);
                   
                }

            }
            return View(departmentVM);

        } 


    public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
            
                return BadRequest();
            
            try
            {
                if (ModelState.IsValid)
                {
                    var mappeddepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                    _unitOfWork.DepartmentRepositry.Delete(mappeddepartment);
                    _unitOfWork.Complete(); 
                    return RedirectToAction(nameof(Index));

                }

            }
            catch (Exception ex)
            {
                // 1. Log Exception
                // 2. Friendly Messages

                ModelState.AddModelError(string.Empty, ex.Message);
               
            }
            return View(departmentVM);

        }
    }
}
