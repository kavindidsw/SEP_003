using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SEP.Models
{
    public class RestaurantLayer
    {
        public IEnumerable<ViewRestaurantDetails> Restaurants
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

                List<ViewRestaurantDetails> restaurant = new List<ViewRestaurantDetails>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select * from Hotel", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ViewRestaurantDetails res = new ViewRestaurantDetails();
                        res.RestaurantId = Convert.ToInt32(dr["HotelId"]);
                        res.Restaurantname = dr["HotelName"].ToString();
                        res.RegistrationNo = dr["RegistrationNo"].ToString();
                        res.Email = dr["Email"].ToString();               
                        res.Address = dr["Address"].ToString();
                        res.Phonenumber = Convert.ToInt32(dr["PhoneNo1"]);
                      

                        restaurant.Add(res);

                    }
                    dr.Close();
                }
                return restaurant;

            }
        }

       


        public bool DeleteResaturants(int ResId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("delete Hotel where HotelId=@ResId", con);
                cmd.CommandType = CommandType.Text;

                SqlParameter resid = new SqlParameter();
                resid.ParameterName = "@ResId";
                resid.Value = ResId;
                cmd.Parameters.Add(resid);

                if (con.State.ToString() == "Closed")
                {
                    con.Open();
                }
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

    }
}