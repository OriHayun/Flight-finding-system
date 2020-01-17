using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HW3_Igroup4.Controllers;

namespace HW3_Igroup4.Models
{
    public class order
    {

        string flightDate;
        string mainMail;
        string airline;
        string cityFrom;
        string cityTo;
        string passngerNames;

        public string FlightDate { get; set; }
        public string MainMail { get; set; }
        public string PassngerNames { get; set; }
        public string Airline { get; set; }
        public string CityFrom { get; set; }
        public string  CityTo { get; set; }

        public List<order> getOrderTable()
        {
            DBservices dbs = new DBservices();
            return dbs.getOrderTable();
        }


        public int addOrder(order o)
        {
            DBservices dbs = new DBservices();
            int numEffected = dbs.addOrer(o);
            return numEffected;
        }
    }
}