using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW3_Igroup4.Models
{
    public class sale
    {
        string airline;
        string cityFrom;
        string cityTo;
        double discount;
        int id;

        public string Airline { get { return airline; } set { airline = value; } }
        public string CityFrom { get { return cityFrom; } set { cityFrom = value; } }
        public string CityTo { get { return cityTo; } set { cityTo = value; } }
        public double Discount { get { return discount; } set { discount = value; } }
        public int Id { get { return id; } set { id = value; } }

        public sale() { }

        public List<sale> getDiscountTable()
        {
            DBservices dbs = new DBservices();
            return dbs.getDiscountTable();
        }

        public List<sale> deleteRow(int id)
        {
            DBservices dbs = new DBservices();
            int numEffected = dbs.deleteRow(id);
            return getDiscountTable();
        }
        
        public List<sale> updateDiscount(int id,double discount)
        {
            DBservices dbs = new DBservices();
            int numEffected = dbs.updateDiscount(id, discount);
            return getDiscountTable();
        }

        public List<sale> insertNewDiscount(sale newDiscount)
        {
            DBservices dbs = new DBservices();
            int numEffected = dbs.insertNewDiscount(newDiscount);
            return getDiscountTable();    
        }

    }
}