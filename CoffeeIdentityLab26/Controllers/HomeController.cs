using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeIdentityLab26.Models;

namespace CoffeeIdentityLab26.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //[Authorize]
        public ActionResult Menu(Item data)
        {
            CoffeeShopIdentityEntities ORM = new CoffeeShopIdentityEntities();
            List<Item> items = ORM.Items.ToList();
            ViewBag.Items = items;
            ViewBag.Items = ORM.Items;
            return View();
        }
        public ActionResult ItemList()
        {
            CoffeeShopIdentityEntities db = new CoffeeShopIdentityEntities();
            List<Item> Items = db.Items.ToList();
            ViewBag.Items = Items;

            //ViewBag.Statuses = db.Statuses.ToList();

            return View();
        }

        //filter by status
        public ActionResult ItemListByPrice()
        {
            CoffeeShopIdentityEntities db = new CoffeeShopIdentityEntities();

            //LINQ Query
            List<Item> Items = (from i in db.Items
                                orderby i.Price
                                select i).ToList();
            ViewBag.Items = Items;



            return View("ItemList");
        }
        //public=Access modifier**
        //returnType=ActionResult
        //BookListByAuthor=Method name
        public ActionResult ItemListByItemName(string ItemName)
        {
            CoffeeShopIdentityEntities db = new CoffeeShopIdentityEntities();
            //LINQ Query
            ViewBag.Items = db.Items.Where(i => i.ItemName == ItemName).ToList();


            return View("ItemList");
        }

        public ActionResult ItemListSorted(string column)
        {
            CoffeeShopIdentityEntities db = new CoffeeShopIdentityEntities();
            //LINQ Query
            if (column == "ItemID")
            {
                ViewBag.Books = (from b in db.Items
                                 orderby b.ItemID
                                 select b).ToList();
            }
            else if (column == "ItemName")
            {
                ViewBag.Books = (from i in db.Items
                                 orderby i.ItemName
                                 select i).ToList();
            }
            else if (column == "Description")
            {
                ViewBag.Books = (from i in db.Items
                                 orderby i.Description
                                 select i).ToList();
            }
            else if (column == "Price")
            {
                ViewBag.Books = (from i in db.Items
                                 orderby i.Price
                                 select i).ToList();
            }

            ViewBag.ItemID = db.Items.ToList();

            return View("ItemList");
        }

        public ActionResult Add(string ItemName)
        {
            CoffeeShopIdentityEntities db = new CoffeeShopIdentityEntities();

            //check if the Cart object already exists
            if (Session["Cart"] == null)
            {
                //if it doesn't, make a new list of books
                List<Item> cart = new List<Item>();
                //add this book to it
                cart.Add((from i in db.Items
                          where i.ItemName == ItemName
                          select i).Single());
                //add the list to the session
                Session.Add("Cart", cart);
            }
            else
            {
                //if it does exist, get the list
                List<Item> cart = (List<Item>)(Session["Cart"]);
                //add this book to it
                cart.Add((from i in db.Items
                          where i.ItemName == ItemName
                          select i).Single());
            }
            return View();


        }

    }
}