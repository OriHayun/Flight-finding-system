using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using HW3_Igroup4.Models;

public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;

    //--------------------------------------------------------------------------------//

    //general

    public DBservices() { }

    private SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

        return cmd;
    }

    //--------------------------------------------------------------------------------//
    //airport

    private String BuildInsertCommand(Airport airport)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values({0},'{1}',{2},{3},'{4}')", airport.Id.ToString(), airport.AirportCode, airport.Latitude.ToString(), airport.Longitude.ToString(), airport.AirportName);
        String prefix = "INSERT INTO airport_2020_ori " + "(Id,AirportCode,Latitude,Longitude,AirportName) ";
        command = prefix + sb.ToString();

        return command;
    }

    public int insert(Airport airport)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("airportDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex); // write to log
        }

        String cStr = BuildInsertCommand(airport);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    public List<string> getAllAirportsNames()
    {
        List<string> airportsNames = new List<string>();
        SqlConnection con = null;
        try
        {
            con = connect("airportDBConnectionString");
            string selectSTR = "SELECT AirportName from airport_2020_ori";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                airportsNames.Add((string)dr["AirportName"]);
            }

            return airportsNames;

        }
        catch (Exception ex)
        {

            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string getAirportCode(string name)
    {
        string code = "";
        SqlConnection con = null;

        try
        {
            con = connect("airportDBConnectionString");
            string selectSTR = "SELECT airportCode from airport_2020_ori where AirportName='" + name + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                code = (string)dr["AirportCode"];
            }
            return code;
        }
        catch (Exception ex)
        {

            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------------------//
    //flight

    private String BuildInsertCommand(Flight flight)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}','{1}','{2}',{3},'{4}','{5}','{6}')", flight.Id, flight.Origin, flight.Destination, flight.Price, flight.Date, flight.Airlines, flight.Road);
        String prefix = "INSERT INTO packageFlight_2020_ori " + "(Id,Origin,Destination,Price,dateFlight,airlines,Road) ";
        command = prefix + sb.ToString();

        return command;
    }

    public int insertFlightAsAPackageFlight(Flight flight)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("airportDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex); // write to log
        }

        String cStr = BuildInsertCommand(flight);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    public List<Flight> getMyFlight()
    {
        List<Flight> myFlight = new List<Flight>();
        SqlConnection con = null;
        try
        {
            con = connect("airportDBConnectionString");
            String selectSTR = "SELECT * FROM packageFlight_2020_ori";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {
                Flight f = new Flight();
                f.Id = (string)dr["Id"];
                f.Origin = (string)dr["Origin"];
                f.Destination = (string)dr["Destination"];
                f.Price = float.Parse(dr["Price"].ToString());
                f.Date = (string)dr["DateFlight"];
                f.Airlines = (string)dr["Airlines"];
                f.Road = (string)dr["Road"];
                myFlight.Add(f);
            }

            return myFlight;
        }
        catch (Exception ex)
        {

            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }

    }

    public List<Flight> getMyFlightWithStop(string stop)
    {
        List<Flight> myFlight = new List<Flight>();
        SqlConnection con = null;

        try
        {
            con = connect("airportDBConnectionString");
            String selectSTR = "SELECT * FROM packageFlight_2020_ori WHERE Road LIKE '%" + stop + "%'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {
                Flight f = new Flight();
                f.Id = (string)dr["Id"];
                f.Origin = (string)dr["Origin"];
                f.Destination = (string)dr["Destination"];
                f.Price = float.Parse(dr["Price"].ToString());
                f.Date = (string)dr["DateFlight"];
                f.Airlines = (string)dr["Airlines"];
                f.Road = (string)dr["Road"];
                myFlight.Add(f);
            }

            return myFlight;


        }
        catch (Exception ex)
        {

            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }

    }

    public int removeFlightById(string flightId)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("airportDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex); // write to log
        }

        String cStr = "DELETE FROM packageFlight_2020_ori WHERE Id='" + flightId + "'";      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //--------------------------------------------------------------------------------//
    //admin//

    public int addNewAdmin(Admin a)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("airportDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex); // write to log
        }

        String cStr = BuildInsertCommand(a);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    private string BuildInsertCommand(Admin a)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}','{1}')", a.UserName , a.UserPassword);
        String prefix = "INSERT INTO Admin_ori_2020 " + "(UserName,UserPassword) ";
        command = prefix + sb.ToString();

        return command;
    }

    public List<Admin> logIn()
    {
        List<Admin> adminList = new List<Admin>();
        SqlConnection con = null;
        try
        {
            con = connect("airportDBConnectionString");
            String selectSTR = "SELECT * FROM admin_ori_2020";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {
                Admin a = new Admin();
                a.UserName = (string)dr["UserName"];
                a.UserPassword = (string)dr["UserPassword"];

                adminList.Add(a);
            }

            return adminList;
        }
        catch (Exception ex)
        {

            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------------------//
    //order//

    public List<order> getOrderTable()
    {
        List<order> orderList = new List<order>();
        SqlConnection con = null;
        try
        {
            con = connect("airportDBConnectionString");
            String selectSTR = "SELECT * FROM orderTable_2020_ori";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {
                order o = new order();
                o.FlightDate = (string)dr["FlightDate"];
                o.MainMail = (string)dr["MainMail"];
                o.Airline = (string)dr["Airline"];
                o.CityFrom = (string)dr["CityFrom"];
                o.CityTo = (string)dr["CityTo"];
                o.PassngerNames = (string)dr["PassngerNames"];
                orderList.Add(o);
            }

            return orderList;
        }
        catch (Exception ex)
        {

            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    private String BuildInsertCommand(order o)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}')", o.FlightDate, o.MainMail,o.Airline,o.CityFrom,o.CityTo, o.PassngerNames);
        String prefix = "INSERT INTO orderTable_2020_ori " + "(FlightDate,MainMail,Airline,CityFrom,CityTo,PassngerNames) ";
        command = prefix + sb.ToString();

        return command;
    }

    public int addOrer(order o)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("airportDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex); // write to log
        }

        String cStr = BuildInsertCommand(o);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //--------------------------------------------------------------------------------//
    //favorite

    public DBservices readFavoriteTable()
    {
        SqlConnection con = null;
        try
        {
            con = connect("airportDBConnectionString");
            da = new SqlDataAdapter("select * from favoriteFlight_2020_ori", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
        }

        catch (Exception ex)
        {
            throw ex;
        }

        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }


        return this;

    }

    public void update()
    {
        da.Update(dt);
    }

    private string BuildInsertCommand(favorite pack)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}','{1}','{2}',{3})", pack.Airline, pack.CityFrom, pack.CityTo, pack.NumOfLike);
        String prefix = "INSERT INTO favoriteFlight_2020_ori " + "(Airline,CityFrom,CityTo,NumberOfLike) ";
        command = prefix + sb.ToString();

        return command;
    }

    public int addNewFavorite(favorite pack)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("airportDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex); // write to log
        }

        String cStr = BuildInsertCommand(pack);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    public List<favorite> getFavoriteTable()
    {
        List<favorite> favoriteTable = new List<favorite>();
        SqlConnection con = null;
        try
        {
            con = connect("airportDBConnectionString");
            string selectSTR = "SELECT * from favoriteFlight_2020_ori";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                favorite f = new favorite();
                f.Airline = (string)dr["Airline"];
                f.CityFrom = (string)dr["CityFrom"];
                f.CityTo = (string)dr["CityTo"];
                f.NumOfLike = Convert.ToInt32(dr["NumberOfLike"]);
                favoriteTable.Add(f);
            }

            return favoriteTable;

        }
        catch (Exception ex)
        {

            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    //--------------------------------------------------------------------------------//
    //sale

    public List<sale> getDiscountTable()
    {
        List<sale> discountList = new List<sale>();
        SqlConnection con = null;
        try
        {
            con = connect("airportDBConnectionString");
            String selectSTR = "SELECT * from discountsTable_ori_2020";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {
                sale s = new sale();
                s.Airline = (string)dr["Airline"];
                s.CityFrom = (string)dr["CityFrom"];
                s.CityTo = (string)dr["CityTo"];
                s.Discount = Convert.ToDouble(dr["Discount"]);
                s.Id = Convert.ToInt16(dr["id"]);
                discountList.Add(s);
            }

            return discountList;
        }
        catch (Exception ex)
        {

            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public int deleteRow(int id)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("airportDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex); // write to log
        }

        String cStr = "delete from discountsTable_ori_2020 where id='"+id+"'";      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    public int updateDiscount (int id , double discount)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("airportDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex); // write to log
        }

        String cStr = "UPDATE discountsTable_ori_2020 SET Discount='" + discount + "' WHERE id='" + id + "'";      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    public int insertNewDiscount(sale newDiscount)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("airportDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex); // write to log
        }

        String cStr = BuildInsertCommand(newDiscount);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    private string BuildInsertCommand(sale newDiscount)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}','{1}','{2}',{3})", newDiscount.Airline, newDiscount.CityFrom, newDiscount.CityTo, newDiscount.Discount);
        String prefix = "INSERT INTO discountsTable_ori_2020 " + "(Airline,CityFrom,CityTo,Discount) ";
        command = prefix + sb.ToString();

        return command;
    }



}