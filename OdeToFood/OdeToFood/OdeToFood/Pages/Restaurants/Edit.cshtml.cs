using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        // This attribute will only execute on a post operation
        [BindProperty]
        public Restaurant Restaurant { get; set; }
        // The type SelectListItem is required for the views' asp-items tag helper
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        // Remember, we can give the page model/controller additional services by adding them to the
        // constructor... as we have with IHtmlHelper
        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }

        // The '?' on the type declaration of the parameter means that it can be null

        public IActionResult OnGet(int? restaurantId)
        {
            // Don't forget that these Cuisines need to be populated on every type of HTTP request
            // This is because ASP.NetCore is stateless - you are responsible for setting up your state!
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            if (restaurantId.HasValue)
            {
                // Since we're using a nullable operator, we have to use .HasValue and if it does, use .Value to get it
                Restaurant = restaurantData.GetById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }
            // If restaurant is not found, redirect to the not found page
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            // Otherwise render the related view (Detail.cshtml)
            return Page();
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                // Don't forget that these Cuisines need to be populated on every type of HTTP request
                // This is because ASP.NetCore is stateless - you are responsible for setting up your state!
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }
            // This if/else is checking if the Restaurant has an ID.. if it does, then it is an edit... If it doesn't, then it's a new restaurant
            if (Restaurant.Id > 0)
            {
                restaurantData.Update(Restaurant);
            }
            else
            {
                restaurantData.Create(Restaurant);
            }
            restaurantData.Commit();
            // THis data is available ONLY DURING THE NEXT REQUEST... This allows the details page to look for it 
            // and render accordingly 
            TempData["Message"] = "Restaurant saved!";
            // On a successful post, redirect the user to the newly created restaurant's details page
            // here, we are creating a new anonymous object to indicate the id for routing
            // This is known as the 'POST-GET-Redirect Pattern
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}