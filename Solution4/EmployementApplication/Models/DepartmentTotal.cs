using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployementApplication.Models
{
    public class DepartmentTotal
    {
         public int  Id { get; set; }
        public string Name { get; set; }

        public int Total
        {
            get;
            set;
        }

    }
}