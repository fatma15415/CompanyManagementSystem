using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    //Domain Model
    public class Department
    {

        
        public int Id { get; set; }

        // this should be exist ar view model not model
     
        public string Code { get; set; }
        [Required ]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; } 
        public ICollection<Employee> employees { get; set; }=new HashSet<Employee>();   
        //Navigational property [Many]
        
    }
}
