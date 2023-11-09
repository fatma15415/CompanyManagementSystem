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
    public class DepartmentRepositry :GenericRepositry<Department>,IDepartmentRepositry
    {
        //DI
        public DepartmentRepositry(APPDBContext dbcontext) :base(dbcontext)
        {
                
        }





    }
}