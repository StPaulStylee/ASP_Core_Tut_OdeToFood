using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DetailModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        // This attribute allows the Message property to be set if there is TempData available
        // We can then check for this in our view 
        [TempData]
        public string Message { get; set; }
        public DetailModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        public Restaurant Restaurant { get; set; }
        // Setting the return type to IActionResult lets us decide what pages to show depending on what our
        // logic determines
        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = restaurantData.GetById(restaurantId);
            // If restaurant is not found, redirect to the not found page
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            // Otherwise render the related view (Detail.cshtml)
            return Page();
        }
    }
}