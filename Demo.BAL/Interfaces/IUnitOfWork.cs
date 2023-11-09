using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepositry EmployeeRepositry { get; set; }
        public IDepartmentRepositry DepartmentRepositry { get; set; }

         int Complete();


    }
}
