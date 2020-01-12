using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HW3_Igroup4.Controllers;

namespace HW3_Igroup4.Models
{
    public class Airport
    {

        public int Id { get; set; }
        public string AirportCode { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string AirportName { get; set; }

        public int insert()
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insert(this);
            return numAffected;
        }

        public List<string> getAllAirportsNames()
        {
            DBservices dbs1 = new DBservices();
            return dbs1.getAllAirportsNames();
        }

        public string getAirportCode(string name)
        {
            DBservices dbs2 = new DBservices();
            string airportCode = dbs2.getAirportCode(name);
            return airportCode;
        }
    }
}