using Demo.BAL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BAL.Repositries
{
    public class EmployeeReprositry : GenericRepositry<Employee> ,IEmployeeRepositry
    {

        //DI

        // To Do Constructor Chaining with Parent (GenericRepositry)
        public EmployeeReprositry(APPDBContext dbcontext) : base(dbcontext)
        {
        }

        public IQueryable<Employee> GetEmployeeByAddress(string address)
        {
            return dbcontext.Employees.Where(e => e.Address == address);
        }

        public IQueryable<Employee> SearchbyName(string name)
        => dbcontext.Employees.Where(E => E.Name.ToLower().Contains(name));
    }
}
