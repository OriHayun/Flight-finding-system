using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HW3_Igroup4.Controllers;

namespace HW3_Igroup4.Models
{
    public class Flight
    {
        string id;
        string origin;
        string destination;
        float price;
        string date;
        string road;
        string airlines;


        public Flight() { }

        
        public string Id { get { return id; } set { id = value; } }
        public string Origin { get { return origin; } set { origin = value; } }
        public string Destination { get { return destination; } set { destination = value; } }
        public float Price { get { return price; } set { price = value; } }
        public string Date { get { return date; } set { date = value; } }
        public string Road { get { return road; } set { road = value; } }
        public string Airlines { get { return airlines; } set { airlines = value; } }



        public int addFlightAsAPackageFlight(Flight flight)
        {
            DBservices dbs = new DBservices();
            int numEffected = dbs.insertFlightAsAPackageFlight(flight);
            return numEffected;
        }

        public List<Flight> getMyFlight()
        {
            DBservices dbs = new DBservices();
            return dbs.getMyFlight();
        }

        public List<Flight> getMyFlightWithStop(string stop)
        {
            DBservices dbs = new DBservices();
            return dbs.getMyFlightWithStop(stop);
        }

        public int removeFlightById(string flightId)
        {
            DBservices dbs = new DBservices();
            int numEffected = dbs.removeFlightById(flightId);
            return numEffected;
        }
    }
}