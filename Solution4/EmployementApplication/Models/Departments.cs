using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployementApplication.Models
{
    public class Departments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Employees> Employs { get; set; } 
    }
}