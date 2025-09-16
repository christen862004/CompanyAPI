using CompanyAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]//api/department
    [ApiController]//bind + valiadtion
    public class DepartmentController : ControllerBase
    {
        CompanyContext companyContext;
        public DepartmentController()
        {
            companyContext = new CompanyContext();
        }
        //api/department  [get]
        [HttpGet]
        public IActionResult showAll()//get | delete with no body
        {
            List<Department> deptList= companyContext.Department.ToList();
            return Ok(deptList);//httpresponse with status code 200 :reposnse body (List<department>)
        }

        [HttpGet]
        [Route("{id:int}")]//api/epartment/2
        //http://localhost:5084/api/Department/OS
        //api/department/1
        public IActionResult Details(int id)
        {
            Department dept= companyContext.Department.FirstOrDefault(d => d.Id == id);
            if (dept != null)
                return Ok(dept);
            return BadRequest("Not Found");
        }

        [HttpGet("{name:alpha}")]//api/department/frontend
        public IActionResult GetDetailsByName(string name)
        {
            Department dept = companyContext.Department.FirstOrDefault(d => d.Name == name);
            if (dept != null)
                return Ok(dept);
            return BadRequest("Not Found");
        }
        

        //----------------------------------
        //api/department  [post]
        [HttpPost]
        public IActionResult addept(Department dept) //request body as json serialization
        {
            if (ModelState.IsValid)
            {
                companyContext.Department.Add(dept);
                companyContext.SaveChanges();
                //return Created($"http://localhost:5084/api/department/{dept.Id}",dept);//getbyidd
                return CreatedAtAction("Details", new { id = dept.Id }, dept);
            }
            return BadRequest(ModelState);
        }
        
        [HttpPut]
        public IActionResult Edit(Department dept) {
            if (ModelState.IsValid)
            {
                //find by id
                Department depFRomDB = companyContext.Department.FirstOrDefault(d=>d.Id==dept.Id);

                //change
                depFRomDB.Name=dept.Name;
                depFRomDB.ManagerName=dept.ManagerName;
                //savechange
                companyContext.SaveChanges();
                //  return Ok("Update Success");
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        //200 ok
        //204 nocontent
        //201    created
        [HttpDelete("{Id:int}")]//api/department/1   method (delete)
        public IActionResult Delete(int Id)
        {
            Department dept = companyContext.Department.FirstOrDefault(d => d.Id == Id);
            companyContext.Department.Remove(dept);
            companyContext.SaveChanges();
            //return Ok("Delete Success");
            return NoContent();

        }
        //api/department  [put]
        //api/department  [delete]


    }
}
