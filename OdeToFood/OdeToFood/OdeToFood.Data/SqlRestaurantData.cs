using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Restaurant Create(Restaurant newRestaurant)
        {
            // You can also add by referencing the table as well, but not necessary as ASP.NetCore can figure it out on its own
            // db.Restaurants.Add(newRestaurant);
            db.Add(newRestaurant);
            return newRestaurant;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if (restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return db.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            if (name == null)
            {
                return db.Restaurants.Select(r => r)
                .OrderBy(r => r.Name)
                .ToList();
            }
            return db.Restaurants.Select(r => r)
            .Where(r => r.Name.StartsWith(name))
            .OrderBy(r => r.Name)
            .ToList();
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            // The Attach method allows EF to track this restaurant that already
            // exists in the DB. Therefore, we can make changes to that restaurant object
            // and EF knows what is going on
            var entity = db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }
    }
}