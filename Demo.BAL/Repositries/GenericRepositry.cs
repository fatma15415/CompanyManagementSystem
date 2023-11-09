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
    public class GenericRepositry<T> : IGenericRepository<T> where T : class 
    {
        private protected readonly APPDBContext dbcontext;

        public GenericRepositry(APPDBContext Dbcontext)
        {
            dbcontext = Dbcontext;
        }
        public void Add(T Item)
             =>   dbcontext.Set<T>().Add(Item);

        public void Update(T Item)
           => dbcontext.Set<T>().Update(Item);

        public void Delete(T Item)
            => dbcontext.Set<T>().Remove(Item);
           
      

        public T Get(int id)
         => dbcontext.Set<T>().Find(id);


        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) dbcontext.Employees.Include(e=>e.department).ToList();   
            }
            return dbcontext.Set<T>().ToList();
        }
       

    }
    
}
