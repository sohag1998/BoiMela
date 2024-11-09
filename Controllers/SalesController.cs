using BoiMela.DataAccess;
using BoiMela.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace BoiMela.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection, string btnSubmit)
        {
            if(formCollection.Count > 0)
            {
                if(btnSubmit == "Insert Sale")
                {
                    Dictionary<int, int> bookQuantities = new Dictionary<int, int>();
                    int cutomerId = Convert.ToInt32(formCollection["customer"]);
                    foreach (var key in formCollection.AllKeys)
                    {
                        if (key.StartsWith("book_"))
                        {
                            // Extract the bookId from the key name (e.g., "book" -> 1)
                            int bookId = Convert.ToInt32(key.Replace("book_", ""));
                            int quantity = Convert.ToInt32(formCollection[key]);
                            Book book = BookDataAccess.GetBookById(bookId);

                            if(book.Stock < quantity)
                            {
                                ViewBag.Message = "Insuficiennt Book";
                                return View();
                            }

                            // Add to dictionary
                            bookQuantities.Add(bookId, quantity);
                        }
                    }
                    int result = SalesDataAccess.InsertSale(bookQuantities, cutomerId);

                    return RedirectToAction("Index", "Sales");

                }
                
            }
            return View();
            
        }
        [HttpGet]
        [Route("Sales/GetSoldBookList/{id}")]
        public ActionResult GetSoldBookList(string id)
        {
            int OrderId = Convert.ToInt32(id);
            List<SoldBook> soldBooks = SalesDataAccess.GetSalesBookList(OrderId);

            return Json(soldBooks, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            int OrderId = Convert.ToInt32(id);
            ViewBag.OrderId = OrderId;

            SalesDetalis salesDetalis = SalesDataAccess.GetSaleDetails(OrderId);
            return View(salesDetalis);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            int OrderId = Convert.ToInt32(id);
            ViewBag.OrderId = OrderId;

            SalesDetalis salesDetalis = SalesDataAccess.GetSaleDetails(OrderId);
            return View(salesDetalis);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection formCollection, string updBtn)
        {
            if(updBtn == "Update")
            {
                if (formCollection != null)
                {

                    int orderId = Convert.ToInt32(formCollection["orderId"]);
                    Dictionary<int, int> bookQuantites = new Dictionary<int, int>();

                    foreach (var key in formCollection.AllKeys)
                    {
                        if (key.StartsWith("book_"))
                        {
                            int bookId = Convert.ToInt32(key.Replace("book_", ""));
                            int quantity = Convert.ToInt32(formCollection[key]);

                            bookQuantites.Add(bookId, quantity);
                        }
                    }

                    string orderStatus = formCollection["OrderStatus"].ToString();

                    int result = SalesDataAccess.UpdateOrder(orderId, orderStatus, bookQuantites);

                    if (result==1)
                    {
                        return RedirectToAction("Detail", "Sales", new { id = orderId });
                    }

                }
            }

            return View();
        }

    }


}