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
        public IActionResult OnGet(int restaurantId)
        {
            // Don't forget that these Cuisines need to be populated on every type of HTTP request
            // This is because ASP.NetCore is stateless - you are responsible for setting up your state!
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            Restaurant = restaurantData.GetById(restaurantId);
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
            if(ModelState.IsValid)
            {
                // Don't forget that these Cuisines need to be populated on every type of HTTP request
                // This is because ASP.NetCore is stateless - you are responsible for setting up your state!
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                restaurantData.Update(Restaurant);
            }
            restaurantData.Commit();
            return Page();
            //return RedirectToPage("./List");
        }
    }
}