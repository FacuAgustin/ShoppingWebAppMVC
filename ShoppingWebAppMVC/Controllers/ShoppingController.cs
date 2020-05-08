using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingWebAppMVC.Models;
using ShoppingWebAppMVC.ViewModel;

namespace ShoppingWebAppMVC.Controllers
{
    public class ShoppingController : Controller
    {
        ShoppingECartDBEntities db = new ShoppingECartDBEntities();
        List<ShoppingCartViewModel> listOfShoppingCartViewModel = new List<ShoppingCartViewModel>(); /*a la clase la hago una lista*/
        // GET: Shopping
        public ActionResult Index()
        {
            //Lista de ShoppingViewModel Igualando valores de las dos tablas para ser ultilizados
            //se cita a objetos de las dos tablas para poder asignarle valor a los valores de ShoppingViewModel.
            IEnumerable<ShoppingViewModel> listOfShoppingViewModel = (from d in db.Items
                                                                      join
                                                                      dbCat in db.Categories
                                                                      on d.CategoryID equals dbCat.CategoryID
                                                                      select new ShoppingViewModel()
                                                                      {
                                                                          ImagenPath = d.ImagenPath,
                                                                          ItemID = d.ItemID,
                                                                          ItemName = d.ItemName,
                                                                          ItemCode = d.ItemCode,
                                                                          Description = d.Description,
                                                                          ItemPrice = d.ItemPrice,
                                                                          Category = dbCat.CategoryName,
                                                                      }).ToList();


            return View(listOfShoppingViewModel);
        }
        [HttpPost]
        public JsonResult Index(string ItemID)
        {
            //creo un obj de ShoppingCartViewModel 
            ShoppingCartViewModel objShoppCart = new ShoppingCartViewModel();

            //este objItem almaceno la coincidencia del ItemID de la tabla Item y ItemId de 
            Items objItem = db.Items.Single(model => model.ItemID.ToString() == ItemID);
            if (Session["CartCount"] != null)
            {
                //agrega a la cart un elemento mas
                listOfShoppingCartViewModel = Session["ItemCart"] as List<ShoppingCartViewModel>;
            }
            if (listOfShoppingCartViewModel.Any(model => model.ItemID == ItemID))
            {

                objShoppCart = listOfShoppingCartViewModel.Single(model => model.ItemID == ItemID);
                objShoppCart.Quantity = objShoppCart.Quantity + 1;
                objShoppCart.Total = objShoppCart.Quantity * objShoppCart.UnitPrice;
            }
            else
            {
                objShoppCart.ItemID = ItemID;
                objShoppCart.ImagenPath = objItem.ImagenPath;
                objShoppCart.ItemName = objItem.ItemName;
                objShoppCart.Quantity = +1;
                objShoppCart.UnitPrice = objItem.ItemPrice;
                objShoppCart.Total = objItem.ItemPrice;
                //Agrego estos nuevo datos a los parametros de ShoppingCartViewModel
                listOfShoppingCartViewModel.Add(objShoppCart);
            }
            //para guardar datos en memoria hasta cerrar la sesion
            Session["CartCount"] = listOfShoppingCartViewModel.Count;
            Session["ItemCart"] = listOfShoppingCartViewModel;
            return Json(new { Success = true, Counter = listOfShoppingCartViewModel.Count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShoppingCart() /*Creo una vista para Cart y hacer una lista de los productos agregados*/
        {
            
            listOfShoppingCartViewModel = Session["ItemCart"] as List<ShoppingCartViewModel>;
            return View(listOfShoppingCartViewModel);
        }

        [HttpPost]
        public ActionResult SaveOrder()
        {
            int OrderId = 0;
            listOfShoppingCartViewModel = Session["ItemCart"] as List<ShoppingCartViewModel>;
            Orders objOrder = new Orders()
            {
                OrderDate = DateTime.Now,
                OrderNumber=String.Format("{0:ddmmyyyyHHmmss}",DateTime.Now)
            };
            db.Orders.Add(objOrder);
            db.SaveChanges();

            //le asigno valor a la variable int OrderId..para usarlo en el foreach
            OrderId = objOrder.OrderId;
            foreach (var item in listOfShoppingCartViewModel)
            {
                OrderDetails objOrderDetail = new OrderDetails();
                objOrderDetail.OrderId = OrderId;
                objOrderDetail.Quantity = item.Quantity;
                objOrderDetail.ItemId = item.ItemID;
                objOrderDetail.UnitPrice = item.UnitPrice;
                objOrderDetail.Total = item.Total;
                db.OrderDetails.Add(objOrderDetail);
                db.SaveChanges();
            }
            Session["ItemCart"] = null;
            Session["CartCount"] = null;
            return RedirectToAction("Index");
        }
    }
}

    

