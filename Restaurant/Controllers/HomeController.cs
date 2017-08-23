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

        }

        [HttpPost("/restaurants")]
        public ActionResult AddRestaurant()
        {

        }

        [HttpGet("/restaurants/new")]
        public ActionResult RestaurantForm()
        {
            return View()
        }

        [HttpGet("/cuisines")]
        public ActionResult Cuisines()
        {

        }

        [HttpPost("/cuisines")]
        public ActionResult AddCuisine()
        {

        }

        [HttpGet("/cuisines/new")]
        public ActionResult CuisineForm()
        {
            return View()
        }


    }


}
