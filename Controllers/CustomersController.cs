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
    public class CustomersController : Controller
    {
        // GET: Customers
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
            if(btnSubmit == "Create A Customer")
            {
                if(formCollection != null)
                {
                    string fname = formCollection["fname"].ToString();
                    string lname = formCollection["lname"].ToString();
                    string email = formCollection["email"].ToString();
                    string phone = formCollection["phone"].ToString();
                    string address = formCollection["address"].ToString();

                    int result = CustomerDataAccess.InsertCustomer(fname, lname, email, phone, address);
                    if(result ==  1)
                    {
                        return RedirectToAction("Index", "Customers");
                    }
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            int Id = Convert.ToInt32(id);
            CustomerEditDtos customer = CustomerDataAccess.GetCustomerById(Id);
            return View(customer);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection formCollection, string btnSubmit)
        {
            if (btnSubmit == "Update Customer")
            {
                if (formCollection != null)
                {
                    int id = Convert.ToInt32(formCollection["id"]);
                    string fname = formCollection["fname"].ToString();
                    string lname = formCollection["lname"].ToString();
                    string email = formCollection["email"].ToString();
                    string phone = formCollection["phone"].ToString();
                    string address = formCollection["address"].ToString();

                    int result = CustomerDataAccess.UpdateCustomer(id, fname, lname, email, phone, address);
                    if (result == 1)
                    {
                        return RedirectToAction("Index", "Customers");
                    }
                }
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            int Id = Convert.ToInt32(id);

            int result = CustomerDataAccess.DeleteCustomer(Id);

            if(result == 1)
            {
                return RedirectToAction("Index", "Customers");
            }

            return View();
        }
    }
}