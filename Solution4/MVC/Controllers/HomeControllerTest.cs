using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Helpers;
using EmployementApplication.Controllers;
using EmployementApplication.Models;
using EmployementApplication;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployementApplication.Controllers;
using EmployementApplication.Models;
using System.Security.Principal;
using Moq;
using MVC.Models;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using Json = System.Web.Helpers.Json;

namespace MVC
{
    [TestClass]
    public class HomeControllerTest
    {
        private static HomeController GetHomeController(IEmployeesRepository employeesRepository, IDepartmentsRepository deptRepository)
        {
            HomeController controller = new HomeController(employeesRepository, deptRepository);

            controller.ControllerContext = new ControllerContext()
            {
                Controller = controller,
                //  RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };
            return controller;
        }

        Employees GetEmp(int id,string Name,int DeptId)
        {
          return new Employees
            {
                Id = id,
                Name = Name,
                DepartmentId = DeptId
                
            };
       
        }


        [TestMethod]
        public void Test_ToRetrievesAllEmpFromRepository()
        {
            // Arrange
            InmemoryEmployeesRepository  emprepository = new InmemoryEmployeesRepository();     
            InmemoryDepartmentRepository deptRepository=new InmemoryDepartmentRepository();
            Employees employee1 = GetEmp(217,"vishal", 93);
            Employees employee2 = GetEmp(218, "vishal", 93);
            Employees employee3 = GetEmp(219, "vishal", 93);
            emprepository.CreateEmployee(employee1);
            emprepository.CreateEmployee(employee2);
            emprepository.CreateEmployee(employee3);
            var controller = GetHomeController(emprepository, deptRepository);
            // Act
            var result = controller.GetAllEmployee();
            string res = JsonConvert.SerializeObject(result);
            JavaScriptSerializer se=new JavaScriptSerializer();
            IEnumerable<Employees> emp = JsonConvert.DeserializeObject<IEnumerable<Employees>>(res);

            // Assert 
           // IEnumerable<Employees> emps =JsonConvert.DeserializeObject<IEnumerable<Employees>>(test);
            Assert.IsTrue(emp.Contains(employee1));
         
        }



   //Test For checking whether an Employee is created properly or not
        [TestMethod]
        public void Test_CreateEmployee()
        {
            // Arrange
            Employees employee1 = GetEmp(217,"Vishal",93);

            InmemoryEmployeesRepository emprepository = new InmemoryEmployeesRepository();
            InmemoryDepartmentRepository deptRepository = new InmemoryDepartmentRepository();
    
            var controller = GetHomeController(emprepository,deptRepository);
          
            // Act
            controller.CreateEmployee(employee1);
          
            // Assert
          
            IEnumerable<Employees> employees = emprepository.GetAllEmployees();
            Assert.IsTrue(employees.Contains(employee1));
           
        }

    }
}
