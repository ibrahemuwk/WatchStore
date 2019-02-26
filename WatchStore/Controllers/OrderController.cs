using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WatchStore.Models;

namespace WatchStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Order
        public JsonResult AddOrder(int id)
        {
            var userId = User.Identity.GetUserId();
            var order=new Order();
            order.ProductId = id;
            order.OrderDate=DateTime.Now;
            order.UserId = userId;
            db.Orders.Add(order);
            db.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult Remove(string id)
        {
            if (id=="all")
            {
                var userId = User.Identity.GetUserId();
                var items = db.Orders.Where(x => x.UserId == userId).ToList();
                if (items.Count > 0)
                {
                    foreach (var order in items)
                    {
                        db.Orders.Remove(order);
                    }
                    db.SaveChanges();
                }
            }
            else { 
            int itemId = Int32.Parse(id);
            var userId = User.Identity.GetUserId();
            var item = db.Orders.FirstOrDefault(x=>x.ProductId== itemId && x.UserId== userId);
            if (item != null) db.Orders.Remove(item);
            //_context.DoLists.SingleOrDefault(m => m.Id == itemId).IsCompleted = chk;
            db.SaveChanges();
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var products=new List<Products>();
            var userProducts = db.Orders.Where(x => x.UserId == userId).ToList();
            if (userProducts !=null|| userProducts.Count>0)
            {
                foreach (var item in userProducts)
                {
                    products.Add(db.Products.SingleOrDefault(x => x.ProductId == item.ProductId));
                }
            }
            
            return View(products);
        }
    }
}