using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudeOperations.Models;

namespace CrudeOperations.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeModel employeeModel = new EmployeeModel();
        
        // GET: Employee
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(EmployeeModel empModel)
        {
            if(ModelState.IsValid)
            {
              int id =  employeeModel.AddEmployee(empModel);
                ViewBag.Success = "Employee Added Successfully";
            }
            return View();
        }

        public ActionResult GetAllEmployeeDetail()
        {
            var result = employeeModel.GetAllEmployeeDetails();
            return View(result);
        }

        public ActionResult UpdateEmployeeDetails(int id)
        {
            var details = employeeModel.GetEmployeeDetails(id);            
            return View(details);
        }

        [HttpPost]
        public ActionResult UpdateEmployeeDetails(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                var details = employeeModel.UpdateEmployeeDetails(model.EmpId, model);
                return RedirectToAction("GetAllEmployeeDetail");
            }
            return View();
        }

        public ActionResult DeleteEmployee(int id)
        {
            bool result = employeeModel.DeleteEmployee(id);
            if(result == true)
            {
                return RedirectToAction("GetAllEmployeeDetail");

            }
            return View();
        }

    }
}