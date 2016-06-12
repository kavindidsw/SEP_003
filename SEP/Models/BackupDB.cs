using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEP.Models
{
    public class BackupDB
    {
        private string backupDIR;
        private string filename;

        public bool BackupDatabase()
        {
            string conStr = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conStr))
            {
                int result;
                //this.backupDIR = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Backup.bak");
              
                this.backupDIR = "C:\\Backups";
                this.filename = DateTime.Now.ToString("ddMMyyyy_HHmmss");

                SqlCommand cmd = new SqlCommand("backup database SEP to disk='" + backupDIR + "\\" + filename + ".Bak'", con);
                cmd.CommandType = CommandType.Text;

          
                con.Open();

                result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }




            }
        }

        public string getfilename()
        {
            return this.filename;
        }
        public string getdirectory()
        {
            return this.backupDIR;
        }

    }
}