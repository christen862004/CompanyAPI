using CompanyAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]//api/bind
    [ApiController] //change binding behaviour
    public class BindController : ControllerBase
    {
        //[HttpPost]//api/bind
        //public IActionResult Add([FromBody]string name)//<==  {}
        //{
        //    return Ok(name);
        //}

        [HttpGet] //reqquet with no body
        //api/bind?lat=233234&lang=45654   [FromQuery]
        //[HttpGet("{Lat}/{Lag}")] //reqquet with no body
        //api/bind/23234/45654   [FromRoute]
        public IActionResult GetPosition([FromRoute]Location loc)
        {
            return Ok(loc);
        }


        //endpoint take premitive
        //api/bind?id=1&name=ahmed   get  (Query string)
        //api/bind/1/ahmedd
        //api/bind/s/aballah
        //[HttpGet("{id:int}/{name:alpha}")]
        //public IActionResult show(int id,string name)//Query string
        //{
        //    return Ok($"id={id} \t name={name}");
        //}
        //[HttpPost]
        //public IActionResult add(Department dept,string Name)
        //{
        //    return Ok();
        //}

    }
}
