using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Models.Provider;
using EmployeeManagement.UI.Providers.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace EmployeeManagement.UI.Providers.ApiClients
{
    public class EmployeeApiClient : IEmployeeApiClient
    {
        private readonly HttpClient _httpClient;
        private HttpContent stringContent;

    public EmployeeApiClient(HttpClient httpClient)
    {
         _httpClient = httpClient;
    }
    public void UpdateAndInsertResponse(EmployeeDetailedViewModel employeeDetailed)
    {
         var stringContent = new StringContent(JsonConvert.SerializeObject(employeeDetailed));
         using (var response = _httpClient.PostAsync("https://localhost:5001/api/employee", stringContent).Result);       
    }

    public IEnumerable<EmployeeViewModel> GetAllEmployee()
        {
            
            using var response = _httpClient.GetAsync("https://localhost:5001/api/get-all").Result;
            var employee = JsonConvert.DeserializeObject<IEnumerable<EmployeeViewModel>>
                           (response.Content.ReadAsStringAsync().Result);
            return employee;
        }
        public EmployeeDetailedViewModel GetEmployeeById(int id)
        {
          
            using var response = _httpClient.GetAsync("https://localhost:5001/api/employees/"+ id).Result;
            var employee = JsonConvert.DeserializeObject<EmployeeDetailedViewModel>
                           (response.Content.ReadAsStringAsync().Result);
            return employee;

        }
        public bool InsertEmployee(EmployeeDetailedViewModel employeeDetailed)
        {
            UpdateAndInsertResponse(employeeDetailed);
            return true;
        }
        public bool UpdateEmployee(EmployeeDetailedViewModel employeeDetailed)
        {
            UpdateAndInsertResponse(employeeDetailed);
            return true;
        }
        public bool DeleteEmployee(int id)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(id));
            using (var response=_httpClient.DeleteAsync("https://localhost:5001/api/employees/"+id).Result)
            {
                return true;
            }
        }
 
    }
}
