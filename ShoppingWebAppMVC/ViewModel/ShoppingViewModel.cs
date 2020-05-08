using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingWebAppMVC.Models;

namespace ShoppingWebAppMVC.ViewModel
{
    public class ShoppingViewModel
    {
        //estos valores van a ser lo que va a pedir la vista Shopping
        public Guid ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal ItemPrice { get; set; }
        public string ImagenPath { get; set; }
        public string Category { get; set; }
    }
}