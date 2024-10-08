using Api.Day1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Day1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        CompanyContext dbcontext=new CompanyContext();

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(dbcontext.Employees.ToList());
        }
        //[HttpGet("{id}")]
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetById(int id)
        {
            var employee = dbcontext.Employees.Find(id);
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPost]
        public ActionResult Add(Employee employee)
        {
            dbcontext.Add(employee);
            dbcontext.SaveChanges();
            return CreatedAtAction("GetById",
                new { id = employee.Id },
                new { message = "successfully created" });
        }
        [HttpPut]
        [Route("{id}")]
        public ActionResult Edit(int id,Employee employeeFromUser)
        {
            if(id != employeeFromUser.Id)
            {
                return BadRequest();
            }
            var empFromDb=dbcontext.Employees.Find(id); 
            if(empFromDb == null)
            {
                return NotFound();
            }
            empFromDb.Age = employeeFromUser.Age;
            empFromDb.Salary= employeeFromUser.Salary;
            empFromDb.Name=employeeFromUser.Name;
            dbcontext.SaveChanges();

            return NoContent();
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var empFromDb = dbcontext.Employees.Find(id);
            if (empFromDb == null)
            {
                return NotFound();
            }
            dbcontext.Remove(empFromDb);
            dbcontext.SaveChanges();
            return NoContent();
        }
    }
}
