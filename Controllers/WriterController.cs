using BoiMela.DataAccess;
using BoiMela.Dtos;
using BoiMela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoiMela.Controllers
{
    public class WriterController : Controller
    {
        // GET: Writer
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection formCollection, string btnSubmit)
        {
            if (btnSubmit == "Create A Writer")
            {
                if (formCollection != null)
                {
                    string name = formCollection["name"].ToString();

                    int result = WriterDataAccess.InsertWriter(name);
                    if (result == 1)
                    {
                        return RedirectToAction("Index", "Writer");
                    }
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            int Id = Convert.ToInt32(id);
            Writer writer = WriterDataAccess.GetWriterById(Id);
            return View(writer);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection formCollection, string btnSubmit)
        {
            if (btnSubmit == "Edit A Writer")
            {
                if (formCollection != null)
                {
                    int id = Convert.ToInt32(formCollection["id"]);
                    string name = formCollection["name"].ToString();

                    int result = WriterDataAccess.EditWriter(id, name);
                    if (result == 1)
                    {
                        return RedirectToAction("Index", "Writer");
                    }
                }
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            int Id = Convert.ToInt32(id);

            int result = WriterDataAccess.DeleteWriter(Id);

            if (result == 1)
            {
                return RedirectToAction("Index", "Writer");
            }

            return View();
        }
    }
}