using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; } 
        [Required(ErrorMessage = "Name is Rquired !!")]
        [MaxLength(50, ErrorMessage = "MaxLength is 50 Chars")]
        [MinLength(10, ErrorMessage = "MinLength is 10 Chars")]
        public string Name { get; set; }

        [Range(22, 35, ErrorMessage = "Age Must be Between 22 , 35")]
        public int? Age { get; set; }
        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
            , ErrorMessage = "Adrress must be like 123 street-city-country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool ISActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }

        public IFormFile Image  { get; set; }
        public string ImageName { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; } //FK
        // FK Rquired ==> ONdELETE :: CASCADE
        //FK Optional ==> ONDelete :: restrict
        public Department department { get; set; }          //Navigational property [One]

        public DateTime CreationOnDate { get; set; } = DateTime.Now;

    }
}
