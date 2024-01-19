using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.models;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
namespace EmployeePayRoleMVC_6vi.Controllers
{
    public class EmployeePayRoleController : Controller
    {
        private readonly IEmployeeBusiness employeeBusiness;
        public EmployeePayRoleController(IEmployeeBusiness employeeBusiness)
        {
            this.employeeBusiness = employeeBusiness;
        }



        //get all emplist
        [HttpGet("getAll")]
        public IActionResult GetAllEmployee()
        {

            List<EmployeeEntity> lstEmployee = employeeBusiness.GetAllEmployees();
            return View(lstEmployee);

        }


        [HttpGet]
        [Route("getempbyname/{Name}")]
        public  IActionResult GetAllEMpByName(String Name)
        {
            List<EmployeeEntity> res=employeeBusiness.GetAllEmployeeByName(Name);

            return View(res);
        }


        // for getting form 
        [HttpGet()]
        [Route("begin")]
        public IActionResult Create()
        {

            return View();
        }

        //posting details from form
        [HttpPost]
        [Route("begin")]
        public IActionResult Create([Bind] EmployeeModel employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employeeBusiness.AddEmployee(employee);
                    return RedirectToAction("GetAllEmployee");

                }
                return View(employee);
            }
            catch (Exception ex)
            {

                return View(employee);
            }
        }

        //for getting edit from
        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Update(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeEntity employee = employeeBusiness.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        //for updating edited details from form
        [HttpPost]
        [Route("Edit/{id}")]
        public IActionResult Update(int id, [Bind] EmployeeEntity employee)
        {
            try
            {
                if (id != employee.EmployeeId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {

                    var result=employeeBusiness.UpdateEmployee(employee);
                    return RedirectToAction("GetAllEmployee");//after register it will redirect to getAllEmployee page
                      //return View(result);
                }
                return View();
            }
            catch (Exception ex)
            {
   
                return View(employee);
            }
        }


        //gettig emp by Id

        [HttpGet]
        public IActionResult EmployeeDetails(int id)
        {

            try
            {
                id = (int)HttpContext.Session.GetInt32("EmployeeId");

                if (id == null)
                {
                    return NotFound();
                }
                EmployeeEntity employee = employeeBusiness.GetEmployeeById(id);

                if (employee == null)
                {
                    return NotFound();

                }

                return View(employee);
            }
            catch (Exception ex)
            {
               
                return View();
            }
        }


        //delete emp by id
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {


                if (id == null)
                {
                    return NotFound();
                }
                EmployeeEntity employee = employeeBusiness.GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                
                return RedirectToAction("GetAllEmployee"); 
            }

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                employeeBusiness.DeleteEmployeeById(id);
                return RedirectToAction("GetAllEmployee");
            }
            catch (Exception ex)
            {
             
                return RedirectToAction("GetAllEmployee"); 
            }
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([Bind] LoginModel model)
        {

            try
            {
                var result = employeeBusiness.EmployeeLogin(model);

                if(result != null)
                {

                    HttpContext.Session.SetInt32("EmployeeId", model.EmployeeId);
                    HttpContext.Session.SetString("FullName", model.FullName);
                    // return Ok(result);
                    return RedirectToAction("EmployeeDetails", new {id=model.EmployeeId});
                }
                else
                {
                    return NotFound("Login Failure");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        



    }
}
