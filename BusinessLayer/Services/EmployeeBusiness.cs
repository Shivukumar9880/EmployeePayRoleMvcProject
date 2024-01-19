using BusinessLayer.Interfaces;
using CommonLayer.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using RepositoryLayer.Entities;

namespace BusinessLayer.Services
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        private readonly IEmployeeRepo employeeRepo;

        public EmployeeBusiness(IEmployeeRepo employeeRepo)
        {
            this.employeeRepo = employeeRepo;   
        }

        public EmployeeModel AddEmployee(EmployeeModel employee)
        {
            return  employeeRepo.AddEmployee(employee);
        }

        public EmployeeEntity DeleteEmployeeById(int employeeId)
        {
            return employeeRepo.DeleteEmployeeById(employeeId);
        }

        public EmployeeEntity EmployeeLogin(LoginModel loginModel)
        {
           return employeeRepo.EmployeeLogin(loginModel);
        }

        public List<EmployeeEntity> GetAllEmployeeByName(string name)
        {
            return employeeRepo.GetAllEmployeeByName(name);
        }

        public List<EmployeeEntity> GetAllEmployees()
        {
            return  employeeRepo.GetAllEmployees();
        }

        public EmployeeEntity GetEmployeeById(int employeeId)
        {
            return employeeRepo.GetEmployeeById(employeeId);
        }

        public EmployeeEntity UpdateEmployee(EmployeeEntity employee)
        {
           return employeeRepo.UpdateEmployee(employee);
        }
    }
}
