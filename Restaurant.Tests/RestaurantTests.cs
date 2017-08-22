using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using RestaurantList.Models;

namespace RestaurantList.Tests
{
    [TestClass]
    public class RestaurantTests : IDisposable
    {
        public RestaurantTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=restaurantlist_test;";
        }
        public void Dispose()
        {
            Restaurant.DeleteAll();
            Cuisine.DeleteAll();
        }

        [TestMethod]
        public void Equals_OverrideTrueForName_Restaurant()
        {
            //Arrange, Act
            Restaurant firstRestaurant = new Restaurant("Reedville Cafe", 0, "1234 Reedville Street", 5, "burgers", 1);
            Restaurant secondRestaurant = new Restaurant("Reedville Cafe", 0, "1234 Reedville Street", 5, "burgers", 1);

            //Assert
            Assert.AreEqual(firstRestaurant.GetName(), secondRestaurant.GetName());
        }

        [TestMethod]
        public void Equals_OverrideTrueForEntireRestaurant()
        {
            //Arrange, Act
            Restaurant firstRestaurant = new Restaurant("Reedville Cafe", 0, "1234 Reedville Street", 5, "burgers", 1);
            Restaurant secondRestaurant = new Restaurant("Reedville Cafe", 0, "1234 Reedville Street", 5, "burgers", 1);

            //Assert
            Assert.AreEqual(firstRestaurant, secondRestaurant);
        }

        [TestMethod]
        public void Save_SavesRestaurantToDatabase_RestaurantList()
        {
            //Arrange
            Restaurant testRestaurant = new Restaurant("Reedville Cafe", 0, "1234 Reedville Street", 5, "burgers", 1);
            testRestaurant.Save();

            //Act
            List<Restaurant> resultList = Restaurant.GetAll();
            List<Restaurant> testList = new List<Restaurant>{testRestaurant};

            //Assert
            CollectionAssert.AreEqual(testList, resultList);
        }

        [TestMethod]
        public void Save_DatabaseAssignsIdToObject_Id()
        {
            //Arrange
            Restaurant testRestaurant = new Restaurant("Reedville Cafe", 0, "1234 Reedville Street", 5, "burgers", 1);
            testRestaurant.Save();

            //Act
            Restaurant savedRestaurant = Restaurant.GetAll()[0];

            int resultId = savedRestaurant.GetId();
            int testId = testRestaurant.GetId();

            //Assert
            Assert.AreEqual(testId, resultId);
        }

        [TestMethod]
        public void Find_FindsRestaurantInDatabase_Restaurant()
        {
            //Arrange
            Restaurant testRestaurant = new Restaurant("Reedville Cafe", 0, "1234 Reedville Street", 5, "burgers", 1);
            testRestaurant.Save();

            //Act
            Restaurant foundRestaurant = Restaurant.FindId(testRestaurant.GetId());

            //Assert
            Assert.AreEqual(testRestaurant, foundRestaurant);
        }

        [TestMethod]
        public void Find_FindsCuisineInDatabase_Restaurant()
        {
            //Arrange
            Restaurant firstRestaurant = new Restaurant("Reedville Cafe", 0, "1234 Reedville Street", 5, "burgers", 1);
            firstRestaurant.Save();
            Restaurant secondRestaurant = new Restaurant("Panera", 0, "567 185th Street", 4, "veggie options", 2);
            secondRestaurant.Save();

            //Act
            List<Restaurant> foundList = Restaurant.FindCuisine(0);
            List<Restaurant> testList = new List<Restaurant> {firstRestaurant, secondRestaurant};

            //Assert
            CollectionAssert.AreEqual(testList, foundList);
        }

        [TestMethod]
        public void UpdateRestaurant_RestaurantCorrectlyUpdates_Restaurant()
        {
            //Arrange
            Restaurant testRestaurant = new Restaurant("Reedville Cafe", 0, "1234 Reedville Street", 5, "burgers", 1);
            testRestaurant.Save();

            //Act
            string newName = "Panera";
            int newCuisineId = 0;
            string newAddress = "567 185th Street";
            int newRating = 4;
            string newSpecialty = "veggie options";

            testRestaurant.UpdateRestaurant(newName, newCuisineId, newAddress, newRating, newSpecialty);

            string resultName = Restaurant.FindId(testRestaurant.GetId()).GetName();

            //Assert
            Assert.AreEqual(newName, resultName);
        }

    }
}
