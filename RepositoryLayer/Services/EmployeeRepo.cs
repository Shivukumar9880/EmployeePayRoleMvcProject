using CommonLayer.models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class EmployeeRepo : IEmployeeRepo
    {

       
        string connectionString = @"Data Source=ZEROBOOK\SQLEXPRESS;Initial Catalog=EmployeePayRoleMvc;Integrated Security=True;Encrypt=False";

        public EmployeeModel AddEmployee(EmployeeModel employee)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                SqlCommand cmd = new SqlCommand("InsertEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@ImagePath", employee.ImagePath);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                cmd.Parameters.AddWithValue("@Notes", employee.Notes);

                cmd.ExecuteNonQuery();

                connection.Close();

            }
            return employee;

        }

        public EmployeeEntity DeleteEmployeeById(int employeeId)
        {
           using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DeleteEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                   var result=  GetEmployeeById(employeeId);
                cmd.ExecuteNonQuery();
                connection.Close();
                return result;

            }
            return null;
        }

      

        public EmployeeEntity GetEmployeeById(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetEmployeeById", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add a parameter for the employeeId
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                // Execute the reader to retrieve data
                SqlDataReader rdr = cmd.ExecuteReader();

                // Check if there are rows returned
                if (rdr.Read())
                {
                    // Create an EmployeeModel object and populate it with data from the reader
                    EmployeeEntity employee = new EmployeeEntity
                    {
                        EmployeeId = Convert.ToInt32(rdr["EmployeeId"]),
                        FullName = rdr["FullName"].ToString(),
                        ImagePath = rdr["ImagePath"].ToString(),
                        Gender = rdr["Gender"].ToString(),
                        Department = rdr["Department"].ToString(),
                        Salary = Convert.ToDecimal(rdr["Salary"]),
                        StartDate = Convert.ToDateTime(rdr["StartDate"]),
                        Notes = rdr["Notes"].ToString()
                      
                    };

                    // Close the reader before returning the result
                    rdr.Close();

                    // Close the connection
                    connection.Close();

                    return employee;
                }

                // If no rows are returned, close the reader and connection and return null
                rdr.Close();
                connection.Close();
                return null;
            }



        }

        public List<EmployeeEntity> GetAllEmployees()
        {   List<EmployeeEntity> empList = new List<EmployeeEntity>();
            using(SqlConnection connection = new SqlConnection( connectionString ))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("GetAllEmployees", connection);
                SqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    EmployeeEntity employee = new EmployeeEntity()
                    {
                        EmployeeId = Convert.ToInt32(rdr["EmployeeId"]),
                        FullName = rdr["FullName"].ToString(),
                        ImagePath = rdr["ImagePath"].ToString(),
                        Gender = rdr["Gender"].ToString(),
                        Department = rdr["Department"].ToString(),
                        Salary = Convert.ToDecimal(rdr["Salary"]),
                        StartDate = Convert.ToDateTime(rdr["StartDate"]),
                        Notes = rdr["Notes"].ToString()
                        
                    };
                    empList.Add(employee);
                }
                rdr.Close();
                connection.Close();
                return empList;
            }
            return empList;     
        }

        public EmployeeEntity UpdateEmployee(EmployeeEntity employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@ImagePath", employee.ImagePath);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                //  cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                DateTime startDate = (employee.StartDate >= SqlDateTime.MinValue.Value && employee.StartDate <= SqlDateTime.MaxValue.Value)
                              ? employee.StartDate
                              : SqlDateTime.MinValue.Value;
                cmd.Parameters.AddWithValue("@StartDate", startDate);

                cmd.Parameters.AddWithValue("@Notes", employee.Notes);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            return employee;
        }

        public EmployeeEntity EmployeeLogin(LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return null;
            }
            if (loginModel.EmployeeId <= 0 || string.IsNullOrEmpty(loginModel.FullName))
            {
                return null;
            }
            EmployeeEntity employee = GetEmployeeById(loginModel.EmployeeId);
            if (employee == null)
            {
                return null;
            }
            if(!employee.FullName.Equals(loginModel.FullName))
            {
                return null;
            }

            return employee;
        }

        public List<EmployeeEntity> GetAllEmployeeByName(string name)
        {

            List<EmployeeEntity> emplist= new List<EmployeeEntity>();
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employee WHERE FullName = @name", Connection);
                cmd.Parameters.AddWithValue("@name", name);
                SqlDataReader rdr =cmd.ExecuteReader();

                while (rdr.Read())
                {
                    EmployeeEntity employee = new EmployeeEntity()
                    {
                        EmployeeId = Convert.ToInt32(rdr["EmployeeId"]),
                        FullName = rdr["FullName"].ToString(),
                        ImagePath = rdr["ImagePath"].ToString(),
                        Gender = rdr["Gender"].ToString(),
                        Department = rdr["Department"].ToString(),
                        Salary = Convert.ToDecimal(rdr["Salary"]),
                        StartDate = Convert.ToDateTime(rdr["StartDate"]),
                        Notes = rdr["Notes"].ToString()

                    };
                    emplist.Add(employee);

                }
                rdr.Close();
                Connection.Close();
                return emplist;
            }
            return emplist;
        }
    }
}
