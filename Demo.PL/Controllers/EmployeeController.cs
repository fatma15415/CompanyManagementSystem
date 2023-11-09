using AutoMapper;
using Demo.BAL.Interfaces;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using Demo.PL.Helper;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeRepositry _employeeRepo;
        //private readonly IDepartmentRepository _departmentRepo;

        public EmployeeController(IMapper mapper,IUnitOfWork unitOfWork /*IEmployeeRepositry employeeRepo , IDepartmentRepository departmentRepo*/)  // Ask CLR For Creating Object From Inteface IEmployeeRepositry
        {
           _mapper = mapper;
            _unitOfWork = unitOfWork;
          //  _employeeRepo = employeeRepo;
        }

       // [HttpGet]
        public IActionResult Index(string SearchInput)
        {
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = _unitOfWork.EmployeeRepositry.GetAll();

            }
            else
            {
                employees = _unitOfWork.EmployeeRepositry.SearchbyName(SearchInput.ToLower());

            }

            var Mappedemp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(Mappedemp);


        }
        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        // this action  for submit form  
        public IActionResult Create(EmployeeViewModel EmployeeVM)
        {
            if (ModelState.IsValid)
            {
               EmployeeVM.ImageName= DocumentSetting.Uploadfile(EmployeeVM.Image, "Images");

                //Manual Mapping
                #region Manual Mapping
                //var mappedEmp = new Employee()
                //{
                //    Name=EmployeeVM.Name,
                //    Age=EmployeeVM.Age, 
                //    Address=EmployeeVM.Address, 
                //    Salary=EmployeeVM.Salary,   
                //    Email=EmployeeVM.Email,
                //    PhoneNumber=EmployeeVM.PhoneNumber,
                //    ISActive=EmployeeVM.ISActive,
                //    HireDate=EmployeeVM.HireDate
                //};
                #endregion

               //AutoMapping with automapper
                var Mappedemp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);

                _unitOfWork.EmployeeRepositry.Add(Mappedemp);
                _unitOfWork.Complete();
                var count = _unitOfWork.Complete();
                if (count > 0)

                    return RedirectToAction(nameof(Index));
                
            }
            return View(EmployeeVM);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();   // Server Validation
            }
            var Employee = _unitOfWork.EmployeeRepositry.Get(id.Value);

            var Mappedemp= _mapper.Map<Employee,EmployeeViewModel>(Employee);  

            if (Employee is null)
                return NotFound();
         
            return View(ViewName, Mappedemp);
        }

        public IActionResult Edit(int? id)
        {
          
            ViewBag.Departments = _unitOfWork.EmployeeRepositry.GetAll();
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel EmployeeVM)
        {
            if (id != EmployeeVM.Id)
                 return BadRequest();

            if (ModelState.IsValid)
                try
                {

                    var Mappedemp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);

                    _unitOfWork.EmployeeRepositry.Update(Mappedemp);
                    _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
               

                }
            catch (Exception ex)
            {
                // 1. Log Exception
                // 2. Friendly Messages

                ModelState.AddModelError(string.Empty, ex.Message);
               
            }
            return View(EmployeeVM);
        }

        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, EmployeeViewModel EmployeeVM)
        {
            if (id != EmployeeVM.Id)
            {
                return BadRequest();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var Mappedemp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);

                    _unitOfWork.EmployeeRepositry.Delete(Mappedemp);
                    var count =_unitOfWork.Complete();
                    if (count > 0)
                    {
                        DocumentSetting.DeleteFile(EmployeeVM.ImageName, "images");
                    }
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                // 1. Log Exception
                // 2. Friendly Messages

                ModelState.AddModelError(string.Empty, ex.Message);
                
            }
            return View(EmployeeVM);

        }
    }
}
