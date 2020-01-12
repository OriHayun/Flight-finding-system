using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace HW3_Igroup4.Models
{
    public class favorite
    {
        string airline;
        string cityFrom;
        string cityTo;
        int numOfLike;

        public string Airline { get { return airline; } set { airline = value; } }
        public string CityFrom { get { return cityFrom; } set { cityFrom = value; } }
        public string CityTo { get { return cityTo; } set { cityTo = value; } }
        public int NumOfLike { get { return numOfLike; } set { numOfLike = value; } }

        public int addToFavorite(favorite pack)
        {
            DBservices dbs = new DBservices();
            dbs = dbs.readFavoriteTable();
            dbs.dt = addOrIncrease(pack, dbs.dt);
            dbs.update();
            return 0;

        }

        private DataTable addOrIncrease(favorite pack, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string airline = (string)dr["AirLine"];
                string cityFrom = (string)dr["CityFrom"];
                string cityTo = (string)dr["CityTo"];
                if (pack.Airline == airline && pack.CityFrom == cityFrom && pack.CityTo == cityTo)
                {
                    int numOfLike = Convert.ToInt32(dr["NumberOfLike"]);
                    dr["NumberOfLike"] = numOfLike + 1;
                    return dt;
                }
            }
            DBservices dbs1 = new DBservices();
            int numEffected = dbs1.addNewFavorite(pack);

            return dt;
        }

        public List<favorite> getFavoriteTable()
        {
            DBservices dbs = new DBservices();
            return dbs.getFavoriteTable();
        }
    }
}