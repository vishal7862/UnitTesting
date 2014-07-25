using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployementApplication.Models
{
    public class Departments
    {
        public int Id { get; set; }
        [Required()]
        public string Name { get; set; }
        public virtual IEnumerable<Employees> Employs { get; set; } 
    }
}