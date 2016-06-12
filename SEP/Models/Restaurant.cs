using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace SEP.Models
{

    public class Restaurant
    {

    }

        public class ViewRestaurantDetails
    {
        public int RestaurantId { get; set; }
        public string Restaurantname { get; set; }
        public string RegistrationNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Phonenumber { get; set; }

      
    }
}