using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration configuartion;

        public ListModel(IConfiguration configuartion)
        {
            this.configuartion = configuartion;
        }

        public string Message { get; set; }
        public void OnGet()
        {
            Message = configuartion["Message"];
        }
    }
}