using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace SEP.Models
{
    public class UserLayer
    {
        public void AddUser(User user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("spaddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter para = new SqlParameter();
                para.ParameterName = "@Fname";
                para.Value = user.Fname;
                cmd.Parameters.Add(para);

                SqlParameter para1 = new SqlParameter();
                para1.ParameterName = "@Lname";
                para1.Value = user.Lname;
                cmd.Parameters.Add(para1);



                SqlParameter para2 = new SqlParameter();
                para2.ParameterName = "@NicNumber";
                para2.Value = user.Nic;
                cmd.Parameters.Add(para2);

                SqlParameter para3 = new SqlParameter();
                para3.ParameterName = "@Phonenumber";
                para3.Value = user.Phonenumber;
                cmd.Parameters.Add(para3);

                SqlParameter para4 = new SqlParameter();
                para4.ParameterName = "@Address";
                para4.Value = user.Address;
                cmd.Parameters.Add(para4);

                SqlParameter para5 = new SqlParameter();
                para5.ParameterName = "@PostalCode";
                para5.Value = user.PostalCode;
                cmd.Parameters.Add(para5);

                SqlParameter para6 = new SqlParameter();
                para6.ParameterName = "@Email";
                para6.Value = user.Email;
                cmd.Parameters.Add(para6);

                SqlParameter para7 = new SqlParameter();
                para7.ParameterName = "@Password";
                para7.Value = user.Password;
                cmd.Parameters.Add(para7);

                SqlParameter para8 = new SqlParameter();
                para8.ParameterName = "@ConfirmPassword";
                para8.Value = user.ConfirmPassword;
                cmd.Parameters.Add(para8);

                SqlParameter para9 = new SqlParameter();
                para9.ParameterName = "@Username";
                para9.Value = user.Username;
                cmd.Parameters.Add(para9);

                con.Open();
                cmd.ExecuteNonQuery();

            }



        }

        public bool saveLoginhistory(userlogin user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
               
               SqlCommand cmd = new SqlCommand(" insert Loginhistory(username,Loggeddate) values(@username,@history)", con);
                cmd.CommandType = CommandType.Text;

                SqlParameter para = new SqlParameter();
                para.ParameterName = "@username";
                para.Value = user.Username;
                cmd.Parameters.Add(para);

                string str = user.UserId + " user has logged to the system at" + DateTime.Now.ToString();
                SqlParameter para1 = new SqlParameter();
                para1.ParameterName = "@history";
                para1.Value = str;
                cmd.Parameters.Add(para1);

                con.Open();

                int result = cmd.ExecuteNonQuery();
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

        public bool Saveimage(UserImage user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand(" update tblCustomer set ProfPic =@img where UserId=@UserId", con);
                cmd.CommandType = CommandType.Text;

                SqlParameter para = new SqlParameter();
                para.ParameterName = "@img";
                para.Value = user.ProfPic;
                cmd.Parameters.Add(para);

                SqlParameter para1 = new SqlParameter();
                para1.ParameterName = "@UserId";
                para1.Value = user.UserId;
                cmd.Parameters.Add(para1);

                con.Open();

                int result = cmd.ExecuteNonQuery();
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

        public void SaveUser(editUser user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("spSaveCustomer1", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter userid = new SqlParameter();
                userid.ParameterName = "@UserId";
                userid.Value = user.UserId;
                cmd.Parameters.Add(userid);

                SqlParameter para = new SqlParameter();
                para.ParameterName = "@Fname";
                para.Value = user.Fname;
                cmd.Parameters.Add(para);

                SqlParameter para1 = new SqlParameter();
                para1.ParameterName = "@Lname";
                para1.Value = user.Lname;
                cmd.Parameters.Add(para1);

                SqlParameter para3 = new SqlParameter();
                para3.ParameterName = "@Phonenumber";
                para3.Value = user.Phonenumber;
                cmd.Parameters.Add(para3);

                SqlParameter para4 = new SqlParameter();
                para4.ParameterName = "@Address";
                para4.Value = user.Address;
                cmd.Parameters.Add(para4);

                SqlParameter para5 = new SqlParameter();
                para5.ParameterName = "@PostalCode";
                para5.Value = user.PostalCode;
                cmd.Parameters.Add(para5);

                SqlParameter para6 = new SqlParameter();
                para6.ParameterName = "@Email";
                para6.Value = user.Email;
                cmd.Parameters.Add(para6);

                SqlParameter para9 = new SqlParameter();
                para9.ParameterName = "@Username";
                para9.Value = user.Username;
                cmd.Parameters.Add(para9);

                con.Open();
                cmd.ExecuteNonQuery();

            }
        }
        public void Updatecode(int code,string Email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("update tblCustomer set code=@code where Email=@Email", con);
                cmd.CommandType = CommandType.Text;

                SqlParameter para1 = new SqlParameter();
                para1.ParameterName = "@code";
                para1.Value =code ;
                cmd.Parameters.Add(para1);

                SqlParameter para2 = new SqlParameter();
                para2.ParameterName = "@Email";
                para2.Value = Email;
                cmd.Parameters.Add(para2);


                con.Open();
                cmd.ExecuteNonQuery();

            }
        }

        public bool RecoveryUserPassword(RecoveryPasswordchange user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("update tblCustomer set Password=@Password where code=@code", con);
                cmd.CommandType = CommandType.Text;

                SqlParameter para1 = new SqlParameter();
                para1.ParameterName = "@code";
                para1.Value = user.Code;
                cmd.Parameters.Add(para1);

                SqlParameter para2 = new SqlParameter();
                para2.ParameterName = "@Password";
                para2.Value = user.Password;
                cmd.Parameters.Add(para2);

                con.Open();
                int result=cmd.ExecuteNonQuery();
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

        public void SaveUserPassword(clsUserPasswordedit user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("spSaveCustomerPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter para10 = new SqlParameter();
                para10.ParameterName = "@UserId";
                para10.Value = user.UserId;
                cmd.Parameters.Add(para10);

                SqlParameter para = new SqlParameter();
                para.ParameterName = "@Password";
                para.Value = user.Password;
                cmd.Parameters.Add(para);

                con.Open();
                cmd.ExecuteNonQuery();

            }
        }
        /// <summary>
        /// ////////////////////////////////////////


        public IEnumerable<UserImage> images
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

                List<UserImage> imagesl = new List<UserImage>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select * from tblCustomer", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        UserImage user = new UserImage();
                        user.UserId = Convert.ToInt32(dr["UserId"]);
                        //  user.ProfPic = (byte[])dr["ProfPic"];
                        if (dr["ProfPic"] != DBNull.Value)
                        {
                            user.ProfPic = (byte[])dr["ProfPic"];
                        }
                        else
                        {
                            user.ProfPic = null;
                        }


                        //user.Password = dr["Password"].ToString();

                        imagesl.Add(user);

                    }
                    dr.Close();

                }
                return imagesl;

            }
        }

        /// </summary>
        public IEnumerable<editUser>Users
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

                List<editUser> users = new List<editUser>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetCustomer",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        editUser user = new editUser();
                        user.UserId = Convert.ToInt32(dr["UserId"]);
                        user.Fname = dr["Fname"].ToString();
                        user.Lname = dr["Lname"].ToString();
                        user.Email = dr["Email"].ToString();
                        user.Username= dr["Username"].ToString();
                        user.Address= dr["Address"].ToString();
                        user.PostalCode= Convert.ToInt32(dr["PostalCode"]);
                        user.Phonenumber = Convert.ToInt32(dr["Phonenumber"]);
                        if (dr["ProfPic"] != DBNull.Value)
                        {
                            user.ProfPic = (byte[])dr["ProfPic"];
                        }
                        else
                        {
                            user.ProfPic = null;
                        }

                        users.Add(user);

                    }
                    dr.Close();
                }
                return users;

            }
        }


        public IEnumerable<clsUserPasswordedit> Usersl
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

                List<clsUserPasswordedit> users = new List<clsUserPasswordedit>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        clsUserPasswordedit user = new clsUserPasswordedit();
                        user.UserId = Convert.ToInt32(dr["UserId"]);
                        user.OldPassword = dr["Password"].ToString();
                        

                        //user.Password = dr["Password"].ToString();

                        users.Add(user);

                    }
                    dr.Close();
                }
                return users;

            }
        }

        public IEnumerable<RecoveryPasswordchange> recoverypasswordchange
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

                List<RecoveryPasswordchange> users = new List<RecoveryPasswordchange>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select * from tblCustomer", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        RecoveryPasswordchange user = new RecoveryPasswordchange();
                        user.UserId = Convert.ToInt32(dr["UserId"]);
                        

                        if (dr["code"] != DBNull.Value)
                        {
                            user.Code = Convert.ToInt32(dr["code"]);
                        }
                        else
                        {
                            user.Code = 0;
                        }

                        user.OldPassword = dr["Password"].ToString();


                        //user.Password = dr["Password"].ToString();

                        users.Add(user);

                    }
                    dr.Close();
                }
                return users;

            }
        }

        public bool DeleteCustomer(int id)
        {
            string conStr = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conStr))
            {
                int result;
                SqlCommand cmd = new SqlCommand("spDeleteCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paraId = new SqlParameter();
                paraId.ParameterName = "@id";
                paraId.Value = id;
                cmd.Parameters.Add(paraId);
                con.Open();

                result=cmd.ExecuteNonQuery();

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

        public void EditUser(User user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("update tblCustomer set Fname=@Fname,Lname=@Lname,Phonenumber=@Phonenumber,Address=@Address,Email=@Email,Password=@Password,ConfirmPassword=@ConfirmPassword,PostalCode=@PostalCode where Username=@Username", con);
                cmd.CommandType = CommandType.Text;

                SqlParameter para = new SqlParameter();
                para.ParameterName = "@Fname";
                para.Value = user.Fname;
                cmd.Parameters.Add(para);

                SqlParameter para1 = new SqlParameter();
                para1.ParameterName = "@Lname";
                para1.Value = user.Lname;
                cmd.Parameters.Add(para1);



                SqlParameter para2 = new SqlParameter();
                para2.ParameterName = "@Phonenumber";
                para2.Value = user.Phonenumber;
                cmd.Parameters.Add(para2);

                SqlParameter para3 = new SqlParameter();
                para3.ParameterName = "@Address";
                para3.Value = user.Address;
                cmd.Parameters.Add(para3);

                SqlParameter para4 = new SqlParameter();
                para4.ParameterName = "@PostalCode";
                para4.Value = user.PostalCode;
                cmd.Parameters.Add(para4);

                SqlParameter para5 = new SqlParameter();
                para5.ParameterName = "@Email";
                para5.Value = user.Email;
                cmd.Parameters.Add(para5);

                SqlParameter para6 = new SqlParameter();
                para6.ParameterName = "@Password";
                para6.Value = user.Password;
                cmd.Parameters.Add(para6);

                SqlParameter para7 = new SqlParameter();
                para7.ParameterName = "@ConfirmPassword";
                para7.Value = user.ConfirmPassword;
                cmd.Parameters.Add(para7);

                SqlParameter para8 = new SqlParameter();
                para8.ParameterName = "@Username";
                para8.Value = user.Username;
                cmd.Parameters.Add(para8);

                con.Open();
                cmd.ExecuteNonQuery();

            }



        }

        public IEnumerable<LoginHistory> Loginhistory
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

                List<LoginHistory> users = new List<LoginHistory>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select * from Loginhistory", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        LoginHistory user = new LoginHistory();
                        user.Username = dr["username"].ToString();
                        user.Loggeddate = dr["Loggeddate"].ToString();


                       

                        users.Add(user);

                    }
                    dr.Close();
                }
                return users;

            }


            
        }
        #region uchini
        public IEnumerable<Order> Orders
        {
            get
            {
                string conStr = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

                List<Order> orders = new List<Order>();
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = new SqlCommand("select o.OrderId,o.UserId,o.HotelId,o.date , c.Fname, c.Lname from   Orderdetails o, tblCustomer c where  o.UserId=c.UserId", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Order order = new Order();
                        order.UserId = Convert.ToInt32(dr["UserId"]);
                        order.OrderId = Convert.ToInt32(dr["OrderId"]);
                        order.HotelId = Convert.ToInt32(dr["HotelId"]);
                        order.date = Convert.ToDateTime(dr["date"]);
                        order.Fname = dr["Fname"].ToString();
                        order.Lname = dr["Lname"].ToString();

                        orders.Add(order);
                    }

                    dr.Close();
                }
                return orders;


            }

        }

        public IEnumerable<BlacklistUser> BlackListUsers
        {
            get
            {
                string conStr = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

                List<BlacklistUser> blUsers = new List<BlacklistUser>();
                using (SqlConnection con = new SqlConnection(conStr))
                {

                    SqlCommand cmd = new SqlCommand("select b.UserId,c.Fname, c.Lname,b.userStatus,o.OrderId from   tblCustomer c, BlackListUsers b, Orderdetails o where  b.UserId = c.UserId and b.userStatus=1 and b.UserId=o.UserId", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        BlacklistUser bUser = new BlacklistUser();
                        bUser.UserId = Convert.ToInt32(dr["UserId"]);
                        bUser.OrderId = Convert.ToInt32(dr["OrderId"]);
                        //str = dr["userStatus"].ToString();
                        bUser.userStatus = "BlackListed";
                        bUser.Fname = dr["Fname"].ToString();
                        bUser.Lname = dr["Lname"].ToString();

                        blUsers.Add(bUser);
                    }

                    dr.Close();
                }
                return blUsers;




            }

        }

        public void DeleteBLCustomer(int id)
        {
            string conStr = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conStr))
            {

                SqlCommand cmd = new SqlCommand("spDeleteBLCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paraId = new SqlParameter();
                paraId.ParameterName = "@id";
                paraId.Value = id;
                cmd.Parameters.Add(paraId);
                con.Open();

                cmd.ExecuteNonQuery();



            }
        }

        #endregion

    }
}