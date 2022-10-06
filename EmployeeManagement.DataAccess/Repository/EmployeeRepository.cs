using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.DataAccess.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SqlConnection _sqlConnection;
        public EmployeeRepository(String connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }
        public EmployeeRepository()
        {
            _sqlConnection = new SqlConnection("data source=(localdb)\\mssqllocaldb; database=EmployeeManagementDb;");
        }
        public IEnumerable<EmployeeData> GetEmployees()
        {
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand(GetEmployeeQuery.GETEMPLOYEE, _sqlConnection);
                var sqlDataReader = sqlCommand.ExecuteReader();
                var listOfStudent = new List<EmployeeData>();
                while (sqlDataReader.Read())
                {
                    listOfStudent.Add(new EmployeeData()
                    {
                        Id = (int)sqlDataReader["id"],
                        Name = (string)sqlDataReader["name"],
                        Department = (string)sqlDataReader["department"],
                        Age = (int)sqlDataReader["age"],
                        Address = (string)sqlDataReader["address"]
                    });

                }
                return listOfStudent;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
        public EmployeeData GetEmployeeById(int id)
        {
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand(GetEmployeeByIdQuery.GETEMPLOYEEBYID, _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", id);
                var sqlDataReader = sqlCommand.ExecuteReader();
                var employeeById = new EmployeeData();
                while (sqlDataReader.Read())
                {
                    employeeById.Id = (int)sqlDataReader["Id"];
                    employeeById.Name = (string)sqlDataReader["Name"];
                    employeeById.Department = (string)sqlDataReader["Department"];
                    employeeById.Age = (int)sqlDataReader["Age"];
                    employeeById.Address = (string)sqlDataReader["Address"];
                }
                return employeeById;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }

        }
        public bool InsertEmployee(EmployeeData employee)
        {
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand(InsertQuery.INSERTMPLOYEE, _sqlConnection);
                sqlCommand.Parameters.AddWithValue("name", employee.Name);
                sqlCommand.Parameters.AddWithValue("age", employee.Age);
                sqlCommand.Parameters.AddWithValue("department", employee.Department);
                sqlCommand.Parameters.AddWithValue("address", employee.Address);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
        public bool UpdateEmployee(EmployeeData employee)
        {
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand(UpdateQuery.UPDATEEMPLOYEE, _sqlConnection);
                sqlCommand.Parameters.AddWithValue("id", employee.Id);
                sqlCommand.Parameters.AddWithValue("age", employee.Age);
                sqlCommand.Parameters.AddWithValue("name", employee.Name);
                sqlCommand.Parameters.AddWithValue("department", employee.Department);
                sqlCommand.Parameters.AddWithValue("address", employee.Address);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
        public bool DeleteEmployee(int id)
        {
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand(DeleteQuery.DELETEEMPLOYEE, _sqlConnection);
                sqlCommand.Parameters.AddWithValue("id", id);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
    }
    public static class DeleteQuery
    {
        public const  string DELETEEMPLOYEE= "delete from Employee where id=@id";
    }
    public static class UpdateQuery
    {
        public const string UPDATEEMPLOYEE = "update Employee set name= @name,age=@age,department=@department,address=@address where id=@id";
    }
    public static class InsertQuery
    {
        public const string INSERTMPLOYEE = "insert into Employee(name,department,age,address) values(@name,@department,@age,@address)";
    }
    public static class GetEmployeeQuery
    {
        public const string GETEMPLOYEE = "select * from Employee";
    }
    public static class GetEmployeeByIdQuery
    {
        public const string GETEMPLOYEEBYID = "select * from Employee where Id=@id";
    }
}
