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

        [TestMethod]
        public void Save_SavesCuisineToDatabase_CuisineList()
        {
            //Arrange
            Cuisine testCuisine = new Cuisine("Italian");
            testCuisine.Save();

            //Act
            List<Cuisine> resultList = Cuisine.GetAll();
            List<Cuisine> testList = new List<Cuisine> {testCuisine};

            //Assert
            CollectionAssert.AreEqual(testList, resultList);
        }

        [TestMethod]
        public void Save_DatabaseAssignsIdToCuisine_Id()
        {
            //Arrange
            Cuisine testCuisine = new Cuisine("Italian");
            testCuisine.Save();

            //Act
            Cuisine savedCuisine = Cuisine.GetAll()[0];

            int resultId = savedCuisine.GetId();
            int testId = testCuisine.GetId();

            //Assert
            Assert.AreEqual(resultId, testId);
        }

        [TestMethod]
        public void Find_FindsCuisineInDatabase_Cuisine()
        {
            //Arrange
            Cuisine testCuisine = new Cuisine("Italian");
            testCuisine.Save();

            //Act
            Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());

            //Assert
            Assert.AreEqual(testCuisine, foundCuisine);
        }

        [TestMethod]
        public void GetRestaurants_RetrievesAllRestaurantsWithCuisine_RestaurantList()
        {
            //Arrange
            Cuisine testCuisine = new Cuisine("Italian");
            testCuisine.Save();

            Restaurant firstRestaurant = new Restaurant("Reedville Cafe", testCuisine.GetId(), "1234 Reedville Street", 5, "burgers", 1);
            firstRestaurant.Save();
            Restaurant secondRestaurant = new Restaurant("Panera", testCuisine.GetId(), "567 185th Street", 4, "veggie options", 2);
            secondRestaurant.Save();

            //Act
            List<Restaurant> testRestaurantList = new List<Restaurant> {firstRestaurant, secondRestaurant};
            List<Restaurant> resultRestaurantList = testCuisine.GetRestaurants();

            //Assert
            CollectionAssert.AreEqual(testRestaurantList, resultRestaurantList);
        }



        public void Dispose()
        {
            Restaurant.DeleteAll();
            Cuisine.DeleteAll();
        }
    }
}
