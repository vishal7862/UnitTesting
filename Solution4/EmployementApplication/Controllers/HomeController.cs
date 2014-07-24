using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using EmployementApplication.Models;
using Newtonsoft.Json;
using WebGrease.Css.Ast.Selectors;

namespace EmployementApplication.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
    
            IEmployeesRepository _employeesRepository;
            IDepartmentsRepository _departmentsRepository;
            public HomeController() : this(new EFEmployeesRepository(),new EFDepartmentsRepository()) { }
            public HomeController(IEmployeesRepository employeesRepository,IDepartmentsRepository departmentsRepository)
            {
                _employeesRepository = employeesRepository;
                _departmentsRepository = departmentsRepository;
            }

            public ViewResult Index()
            {
                return View();
            }


            [HttpPost]
            public JsonResult  CreateEmployee(Employees employee)
            {

                 _employeesRepository.CreateEmployee(employee);
                 string res = JsonConvert.SerializeObject(employee);
                 return Json(res);

            }

            [HttpGet]
            public JsonResult GetAllEmployee()
            {

                IEnumerable<Employees> employee = _employeesRepository.GetAllEmployees();

                string res = JsonConvert.SerializeObject(employee);

                return Json(res, JsonRequestBehavior.AllowGet);


            }


            [HttpGet]
            public JsonResult GetEmpById(int id)
            {

                Employees employee = _employeesRepository.GetAllEmployees().Single(e => e.Id==id);
                string res = JsonConvert.SerializeObject(employee);
                return Json(res, JsonRequestBehavior.AllowGet);

            }

            [HttpPost]
            public JsonResult EditEmp(Employees emp)
            {
                _employeesRepository.Edit(emp);
                string res = JsonConvert.SerializeObject(emp);
                return Json(res);
            }

            [HttpGet]
            public JsonResult LoadDropDownList()
            {
                var dept = _departmentsRepository.GetAllDepartments();

                string res = JsonConvert.SerializeObject(dept, Formatting.Indented, new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });

                return Json(res, JsonRequestBehavior.AllowGet);
            }



            [HttpPost]
            public JsonResult DeleteEmp(int id)
            {
                Employees emp = _employeesRepository.GetAllEmployees().Single(e => e.Id == id);
               _employeesRepository.DeleteEmp(id);
               string res = JsonConvert.SerializeObject(emp);
               return Json(res);

            }

      


        ////---------------------------------------------------------------------departmentcode begins------------------------------------------------

            public JsonResult CreateDepartment(Departments department)
            {

               _departmentsRepository.CreateDepartment(department);
               string res = JsonConvert.SerializeObject(department);
               return Json(res);
                
            }


            //public JsonResult GetAllDepartments()
            //{

            //    IEnumerable<Departments> departments = _departmentsRepository.GetAllDepartments();

            //    string res = JsonConvert.SerializeObject(departments);

            //    return Json(res, JsonRequestBehavior.AllowGet);


            //}

            [HttpGet]
            public JsonResult GetAllEmployeesByDepartments()
            {

            var employees = from department in _departmentsRepository.GetAllDepartments()
                            join employee in _employeesRepository.GetAllEmployees()
                                on department.Id equals employee.DepartmentId
                                into employeeGroup
                            select new DepartmentTotal { Id = department.Id, Name = department.Name, Total = employeeGroup.Count() };

            string res = JsonConvert.SerializeObject(employees);

                return Json(res, JsonRequestBehavior.AllowGet);
            }

         [HttpPost]
         public JsonResult DeleteDept(int id)
          {
            Departments dept = _departmentsRepository.GetAllDepartments().Single(e=>e.Id==id);
           _departmentsRepository.Delete(id);
            string res = JsonConvert.SerializeObject(dept);
            return Json(res);
          }

        [HttpPost]
        public JsonResult EditDept(Departments dept)
        {
            _departmentsRepository.EditDept(dept);
            string res = JsonConvert.SerializeObject(dept);
            return Json(res);
        }

        public JsonResult GetDeptById(int id)
            {

                Departments department = _departmentsRepository.GetAllDepartments().Single(dept => dept.Id==id);
                string res = JsonConvert.SerializeObject(department, Formatting.Indented,
                                                                                        new JsonSerializerSettings
                                                                                        {
                                                                                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                                                                        });
                return Json(res, JsonRequestBehavior.AllowGet);
            }
    }
}