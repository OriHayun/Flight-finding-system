using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HW3_Igroup4.Models;

namespace HW3_Igroup4.Controllers
{
    public class adminController : ApiController
    {
        // GET api/<controller>
        public List<Admin> Get()
        {
            Admin a = new Admin();
            List<Admin> aList = a.logIn();
            return aList;
            //return null;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public int Post([FromBody]Admin a)
        {
            int numEffected = a.addNewAdmin(a);
            return numEffected;
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