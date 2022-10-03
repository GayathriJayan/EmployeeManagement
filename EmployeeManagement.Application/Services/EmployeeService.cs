using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using EmployeeManagement.DataAccess.Repository;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }
        public IEnumerable<EmployeeDto> GetEmployees()
        {
            var getEmployee = _employeeRepository.GetEmployees();
            var employeesDto = new List<EmployeeDto>();
            foreach (var employee in getEmployee)
            {
                var employeeDto = new EmployeeDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Department = employee.Department,
                    Address = employee.Address

                };
                employeesDto.Add(employeeDto);
            }
            return employeesDto;
            
        }
        public EmployeeDto GetEmployeeById(int id)
        {
            var getEmployeeById = _employeeRepository.GetEmployeeById(id);
            var employees = new EmployeeDto()
            {

                Id = getEmployeeById.Id,
                Name = getEmployeeById.Name,
                Age = getEmployeeById.Age,
                Department = getEmployeeById.Department,
                Address = getEmployeeById.Address

            };
            return employees;
        }
        public bool InsertEmployee(EmployeeDto employees)
        {
            try
            {             
                var employeData = new EmployeeData()
                {
                    Name = employees.Name,
                    Age = employees.Age,
                    Department= employees.Department,
                    Address = employees.Address
                };
                _employeeRepository.InsertEmployee(employeData);
                return true;
                
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateEmployee(EmployeeDto employees)
        {
            try
            {
                var employeData = new EmployeeData()
                {
                    Id = employees.Id,
                    Name = employees.Name,
                    Age = employees.Age,
                    Department = employees.Department,
                    Address = employees.Address

                };
              
                _employeeRepository.UpdateEmployee(employeData);
                
                return true;
            }
            catch
            {
                throw;
            }
        }
        public bool DeleteEmployee(int id)
        {
            var getEmployeeById = _employeeRepository.DeleteEmployee(id);
            return true;
        }  
    }
}
