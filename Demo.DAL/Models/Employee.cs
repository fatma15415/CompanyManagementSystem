using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int? Age { get; set; }
        
        public string Address { get; set; }    
        public decimal Salary { get; set; }

        public bool ISActive { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }  

        public DateTime HireDate { get; set; }
        public DateTime CreationOnDate { get; set; } = DateTime.Now;
        public string ImageName { get; set; }

        public int? DepartmentId { get; set; } //FK
        // FK Rquired ==> ONdELETE :: CASCADE
        //FK Optional ==> ONDelete :: restrict
        public Department department { get; set; }          //Navigational property [One]




    }
}
