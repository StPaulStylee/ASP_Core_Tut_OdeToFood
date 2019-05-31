using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Create(Restaurant newRestaurant);
        int Commit();
    }

    // This will be for development only; setting us up to easily tranfser this to DB access
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> Restaurants;
        public InMemoryRestaurantData()
        {
            Restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Jeff's Pizza", Location = "Saint Paul, MN", Cuisine = CuisineType.Italian },
                new Restaurant { Id = 2, Name = "Emily's Curry", Location = "Arlington, VA", Cuisine = CuisineType.Indian},
                new Restaurant { Id = 3, Name = "Graham's Crackers", Location = "Minneapolis, MN", Cuisine = CuisineType.Mexican}
            };
        }
        // The parameter is optional becaue of the default value of null
        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            if (name == null)
            {
                return Restaurants.Select(r => r)
                .OrderBy(r => r.Id)
                .ToList();
            }
            return Restaurants.Select(r => r)
            .Where(r => r.Name.StartsWith(name))
            .OrderBy(r => r.Id)
            .ToList();
        }

        // Returns a restaurant that has the given id or the default, which is null
        public Restaurant GetById(int id)
        {
            return Restaurants.SingleOrDefault(restaurant => restaurant.Id == id);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = Restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Create(Restaurant newRestaurant)
        {
            Restaurants.Add(newRestaurant);
            // This is for development only to simulate the primary key that would 
            // be add automatically by SQL Server
            // This is a LINQ query that finds the highest ID in our list and adds one to it
            newRestaurant.Id = Restaurants.Max(r => r.Id ) + 1;
            return newRestaurant;
        }
    }
}
