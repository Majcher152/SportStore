using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Please give product's name.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please give product's description.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please give positive price.")]
        [Display(Name = "Name")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please give product's category.")]
        [Display(Name = "Category")]
        public string Category { get; set; }

        //Currency
        public readonly CultureInfo Poland =
            CultureInfo.GetCultureInfo("pl-PL");

    }
}
