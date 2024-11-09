using BoiMela.DataAccess;
using BoiMela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoiMela.Controllers
{
    public class GenreController : Controller
    {
        // GET: Genre
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
            if (btnSubmit == "Create A Genre")
            {
                if (formCollection != null)
                {
                    string name = formCollection["name"].ToString();

                    int result = GenreDataAccess.InsertGenre(name);
                    if (result == 1)
                    {
                        return RedirectToAction("Index", "Genre");
                    }
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            int Id = Convert.ToInt32(id);
            Genre genre = GenreDataAccess.GetGenreById(Id);
            return View(genre);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection formCollection, string btnSubmit)
        {
            if (btnSubmit == "Edit Genre")
            {
                if (formCollection != null)
                {
                    int id = Convert.ToInt32(formCollection["id"]);
                    string name = formCollection["name"].ToString();

                    int result = GenreDataAccess.EditGenre(id, name);
                    if (result == 1)
                    {
                        return RedirectToAction("Index", "Genre");
                    }
                }
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            int Id = Convert.ToInt32(id);

            int result = GenreDataAccess.DeleteGenre(Id);

            if (result == 1)
            {
                return RedirectToAction("Index", "Genre");
            }

            return View();
        }
    }
}