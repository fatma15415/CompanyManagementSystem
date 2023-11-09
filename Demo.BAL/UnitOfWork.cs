using Demo.BAL.Interfaces;
using Demo.BAL.Repositries;
using Demo.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BAL
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly APPDBContext _dBContext;

        public IEmployeeRepositry EmployeeRepositry { get; set; }
        public IDepartmentRepositry DepartmentRepositry { get; set; }

        public UnitOfWork(APPDBContext DBContext)
        {
            _dBContext = DBContext;
            EmployeeRepositry = new EmployeeReprositry(_dBContext);

            DepartmentRepositry = new DepartmentRepositry(_dBContext);
        }
        public int Complete ()
        {
            return _dBContext.SaveChanges();
        }
        public void Dispose ()
        {
            _dBContext.Dispose();

        }

    }
}
