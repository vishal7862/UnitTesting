using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        //static List<Employees> fill()
        //{
        //    List<Employees> emps=new List<Employees>();
        //    emps.Add(new Employees{Id=220,Name = "Abc",DepartmentId = 93});
        //    emps.Add(new Employees { Id = 220, Name = "Abc", DepartmentId = 93 });
        //    emps.Add(new Employees { Id = 221, Name = "Pqr", DepartmentId = 93 }); 
        //    emps.Add(new Employees { Id = 222, Name = "Xyz", DepartmentId = 93 });
        //    return emps;
        //} 

       
        [TestMethod]
        public void Test_ToRetrievesAllEmpFromRepository()
        {
            
            // Arrange
            Employees employee1 = GetEmp(217, "vishal", 93);
            Employees employee2 = GetEmp(218, "vishal", 93);
            Employees employee3 = GetEmp(219, "vishal", 93);

            InmemoryEmployeesRepository  emprepository = new InmemoryEmployeesRepository();     
            InmemoryDepartmentRepository deptRepository=new InmemoryDepartmentRepository();
            emprepository.CreateEmployee(employee1);
            emprepository.CreateEmployee(employee2);
            emprepository.CreateEmployee(employee3);

            var controller = GetHomeController(emprepository, deptRepository);
           
            // Act
            var actual= controller.GetAllEmployee();
            var strActual = actual.Data;
            // Assert 
            IEnumerable<Employees> employees = emprepository.GetAllEmployees();
            JavaScriptSerializer se=new JavaScriptSerializer();
            string jsonStr = se.Serialize(employees);
            
            Assert.IsNotNull(actual);
            Assert.AreEqual(jsonStr, strActual);
           // Assert.AreSame(jsonStr,strActual);  Assertion fails as it is pointing to different objects



        }



   //Test For checking whether an Employee is created properly or not
        [TestMethod]
        public void Test_CreateEmployee()
        {
            // Arrange
            Employees employee1 = GetEmp(217,"Vishal",93);
             Employees employee2 = GetEmp(218, "vishal", 93);
            Employees employee3 = GetEmp(219, "vishal", 93);
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
