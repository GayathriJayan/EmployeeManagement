﻿using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Providers.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.UI.Controllers.InternalAPI
{
    [Route("api/internal/employees")]
    [ApiController]
    public class EmployeeInternalApiController : ControllerBase
    {
        private readonly IEmployeeApiClient _employeeApiClient;

        public EmployeeInternalApiController(IEmployeeApiClient employeeApiClient)
        {
            _employeeApiClient = employeeApiClient;
        }


        [HttpGet("employees")]       
        public IActionResult GetAll()
        {
            try
            {
                var employee = _employeeApiClient.GetAllEmployee();

                return Ok(employee);
            }

            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeeById([FromRoute] int employeeId)
        {
            try
            {
                var employee = _employeeApiClient.GetEmployeeById(employeeId);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        [HttpPost("employees")]      
        public IActionResult InsertEmployee([FromBody] EmployeeDetailedViewModel employeeDetailed)
        {
            try
            {
                var employeeDetails = _employeeApiClient.InsertEmployee(employeeDetailed);
                return Ok(employeeDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("{id}")]       
        public IActionResult UpdateEmployee([FromBody] EmployeeDetailedViewModel employeeDetailed)
        {
            try
            {
                var employeeDetails = _employeeApiClient.UpdateEmployee(employeeDetailed);
                return Ok(employeeDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee([FromRoute] int id)
        {
            try
            {
                var employeeDetails = _employeeApiClient.DeleteEmployee(id);
                return Ok(employeeDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
