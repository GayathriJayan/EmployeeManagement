using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Models.Provider;
using EmployeeManagement.UI.Providers.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EmployeeManagement.UI.Providers.ApiClients
{
    public class EmployeeApiClient : IEmployeeApiClient
    {
        #region PRIVATE VARIABLES
        private readonly HttpClient _httpClient;
        private HttpContent stringContent;
        private const string _url = "https://localhost:5001/api/";
        #endregion
        #region CTOR
        public EmployeeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        #endregion

        public IEnumerable<EmployeeViewModel> GetAllEmployee()
        {

            using var response = _httpClient.GetAsync(_url + "get-all").Result;
            var employee = JsonConvert.DeserializeObject<IEnumerable<EmployeeViewModel>>
                           (response.Content.ReadAsStringAsync().Result);
            return employee;
        }
        public EmployeeDetailedViewModel GetEmployeeById(int id)
        {

            using var response = _httpClient.GetAsync(_url + "employees/" + id).Result;
            var employee = JsonConvert.DeserializeObject<EmployeeDetailedViewModel>
                           (response.Content.ReadAsStringAsync().Result);
            return employee;

        }
        public bool InsertEmployee(EmployeeDetailedViewModel employeeDetailed)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(employeeDetailed), Encoding.UTF8, "application/json");
            using (var response = _httpClient.PostAsync(_url + "employee", stringContent).Result)
            {
                response.Content.ReadAsStringAsync();
                return true;
            }
        }
        public bool UpdateEmployee(EmployeeDetailedViewModel employeeDetailed)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(employeeDetailed), Encoding.UTF8, "application/json");
            using (var response = _httpClient.PutAsync(_url + "employee-update", stringContent).Result)
            {
                var x = response;
            }
            return true;
        }
        public bool DeleteEmployee(int id)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(id));
            using (var response = _httpClient.DeleteAsync(_url + "employees/" + id).Result)
            {
                return true;
            }
        }

    }
}
