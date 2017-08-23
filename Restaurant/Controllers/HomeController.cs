using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using RestaurantList.Models;

namespace RestaurantList.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("/restaurants")]
        public ActionResult Restaurants()
        {
          return View();
        }

        [HttpPost("/restaurants")]
        public ActionResult AddRestaurant()
        {
          return View();
        }

        [HttpGet("/restaurants/new")]
        public ActionResult RestaurantForm()
        {
            return View();
        }

        [HttpGet("/cuisines")]
        public ActionResult Cuisines()
        {
          return View();
        }

        [HttpPost("/cuisines")]
        public ActionResult AddCuisine()
        {
          Cuisine newCuisine = new Cuisine(Request.Form["new-name"]);
          newCuisine.Save();
          return View();
        }

        [HttpGet("/cuisines/new")]
        public ActionResult CuisineForm()
        {
            return View();
        }
    }
}
