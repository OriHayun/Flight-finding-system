using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HW3_Igroup4.Models;

namespace HW3_Igroup4.Controllers
{
    public class salesController : ApiController
    {
        // GET api/<controller>
        public List<sale> Get()
        {
            sale s = new sale();
            return s.getDiscountTable();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public List<sale> Post([FromBody]sale newDiscount)
        {
            return newDiscount.insertNewDiscount(newDiscount);
        }

        // PUT api/<controller>/5
        public List<sale> Put(int id, [FromBody]string value)
        {
            double discount = Convert.ToDouble(value);
            sale s = new sale();
            return s.updateDiscount(id, discount);
        }

        // DELETE api/<controller>/5
        public List<sale> Delete(int id)
        {
            sale s = new sale();
            return s.deleteRow(id);
        }
    }
}