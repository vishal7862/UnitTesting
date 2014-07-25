using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployementApplication.Models;

namespace MVC.Models
{
    class InmemoryEmployeesRepository:IEmployeesRepository
    {
        private List<Employees> db = new List<Employees>();

       
        public IEnumerable<Employees> GetAllEmployees()
        {
            return db.ToList();
        }

        public void CreateEmployee(Employees employee)
        {
            db.Add(employee);
        }

        public void Edit(Employees employee)
        {
            foreach (Employees emp in db)
            {
                if (emp.Id == employee.Id)
                {
                    db.Remove(emp);
                    db.Add(employee);
                    break;
                }
            }
            
        }

        public void DeleteEmp(int id)
        {
            Employees emp = db.Single(e => e.Id == id);
            db.Remove(emp);

        }
     
    }
}
