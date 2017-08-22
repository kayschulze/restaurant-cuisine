using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using RestaurantList.Models;

namespace RestaurantList.Tests
{
    [TestClass]
    public class CuisineTests : IDisposable
    {
        public CuisineTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=restaurantlist_test;";
        }

        [TestMethod]
        public void GetAll_CuisinesEmptyAtFirst_0()
        {
            //Arrange, Act
            int result = Cuisine.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueForSameName_Cuisine()
        {
            //Arrange, Act
            Cuisine firstCuisine = new Cuisine("Italian");
            Cuisine secondCuisine = new Cuisine("Italian");

            //Assert
            Assert.AreEqual(firstCuisine, secondCuisine);
        }

        public void Dispose()
        {
            Restaurant.DeleteAll();
            Cuisine.DeleteAll();
        }
    }
}
