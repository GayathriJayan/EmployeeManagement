using EmployeeManagement.API.Models;
using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.Application.Services;
using EmployeeManagement.DataAccess.Models;
using EmployeeManagement.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeApiController(IEmployeeService employeeService)
        {
           _employeeService = employeeService;
        }

        [HttpGet("get-all")]
    
        public IActionResult GetEmployees()
        {
            try
            
            {
                var getEmployee = _employeeService.GetEmployees();
                return Ok(getEmployee);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
           
        }

        [HttpGet("employees/{Id}")]
  
        public IActionResult GetEmployeeById([FromRoute] int Id)
        
        {
            try
            {             
                var employeeById = _employeeService.GetEmployeeById(Id);
                var employeeDetail = new EmployeeDto()
                {
                    Id = employeeById.Id,
                    Name = employeeById.Name,
                    Department = employeeById.Department,
                    Age = employeeById.Age,
                    Address = employeeById.Address
                };
                return Ok(employeeDetail);
               
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpPost("employee")]
        public IActionResult InsertEmployee([FromBody] Models.EmployeeDetailedViewModel employeeDetailedView)
        {
            try
            {
                var employeeDetail = new EmployeeDto()
                {
                    Id= employeeDetailedView.Id,
                    Name = employeeDetailedView.Name,
                    Department = employeeDetailedView.Department,
                    Age = employeeDetailedView.Age,
                    Address = employeeDetailedView.Address,
                };
                var insertEmployee = _employeeService.InsertEmployee(employeeDetail);
                return Ok(insertEmployee);
            }
            catch(Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }
        [HttpPut("employee-update")]     
        public IActionResult UpdateEmployee([FromBody] Models.EmployeeDetailedViewModel employeeDetailedView)
        {
            try
            {
                var employeeDetail = new EmployeeDto()
                {
                   Id = employeeDetailedView.Id,
                   Name = employeeDetailedView.Name,
                   Department = employeeDetailedView.Department,
                   Age = employeeDetailedView.Age,
                   Address = employeeDetailedView.Address
                };
                var updateEmployee = _employeeService.UpdateEmployee(employeeDetail);
                return Ok(updateEmployee);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }
        [HttpDelete("employees/{id}")]
      
        public IActionResult DeleteEmployee([FromRoute] int id)
        {
            try
            {
                var deleteEmployee = _employeeService.DeleteEmployee(id);
                return Ok(deleteEmployee);

            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }
    }
}
