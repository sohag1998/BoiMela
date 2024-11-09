using BoiMela.DataAccess;
using BoiMela.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoiMela.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
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
            int result = 0;
            if(btnSubmit == "insert")
            {
                string title = formCollection["title"].ToString();
                string writer = formCollection["writer"].ToString();
                string publication = formCollection["publication"].ToString();
                string genre = formCollection["genre"].ToString();
                string description = formCollection["description"].ToString();
                decimal price = Convert.ToDecimal(formCollection["price"].ToString());
                int qunt  = Convert.ToInt32(formCollection["qunt"].ToString());

                result = BookDataAccess.InsertBook(title, writer, publication, genre, description, price, qunt);
            }
            if (result == 0)
            {
                return View(formCollection);
            }
            else
                return RedirectToAction("Index", "Book");
           
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            int bookId = Convert.ToInt32(id);
            Book book = BookDataAccess.GetBookById(bookId);

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection, string btnSubmit)
        {
            int result = 0;
            if (btnSubmit == "edit")
            {
                int id = Convert.ToInt32(formCollection["bookId"]);
                string title = formCollection["title"].ToString();
                string writer = formCollection["writer"].ToString();
                string publication = formCollection["publication"].ToString();
                string genre = formCollection["genre"].ToString();
                string description = formCollection["description"].ToString();
                decimal price = Convert.ToDecimal(formCollection["price"].ToString());
                int qunt = Convert.ToInt32(formCollection["qunt"].ToString());

                result = BookDataAccess.EditBook(id, title, writer, publication, genre, description, price, qunt);
            }
            if (result == 0)
            {
                return View(formCollection);
            }
            else
                return RedirectToAction("Index", "Book");

        }
        public ActionResult Books()
        {
            return Json(BookDataAccess.GetBooksData(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            int bookId = Convert.ToInt32(id);
            int result = BookDataAccess.DeleteBook(bookId); 
            if(result == 1)
            {
                return RedirectToAction("Index", "Book");
            }
            return View();
        }

    }
}