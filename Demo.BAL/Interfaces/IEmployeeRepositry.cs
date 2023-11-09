using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BAL.Interfaces
{
    public interface IEmployeeRepositry:IGenericRepository<Employee>
    {
        public IQueryable<Employee> GetEmployeeByAddress (string address); 

       public IQueryable<Employee> SearchbyName (string name); 

    }
}
