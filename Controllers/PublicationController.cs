using BoiMela.DataAccess;
using BoiMela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoiMela.Controllers
{
    public class PublicationController : Controller
    {
        // GET: Publication
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
            if (btnSubmit == "Create A Publication")
            {
                if (formCollection != null)
                {
                    string name = formCollection["name"].ToString();

                    int result = PublicationDataAccess.InsertPublication(name);
                    if (result == 1)
                    {
                        return RedirectToAction("Index", "Publication");
                    }
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            int Id = Convert.ToInt32(id);
            Publication publication = PublicationDataAccess.GetPublicationById(Id);
            return View(publication);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection formCollection, string btnSubmit)
        {
            if (btnSubmit == "Edit Publication")
            {
                if (formCollection != null)
                {
                    int id = Convert.ToInt32(formCollection["id"]);
                    string name = formCollection["name"].ToString();

                    int result = PublicationDataAccess.EditPublication(id, name);
                    if (result == 1)
                    {
                        return RedirectToAction("Index", "Publication");
                    }
                }
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            int Id = Convert.ToInt32(id);

            int result = PublicationDataAccess.DeletePublication(Id);

            if (result == 1)
            {
                return RedirectToAction("Index", "Publication");
            }

            return View();
        }
    }
}