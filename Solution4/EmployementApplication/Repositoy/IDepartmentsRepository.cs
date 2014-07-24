using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployementApplication.Models
{
    public interface IDepartmentsRepository
    {
        IEnumerable<Departments> GetAllDepartments();

        void CreateDepartment(Departments dept);

        void EditDept(Departments dept);
        void Delete(int id);
    }
}