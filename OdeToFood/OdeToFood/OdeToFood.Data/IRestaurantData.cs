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
        // The parameter is option becaue of the default value of null
        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return Restaurants.Select(r => r)
                .Where(string.IsNullOrEmpty(name) || (r) => r.Name.StartsWith(name))
                .OrderBy(r => r.Id)
                .ToList();
        } 
    }
}
