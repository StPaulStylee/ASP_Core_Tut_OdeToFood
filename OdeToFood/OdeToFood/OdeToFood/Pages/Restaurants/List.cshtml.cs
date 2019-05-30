using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration configuartion;
        private readonly IRestaurantData restaurantData;

        // We are able to pass IRestaurantData here because we added it to the services in Startup.cs
        public ListModel(IConfiguration configuartion, IRestaurantData restaurantData)
        {
            this.configuartion = configuartion;
            this.restaurantData = restaurantData;
        }

        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }

        // Here we are passing the name of the input propery we want to target from our view, this
        // is done for model binding purposes. The param name must match the name of the input
        public void OnGet(string searchTerm)
        {
            // This gives us data from the HttpRequest that has been invoed
            //HttpContext.Request...

            // BUt we want to use this for this particular instance...Model Binding - The goal is to move 
            // data from a request into an Input Model Binder

            Message = configuartion["Message"];
            Restaurants = restaurantData.GetRestaurantsByName(searchTerm);
        }
    }
}