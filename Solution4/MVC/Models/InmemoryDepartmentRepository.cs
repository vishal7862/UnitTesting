using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployementApplication.Models;
namespace MVC.Models
{
    class InmemoryDepartmentRepository : IDepartmentsRepository
    {
        List<Departments> db =new List<Departments>();
        public IEnumerable<Departments> GetAllDepartments()
        {
            return db.ToList();
        }

        public void CreateDepartment(Departments dept)
        {
            db.Add(dept);
        }

        public void  EditDept(Departments dept)
        {
            foreach (Departments departments in db)
            {
                if (departments.Id == dept.Id)
                {
                    db.Remove(dept);
                    db.Add(dept);
                    break;
                }
            }

        }

        public void Delete(int id)
        {
            Departments  dept = db.Single(dpt => dpt.Id == id);
            db.Remove(dept);

        }
    }
}
