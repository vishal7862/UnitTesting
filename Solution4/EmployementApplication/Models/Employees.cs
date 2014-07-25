using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployementApplication.Models
{
    public class Employees
    {

        public int Id { get; set; }
       [Required()]
        public string Name { get; set; }
       
        public int DepartmentId { get; set; }
  
        public virtual Departments Department { get; set; } 
    }
}