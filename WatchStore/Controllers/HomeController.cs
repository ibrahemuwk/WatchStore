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
            return View();
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
       
        public ActionResult Galary()
        {
        ApplicationDbContext db = new ApplicationDbContext();
        var products = db.Products.Include(p => p.Brand);

            return View(products.ToList());
        }

        public ActionResult ProductDetail(int id)
        {
            ApplicationDbContext db=new ApplicationDbContext();

            var product = db.Products.FirstOrDefault(x => x.ProductId == id);
            ViewBag.BrandName = db.Brands.Find(product.BrandId)?.Name;
            return View(product);
        }
    }
}