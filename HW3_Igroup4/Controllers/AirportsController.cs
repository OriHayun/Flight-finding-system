using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HW3_Igroup4.Models;

namespace HW3_Igroup4.Controllers
{
    public class AirportsController : ApiController
    {
        // GET api/<controller>
        public List<string> Get()
        {
            Airport name = new Airport();
            List<string> allNames = new List<string>();
            allNames = name.getAllAirportsNames();
            return allNames;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        [Route("api/Airports/code/{name}")]
        // GET api/Flight/code/tel aviv israel
        public string Get(string name)
        {
            Airport codeOfAirport = new Airport();
            string airportCode = codeOfAirport.getAirportCode(name);
            return airportCode;
            
        }

        // POST api/<controller>
        public void Post([FromBody] Airport [] airportArr)
        {
            
            for (int i = 0; i < airportArr.Count(); i++)
            {
                airportArr[i].insert();
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}