using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace RestaurantList.Models
{
    public class Restaurant
    {
        private int _id;
        private string _name;
        private int _cuisine_id;
        private string _address;
        private int _rating;
        private string _specialty;

        public Restaurant(string name, int cuisine_id, string address, int rating, string specialty, int id = 0)
        {
            _id = id;
            _name = name;
            _cuisine_id = cuisine_id;
            _address = address;
            _rating = rating;
            _specialty = specialty;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetCuisineId()
        {
            return _cuisine_id;
        }

        public string GetAddress()
        {
            return _address;
        }

        public int GetRating()
        {
            return _rating;
        }

        public string GetSpecialty()
        {
            return _specialty;
        }

        public override bool Equals(System.Object otherRestaurant)
        {
            if (!(otherRestaurant is Restaurant))
            {
                return false;
            }
            else
            {
                Restaurant newRestaurant = (Restaurant) otherRestaurant;
                bool idEquality = this.GetId() == newRestaurant.GetId();
                bool nameEquality = this.GetName() == newRestaurant.GetName();
                bool cuisineEquality = this.GetCuisineId() == newRestaurant.GetCuisineId();
                bool ratingEquality = this.GetRating() == newRestaurant.GetRating();
                bool specialtyEquality = this.GetSpecialty() == newRestaurant.GetSpecialty();

                return (idEquality && nameEquality && cuisineEquality && ratingEquality && specialtyEquality);
            }
        }

        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO restaurants(name, cuisine_id, address, rating, specialty) VALUES (@name, @cuisine_id, @address, @rating, @specialty);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            MySqlParameter cuisineId = new MySqlParameter();
            cuisineId.ParameterName = "@cuisine_id";
            cuisineId.Value = this._cuisine_id;
            cmd.Parameters.Add(cuisineId);

            MySqlParameter address = new MySqlParameter();
            address.ParameterName = "@address";
            address.Value = this._address;
            cmd.Parameters.Add(address);

            MySqlParameter rating = new MySqlParameter();
            rating.ParameterName = "@rating";
            rating.Value = this._rating;
            cmd.Parameters.Add(rating);

            MySqlParameter specialty = new MySqlParameter();
            specialty.ParameterName = "@specialty";
            specialty.Value = this._specialty;
            cmd.Parameters.Add(specialty);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Restaurant> GetAll()
        {
            List<Restaurant> allRestaurants = new List<Restaurant> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM restaurants;";

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int restaurantId = rdr.GetInt32(0);
                string restaurantName = rdr.GetString(1);
                string restaurantAddress = rdr.GetString(2);
                int restaurantRating = rdr.GetInt32(3);
                int restaurantCuisineId = rdr.GetInt32(4);
                string restaurantSpecialty = rdr.GetString(5);

                Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCuisineId, restaurantAddress, restaurantRating, restaurantSpecialty, restaurantId);

                allRestaurants.Add(newRestaurant);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allRestaurants;
        }

        public static Restaurant FindId(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM restaurants WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int restaurantId = 0;
            string restaurantName = "";
            int restaurantCuisineId = 0;
            string restaurantAddress = "";
            int restaurantRating = 0;
            string restaurantSpecialty = "";

            while(rdr.Read())
            {
                restaurantId = rdr.GetInt32(0);
                restaurantName = rdr.GetString(1);
                restaurantAddress = rdr.GetString(2);
                restaurantRating = rdr.GetInt32(3);
                restaurantCuisineId = rdr.GetInt32(4);
                restaurantSpecialty = rdr.GetString(5);
            }

            Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCuisineId, restaurantAddress, restaurantRating, restaurantSpecialty, restaurantId);
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
            return newRestaurant;
        }

        public static List<Restaurant> FindCuisine(int cuisine_id)
        {
            List<Restaurant> allCuisineRestaurants = new List<Restaurant>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM restaurants WHERE cuisine_id = (@searchCuisineId);";

            MySqlParameter searchCuisineId = new MySqlParameter();
            searchCuisineId.ParameterName = "@searchCuisineId";
            searchCuisineId.Value = cuisine_id;
            cmd.Parameters.Add(searchCuisineId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int restaurantId = 0;
            string restaurantName = "";
            int restaurantCuisineId = 0;
            string restaurantAddress = "";
            int restaurantRating = 0;
            string restaurantSpecialty = "";

            while(rdr.Read())
            {
                restaurantId = rdr.GetInt32(0);
                restaurantName = rdr.GetString(1);
                restaurantAddress = rdr.GetString(2);
                restaurantRating = rdr.GetInt32(3);
                restaurantCuisineId = rdr.GetInt32(4);
                restaurantSpecialty = rdr.GetString(5);

                Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCuisineId, restaurantAddress, restaurantRating, restaurantSpecialty, restaurantId);

                allCuisineRestaurants.Add(newRestaurant);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allCuisineRestaurants;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM restaurants;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void UpdateRestaurant(string newName, int newCuisineId, string newAddress, int newRating, string newSpecialty)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE restaurants SET name = @newName, cuisine_id = @newCuisineId, address = @newAddress, rating = @newRating, specialty = @newSpecialty WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);

            MySqlParameter cuisineId = new MySqlParameter();
            cuisineId.ParameterName = "@newCuisineId";
            cuisineId.Value = newCuisineId;
            cmd.Parameters.Add(cuisineId);

            MySqlParameter address = new MySqlParameter();
            address.ParameterName = "@newAddress";
            address.Value = newAddress;
            cmd.Parameters.Add(address);

            MySqlParameter rating = new MySqlParameter();
            rating.ParameterName = "@newRating";
            rating.Value = newRating;
            cmd.Parameters.Add(rating);

            MySqlParameter specialty = new MySqlParameter();
            specialty.ParameterName = "@newSpecialty";
            specialty.Value = newSpecialty;
            cmd.Parameters.Add(specialty);

            cmd.ExecuteNonQuery();
            _name = newName;
            _cuisine_id = newCuisineId;
            _address = newAddress;
            _rating = newRating;
            _specialty = newSpecialty;

            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }

}
