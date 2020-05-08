using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingWebAppMVC.ViewModel
{
    public class ItemViewModel
    {
        public Guid ItemID { get; set; }
        public int CategoryID { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase ImagenPath { get; set; }
        public decimal ItemPrice { get; set; }
        public IEnumerable<SelectListItem> CategorySelectListItems { get; set; }
    }
}