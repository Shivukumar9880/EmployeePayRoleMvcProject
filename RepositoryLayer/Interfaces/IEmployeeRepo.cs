using CommonLayer.models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IEmployeeRepo
    {
        EmployeeModel AddEmployee(EmployeeModel employee);
        EmployeeEntity DeleteEmployeeById(int employeeId);

        EmployeeEntity GetEmployeeById(int employeeId);

        List<EmployeeEntity> GetAllEmployees();

        EmployeeEntity UpdateEmployee(EmployeeEntity employee);

        EmployeeEntity EmployeeLogin(LoginModel loginModel);

        List<EmployeeEntity> GetAllEmployeeByName(string name);

    }
}
