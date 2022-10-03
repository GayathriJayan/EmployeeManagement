using EmployeeManagement.Application.Contracts;
using EmployeeManagement.DataAccess.Repository;
using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Providers.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.UI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeApiClient _employeeApiClient;

        public EmployeeController(IEmployeeApiClient employeeApiClient)
        {
            this._employeeApiClient = employeeApiClient;
        }
        public IActionResult Index()
        {
            try
            {
                var employees = _employeeApiClient.GetAllEmployee();
                return View(employees);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IActionResult GetEmployeeById(int id)
        {
            try
            {
                var employees = _employeeApiClient.GetEmployeeById(id);
                return View(employees);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IActionResult InsertEmployee(EmployeeDetailedViewModel employee)
        {
            try
            {
                var postEmployee = _employeeApiClient.InsertEmployee(employee);
                return View(postEmployee);
            }
            catch(Exception)
            {
                throw;
            }
           
        }
        public IActionResult UpdateEmployee(EmployeeDetailedViewModel employee)
        {
            try
            {
                var updateEmployee = _employeeApiClient.UpdateEmployee(employee);
                return View(updateEmployee);
            }
            catch(Exception)
            {
                throw;
            }          
        }
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                var deleteEmployee = _employeeApiClient.DeleteEmployee(id);
                return View(deleteEmployee);
            }
            catch
            {
                throw;
            }
           
        }
    }
}
