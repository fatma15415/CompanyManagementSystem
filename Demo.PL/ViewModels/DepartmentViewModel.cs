using Demo.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace Demo.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        // this should be exist ar view model not model
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is rquired!!")]
        public string Name { get; set; }
        [Display(Name = "Date Of Creation")]

        public DateTime DateOfCreation { get; set; }

        public ICollection<Employee> employees { get; set; } = new HashSet<Employee>();
        //Navigational property [Many]
    }
}
