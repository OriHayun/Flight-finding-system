using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HW3_Igroup4.Models;

namespace HW3_Igroup4.Controllers
{
    public class FlightController : ApiController
    {
        // GET api/<controller>
        public List<Flight> Get()
        {
            Flight f = new Flight();
            return f.getMyFlight();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        [Route("api/Flight/Stop/{stop}")]
        // GET api/Flight/Stop/BERLIN
        public List<Flight> Get(string stop)
        {
            Flight f = new Flight();
            return f.getMyFlightWithStop(stop);
        }

        // POST api/<controller>
        public int Post([FromBody]Flight flight)
        {
            int numEffected = flight.addFlightAsAPackageFlight(flight);
            return numEffected;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/flightId
        public void Delete([FromBody]string flightId)
        {
            Flight f = new Flight();
            f.removeFlightById(flightId);
        }


    }
}