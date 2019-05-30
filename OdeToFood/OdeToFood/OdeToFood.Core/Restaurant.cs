using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OdeToFood.Core
{
    public class Restaurant
    {
        // This attribute will require that this property always be set
        // This is useful for HTTP requests where data is being entered/edited
        [Required, StringLength(80)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Location { get; set; }
        public int Id { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
