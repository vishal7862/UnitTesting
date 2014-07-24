using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployementApplication.Models
{
    public interface IEmployeesRepository
    {
        IEnumerable<Employees> GetAllEmployees();
        void CreateEmployee(Employees employee);
        void Edit(Employees employee);
        void DeleteEmp(int id);
    }
}