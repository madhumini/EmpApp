using Employee_Information.Models;
using Employee_Information.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Employee_Information.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class EmployeesController : ControllerBase
    {
        readonly IDataAccess _dataAccess;
        public EmployeesController(IDataAccess dataAccess, IConfiguration configuration)
        {
            this._dataAccess = dataAccess;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _dataAccess.GetAllEmployees();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            _dataAccess.InsertEmployee(employee);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Employee employee)
        {
            _dataAccess.UpdateEmployee(employee);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dataAccess.DeleteEmployee(id);
            return Ok();
        }

        [HttpGet("average-salary")]
        public IActionResult GetAverageSalary()
        {
            return Ok(_dataAccess.GetAverageSalary());
        }
    }

}
