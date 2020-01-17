using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW3_Igroup4.Models
{
    public class Admin
    {

        string userName;
        string userPassword;

        public string UserName{ get { return userName; } set { userName = value; } }
        public string UserPassword { get { return userPassword; } set {userPassword=value ;} }

        public int addNewAdmin(Admin a)
        {
            DBservices dbs = new DBservices();
            int numEffected = dbs.addNewAdmin(a);
            return numEffected;
        }

        public List<Admin> logIn()
        {
            DBservices dbs = new DBservices();
            List<Admin> adminList = dbs.logIn();
            return adminList;
        }

    }
}