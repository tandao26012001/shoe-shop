using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiShop.Controllers
{
    
    public class ProductController : Controller
    {
        MultiShopDbContext db = new MultiShopDbContext();
        //
        // GET: /Product/
        public ActionResult Category(int CategoryID=0)
        {
            if(CategoryID!=0)
            {
                ViewBag.TieuDe = db.Categories.FirstOrDefault(p => p.Id == CategoryID).Name;
                var model = db.Products.Where(p => p.CategoryId == CategoryID);
                return View(model);
            }
            
            return View();
        }
        //Download source code tại Sharecode.vn
        public ActionResult Search(String SupplierId = "", int CategoryId = 0, String Keywords = "")
        {
            if (SupplierId != "")
            {
                var model = db.Products
                    .Where(p => p.SupplierId == SupplierId);
                return View(model);
            }
            else if (CategoryId != 0)
            {
                var model = db.Products
                    .Where(p => p.CategoryId == CategoryId);
                return View(model);
            }
            else if (Keywords != "")
            {
                var model = db.Products
                    .Where(p => p.Name.Contains(Keywords));
                return View(model);
            }
            return View(db.Products);
        }

        public ActionResult Detail(int id,string SupplierID)
        {
            var model = db.Products.Find(id);

            // Tăng số lần xem
            model.Views++;
            db.SaveChanges();

            // Lấy cookie cũ tên views
            var views = Request.Cookies["views"];
            // Nếu chưa có cookie cũ -> tạo mới
            if (views == null)
            {
                views = new HttpCookie("views");
            }
            // Bổ sung mặt hàng đã xem vào cookie
            views.Values[id.ToString()] = id.ToString();
            // Đặt thời hạn tồn tại của cookie
            views.Expires = DateTime.Now.AddMonths(1);
            // Gửi cookie về client để lưu lại
            Response.Cookies.Add(views);

            // Lấy List<int> chứa mã hàng đã xem từ cookie
            var keys = views.Values
                .AllKeys.Select(k => int.Parse(k)).ToList();
            // Truy vấn háng đãn xem
            ViewBag.Views = db.Products
                .Where(p => keys.Contains(p.Id));
            return View(model);
        }
        public ActionResult List()
        {
            List<Product> listProduct = new List<Product>();
            var listPro = db.Products.Include("Category").ToList();
            foreach (var item in listPro)
            {
                listProduct.Add(new Product
                {
                    Id = item.Id,
                    Name = item.Name,
                    UnitBrief = item.UnitBrief,
                    Image = item.Image,
                    ProductDate = item.ProductDate,
                    Available = item.Available,
                    UnitPrice = item.UnitPrice,
                    Description = item.Description,
                    CategoryId = item.CategoryId,
                    SupplierId = item.SupplierId,
                    Quantity = item.Quantity,
                    Discount = item.Discount,
                    Special = item.Special,
                    Latest = item.Latest,
                    Views = item.Views

                });
            }
            return View(listProduct);
        }
    }
}