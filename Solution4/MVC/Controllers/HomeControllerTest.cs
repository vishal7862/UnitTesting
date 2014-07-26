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

using MVC.Models;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using NUnit.Core;
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


        //Test for Employee Actions

        Employees GetEmp(int id, string Name, int DeptId)
        {
            return new Employees
              {
                  Id = id,
                  Name = Name,
                  DepartmentId = DeptId

              };

        }


        // Test For Retrieving EmpData
        [TestMethod]
        public void Test_ToRetrievesAllEmpFromRepository()
        {

            // Arrange
            InmemoryEmployeesRepository emprepository = new InmemoryEmployeesRepository();
            InmemoryDepartmentRepository deptRepository = new InmemoryDepartmentRepository();

            //populating the list of employees
            Employees employee1 = GetEmp(217, "vishal", 93);
            Employees employee2 = GetEmp(218, "vishal", 93);
            Employees employee3 = GetEmp(219, "vishal", 93);

            emprepository.CreateEmployee(employee1);
            emprepository.CreateEmployee(employee2);
            emprepository.CreateEmployee(employee3);

            var controller = GetHomeController(emprepository, deptRepository);

            // Act
            var actual = controller.GetAllEmployee();
            var strActual = actual.Data;

            // Assert
            Assert.IsNotNull(actual);

            IEnumerable<Employees> employees = emprepository.GetAllEmployees();
            JavaScriptSerializer se = new JavaScriptSerializer();
            string jsonStr = se.Serialize(employees);
            Assert.AreEqual(jsonStr, strActual);


            Employees emp = emprepository.GetAllEmployees().Single(e => e.Id == 217);
            string jsonEmpStr = se.Serialize(emp);
            Assert.IsTrue(jsonStr.Contains(jsonEmpStr));
        }



        //Tests For Create Employee
  
    
        [TestMethod]
        public void Test_CreateEmployee_CheckingValidEntryIsCreatedIntoRepository()
        {
            // Arrange
            Employees employee1 = GetEmp(1, "vishal", 93);

            InmemoryEmployeesRepository emprepository = new InmemoryEmployeesRepository();
            InmemoryDepartmentRepository deptRepository = new InmemoryDepartmentRepository();


            var controller = GetHomeController(emprepository, deptRepository);


            // Act
            controller.CreateEmployee(employee1);


            // Assert

            IEnumerable<Employees> employees = emprepository.GetAllEmployees();
            Assert.IsTrue(employees.Contains(employee1));
        }

        // Test For DeleteEmployee
        [TestMethod]
        public void Test_DeleteEmployee_CheckingEmployeeGettingDeleted()
        {
            // Arrange
            InmemoryEmployeesRepository emprepository = new InmemoryEmployeesRepository();
            InmemoryDepartmentRepository deptRepository = new InmemoryDepartmentRepository();

            Employees employee1 = GetEmp(1, "vishal", 93);

            //populating the list of employees
            emprepository.CreateEmployee(employee1);


            //initalizing controller
            var controller = GetHomeController(emprepository, deptRepository);

            // Act
            var actual = controller.DeleteEmp(employee1.Id);


            // Assert
            //Assert.IsNotNull(actual);

            IEnumerable<Employees> employees = emprepository.GetAllEmployees();
            Assert.IsFalse(employees.Contains(employee1));
        }

        // Test For EditEmp
        [TestMethod]
        public void Test_EditEmployee_CheckingEmployeeGettingEdited()
        {
            // Arrange
            InmemoryEmployeesRepository emprepository = new InmemoryEmployeesRepository();
            InmemoryDepartmentRepository deptRepository = new InmemoryDepartmentRepository();

            Employees employee1 = GetEmp(1, "vishal", 93);
            Employees employee2 = GetEmp(2, "abc", 93);
            Employees employee3 = GetEmp(3, "pqr", 93);

            //populating the list of employees
            emprepository.CreateEmployee(employee1);
            emprepository.CreateEmployee(employee2);
            emprepository.CreateEmployee(employee3);

            employee1.Name = "Test1";

            var controller = GetHomeController(emprepository, deptRepository);

            // Act
            var actual = controller.EditEmp(employee1);


            // Assert
            if (employee1.Id < 0)
            {
                Assert.Fail();
            }
            IEnumerable<Employees> employees = emprepository.GetAllEmployees();
            var initialEmp = employees.Where(x => x.Id == 1).First();
            Assert.AreEqual(employee1.Name, initialEmp.Name);
            // Assert.IsInstanceOfType(actual, typeof(JsonResult));
        }


        //Test for Department Actions

        Departments GetDept(int id, string Name)
        {
            return new Departments
            {
                Id = id,
                Name = Name,
            };

        }


        // Test For Retrieving DeptData
        [TestMethod]        
        public void Test_ToRetrievesAllDeptFromRepository()
        {

            // Arrange
            InmemoryEmployeesRepository emprepository = new InmemoryEmployeesRepository();
            InmemoryDepartmentRepository deptRepository = new InmemoryDepartmentRepository();

            //populating the list of department
            Departments dept1 = GetDept(1, "CE");
            Departments dept2 = GetDept(2, "IT");
            Departments dept3 = GetDept(3, "Electrical");

            deptRepository.CreateDepartment(dept1);
            deptRepository.CreateDepartment(dept2);
            deptRepository.CreateDepartment(dept3);

            //populating the list of employees
            Employees employee1 = GetEmp(217, "vishal", 1);
            Employees employee2 = GetEmp(218, "Abc", 1);
            Employees employee3 = GetEmp(219, "Pqr", 2);

            emprepository.CreateEmployee(employee1);
            emprepository.CreateEmployee(employee2);
            emprepository.CreateEmployee(employee3);

            var controller = GetHomeController(emprepository, deptRepository);

            // Act
            var actual = controller.GetAllEmployeesByDepartments();
            var strActual = actual.Data;

            // Assert 
            var employeesBydepartment = from department in deptRepository.GetAllDepartments()
                                        join employee in emprepository.GetAllEmployees()
                                            on department.Id equals employee.DepartmentId
                                            into employeeGroup
                                        select new DepartmentTotal { Id = department.Id, Name = department.Name, Total = employeeGroup.Count() };

            JavaScriptSerializer se = new JavaScriptSerializer();
            string jsonStr = se.Serialize(employeesBydepartment);

            Assert.IsNotNull(actual);
            Assert.AreEqual(jsonStr, strActual);            
  
        }



        //Tests For Create Department
        

        [TestMethod]
        public void Test_CreateDepartment_CheckingValidEntryIsCreatedIntoRepository()
        {
            // Arrange
            Departments dept1 = GetDept(1, "CE");

            InmemoryEmployeesRepository emprepository = new InmemoryEmployeesRepository();
            InmemoryDepartmentRepository deptRepository = new InmemoryDepartmentRepository();


            var controller = GetHomeController(emprepository, deptRepository);


            // Act
            controller.CreateDepartment(dept1);


            // Assert
            IEnumerable<Departments> departments = deptRepository.GetAllDepartments();
            Assert.IsTrue(departments.Contains(dept1));
        }

        // Test For DeleteEmployee
        [TestMethod]
        public void Test_DeleteEmployee_CheckingDepartmentGettingDeleted()
        {
            // Arrange
            InmemoryEmployeesRepository emprepository = new InmemoryEmployeesRepository();
            InmemoryDepartmentRepository deptRepository = new InmemoryDepartmentRepository();

            Departments dept1 = GetDept(1, "CE");

            //populating the list of employees
            deptRepository.CreateDepartment(dept1);


            //initalizing controller
            var controller = GetHomeController(emprepository, deptRepository);

            // Act
            var actual = controller.DeleteDept(dept1.Id);


            // Assert
            Assert.IsNotNull(actual);

            IEnumerable<Departments> departments = deptRepository.GetAllDepartments();
            Assert.IsFalse(departments.Contains(dept1));
        }

        // Test For EditEmp
        [TestMethod]
        public void Test_EditDepartment_CheckingDepartmentGettingEdited()
        {
            // Arrange
            InmemoryEmployeesRepository emprepository = new InmemoryEmployeesRepository();
            InmemoryDepartmentRepository deptRepository = new InmemoryDepartmentRepository();

            Departments dept1 = GetDept(1, "CE");
            Departments dept2 = GetDept(2, "IT");
            Departments dept3 = GetDept(3, "Electrical");
         

            //populating the list of employees
            deptRepository.CreateDepartment(dept1);
            deptRepository.CreateDepartment(dept2);
            deptRepository.CreateDepartment(dept3);

            var controller = GetHomeController(emprepository, deptRepository);

            // Act
            var actual = controller.EditDept(dept1);
            dept1.Name = "abc";

            // Assert
            if (dept1.Id < 0)
            {
                Assert.Fail();
            }
             IEnumerable<Departments> departments = deptRepository.GetAllDepartments();
             var initialDept = departments.Where(x => x.Id == 1).First();
             Assert.AreEqual(dept1.Name, initialDept.Name);
            // Assert.IsInstanceOfType(actual, typeof(JsonResult));
        }



    }
}
