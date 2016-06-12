using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SEP.Models
{
    public class User
    {


        public int UserId { get; set; }

       // [DisplayName("dfdf")]
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Please enter lowercase & uppercase letters only")]
        public string Fname { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string Lname { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        public int PostalCode { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public int Phonenumber { get; set; }

        [Required(ErrorMessage = "NIC is required")]
        [RegularExpression((@"^[0-9]{9}[vVxX]$"), ErrorMessage = "Please enter a valid NIC number")]
        public string Nic { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6})$", ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Confirm password does not match")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }

      


    }


    public class userlogin
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Password { get; set; }

        public bool Rememberme { get; set; }


        public bool IsValid(string _username, string _pwd)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            string _sql = "Select * From tblCustomer  Where Username='" + _username + "' And Password='" + _pwd + "'";

            if (con.State.ToString() == "Closed")
            {

                con.Open();

            }
            SqlCommand cmd = new SqlCommand(_sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                UserId = Convert.ToInt32(dr["UserId"]);
                return true;

            }

            else
            {
                return false;

            }
#pragma warning disable CS0162 // Unreachable code detected
            con.Close();
#pragma warning restore CS0162 // Unreachable code detected
            dr.Close();
        }


    }

    public class LoginHistory
    {
        public string Username;
        public string Loggeddate;

    }


    public class UserImage
    {
        public int UserId { get; set; }

        public byte[] ProfPic { get; set; }
        [Required(ErrorMessage = "Please select file")]
        public HttpPostedFileBase File { get; set; }
    }

    public class RecoveryPasswordchange
    {
        public int UserId { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Confirm password does not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public int Code { get; set; }


    }

    public class editUser{

        public int UserId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First name is required")]
        public string Fname { get; set; }

        [DisplayName("Last name")]
        [Required(ErrorMessage = "Last name is required")]
        public string Lname { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [DisplayName("PostalCode")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Postal Code is required")]
        public int PostalCode { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6})$", ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [DisplayName("Phone")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Phone number is required")]
       // [RegularExpression("^[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]", ErrorMessage ="Please Enter Valid Phone number")]
        public int Phonenumber { get; set; }

        public byte[] ProfPic { get; set; }

       



    }

    public class clsUserPasswordedit
    {
        public int UserId { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Confirm password does not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


       

    }

  

    public class clsSendFeedback
    {
        public string Rate { get; set; }

        [DisplayName("Please Select")]
        public string Feedbacktype { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Feedback")]
        public string feedbacktext { get; set; }

    }
    public class clsAskquestion
    {


        [DisplayName("Enter your email address")]
        public string emailaddress { get; set; }


        [DisplayName("Enter your order number")]
        public string order { get; set; }


        [DisplayName("Reason for contact")]
        public string reason { get; set; }


        [DataType(DataType.MultilineText)]
        [DisplayName("How can we help you")]
        public string questionText { get; set; }

    }
    public class Order
    {
        [DisplayName("Order ID")]
        public int OrderId { get; set; }

        [DisplayName("User ID")]
        public int UserId { get; set; }

        [DisplayName("First Name")]
        public string Fname { get; set; }

        [DisplayName("Last Name")]
        public string Lname { get; set; }

        [DisplayName("Hotel ID")]
        public int HotelId { get; set; }

        [DisplayName("Date")]
        public DateTime date { get; set; }


    }

    public class BlacklistUser
    {
        [DisplayName("User ID")]
        public int UserId { get; set; }

        [DisplayName("Order ID")]
        public int OrderId { get; set; }

        [DisplayName("First Name")]
        public string Fname { get; set; }

        [DisplayName("Last Name ")]
        public string Lname { get; set; }

        [DisplayName("Customer Status")]
        public string userStatus { get; set; }


    }

}