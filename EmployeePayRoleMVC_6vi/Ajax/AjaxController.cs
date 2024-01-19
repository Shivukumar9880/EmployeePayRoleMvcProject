using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;

namespace EmployeePayRoleMVC_6vi.Ajax
{
    public class AjaxController : Controller
    {


        private readonly IEmployeeBusiness empBusiness;

        public AjaxController(IEmployeeBusiness empBusiness)
        {
            this.empBusiness = empBusiness;
        }
        public IActionResult Index()
        {
            return View();
        }
     
        public JsonResult GetAllEmployee() 
        { 
            var result = empBusiness.GetAllEmployees();


            return new JsonResult(result);

        }
    }
}
