using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace EmployementApplication.Models
{
    public class EFEmployeesRepository : IEmployeesRepository
    {
      public ManagementDbContext db =new ManagementDbContext();

        public IEnumerable<Employees> GetAllEmployees()
        {
            return db.Employs.ToList();
        }

      public void CreateEmployee(Employees employee)
      {
          db.Employs.Add(employee);
          db.SaveChanges();

      }

         public void Edit(Employees employee)
        {
           db.Entry(employee).State=EntityState.Modified;
           db.SaveChanges();
        }

        public void DeleteEmp(int id)
        {
            Employees emp=db.Employs.Find(id);
            db.Employs.Remove(emp);
            db.SaveChanges();
        }
     
    }
}