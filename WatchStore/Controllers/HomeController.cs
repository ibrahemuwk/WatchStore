using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchStore.Models;

namespace WatchStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db=new ApplicationDbContext();
            var RandomItems = db.Products.Include(x=>x.Brand).OrderBy(r => Guid.NewGuid()).Take(8).ToList();
            return View(RandomItems);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public FileContentResult UserPhotos(int id)
        {
           

                
                var bdProducts = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var productImage = bdProducts.Products.Find(id);
                if (productImage != null)
                {
                    var binaryImg = productImage.ProductImage;

                
                    return new FileContentResult(binaryImg, "image/jpeg");
                }
                else
                {
                    string fileName = HttpContext.Server.MapPath(@"/dist/img/noUser.png");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);
                    return File(imageData, "image/png");
            }
        }
       
        public ActionResult Galary(string key)
        {
        ApplicationDbContext db = new ApplicationDbContext();
        var products = db.Products.Include(p => p.Brand);
        if (key!="all")
        {
            products = products.Where(x => x.Brand.Name == key);
        }

            return View(products.ToList());
        }

        public ActionResult ProductDetail(int id)
        {
            ApplicationDbContext db=new ApplicationDbContext();

            var product = db.Products.FirstOrDefault(x => x.ProductId == id);
            ViewBag.BrandName = db.Brands.Find(product.BrandId)?.Name;
            return View(product);
        }

        //public ActionResult MiniCartget()
        //{
        //    var db = new ApplicationDbContext();
        //    var userId = User.Identity.GetUserId();
        //    var products = new List<Products>();
        //    var userProducts = db.Orders.Where(x => x.UserId == userId).ToList();
        //    if (userProducts != null || userProducts.Count > 0)
        //    {
        //        foreach (var item in userProducts)
        //        {
        //            products.Add(db.Products.SingleOrDefault(x => x.ProductId == item.ProductId));
        //        }
        //    }
        //    return PartialView("MiniCart", products);
        //}
       
        public ActionResult MiniCart()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            ViewBag.cartNum = db.Orders.Where(x=>x.UserId==userId).Count();
            return PartialView();
        }
        public ActionResult Search(string searchProduct)
        {
            var products = GetProducts(searchProduct);
            return PartialView(products);
        }
        private List<Products> GetProducts(string searchString)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Products.Include(p => p.Brand)
                .Where(a => a.Brand.Name.Contains(searchString) || a.ProductName.Contains(searchString))
                .ToList();
        }

        public ActionResult Filter(string hidLower, string hidUpper)
        {
            int lower=Int32.Parse(hidLower);
            int upper=Int32.Parse(hidUpper);
            ApplicationDbContext db = new ApplicationDbContext();
            var products=db.Products.Include(p => p.Brand)
                .Where(a => a.ProductPrice >=lower && a.ProductPrice <= upper)
                .ToList();
            return PartialView("Search", products);
        }
    }
}