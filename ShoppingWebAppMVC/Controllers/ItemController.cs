using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingWebAppMVC.Models;
using ShoppingWebAppMVC.ViewModel;


namespace ShoppingWebAppMVC.Controllers
{
    public class ItemController : Controller
    {
        ShoppingECartDBEntities db = new ShoppingECartDBEntities();
        public ItemController()
        {

        }
        // GET: Item
        public ActionResult Index()
        {
            ItemViewModel objItemViewModel = new ItemViewModel();
            //A CategorySelectListItems le asignamos valores de CategoryName-CategoryID
            objItemViewModel.CategorySelectListItems = (from d in db.Categories   
                                                        select new SelectListItem() { 
                                                            Text=d.CategoryName,
                                                            Value=d.CategoryID.ToString(),
                                                            Selected=true
                                                        });
            return View(objItemViewModel);
        }
        [HttpPost]
        public JsonResult Index(ItemViewModel objItemViewModel) /*para pasar los datos a la base de datos*/
        {
            string NewImage = Guid.NewGuid() + Path.GetExtension(objItemViewModel.ImagenPath.FileName); /*crea 9a48-614757b6cd53.jfif*/
            objItemViewModel.ImagenPath.SaveAs(Server.MapPath("~/Imagens/" + NewImage)); /*se Guarda la imagen con el nombre de la carpeta a donde se guarda y la ruta*/

            Items objItem = new Items(); /*creo un objeto de la tabla items. para pasarle los valores de objItemViewModel*/
            objItem.ImagenPath = "~/Imagens/" + NewImage; /*se le pasa el acceso y el nombre de la ruta*/
            objItem.CategoryID = objItemViewModel.CategoryID;
            objItem.Description = objItemViewModel.Description;
            objItem.ItemCode = objItemViewModel.ItemCode;
            objItem.ItemID = Guid.NewGuid(); /*Representa un identificador único global*/
            objItem.ItemName = objItemViewModel.ItemName;
            objItem.ItemPrice = objItemViewModel.ItemPrice;
            db.Items.Add(objItem); /*agregamos objItem a la tabla items*/
            db.SaveChanges();

            return Json(new { success = true, Message = "Compra Guardada con Exito" }, JsonRequestBehavior.AllowGet);
        }
    } 
}