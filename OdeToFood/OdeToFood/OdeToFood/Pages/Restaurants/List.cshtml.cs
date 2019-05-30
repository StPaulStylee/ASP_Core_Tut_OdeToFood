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
        // These can be thought of as "output models" because we use them to display data (output) 
        // to the view
        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }
        // This attribute tells ASP.NetCore to use this property as an output and input model
        // During the executiong of an HTTP request this property will be set from info in the request
        // We add the "SupportsGet"  flag because the default behavior of the attr is to only execute on a POST request
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        private readonly IConfiguration configuartion;
        private readonly IRestaurantData restaurantData;

        // We are able to pass IRestaurantData here because we added it to the services in Startup.cs
        public ListModel(IConfiguration configuartion, IRestaurantData restaurantData)
        {
            this.configuartion = configuartion;
            this.restaurantData = restaurantData;
        }

        // Here we are passing the name of the input propery we want to target from our view, this
        // is done for model binding purposes. The param name must match the name of the input
        // This parameter can be used as an input model because we use input from the user/view to perform logic
        //Note: The param has been removed and we are not using the public property SearchTerm using BindProperty
        // The propery must also match the exact name of the input
        public void OnGet()
        {
            // This gives us data from the HttpRequest that has been invoked
            //HttpContext.Request...

            // BUt we want to use this for this particular instance...Model Binding - The goal is to move 
            // data from a request into an Input Model Binder

            Message = configuartion["Message"];
            Restaurants = restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}