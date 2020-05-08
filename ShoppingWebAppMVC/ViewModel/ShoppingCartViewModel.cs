using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingWebAppMVC.ViewModel
{
    public class ShoppingCartViewModel
    {
        //esto es lo que se va mostrar cuando pasen los datos de los produtos agredados
        public string ItemID { get; set; }
        public string ImagenPath { get; set; }
        public string ItemName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public decimal Quantity { get; set; }

    }
}