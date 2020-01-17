using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HW3_Igroup4.Models;

namespace HW3_Igroup4.Controllers
{
    public class orderController : ApiController
    {
        // GET api/<controller>
        public List<order> Get()
        {
            order o = new order();
            return o.getOrderTable();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public int Post([FromBody]order o)
        {
            int numEffcted = o.addOrder(o);
            return numEffcted;
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