using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.ViewModels
{
    //is a selection of the required properties to be used by the presentation (interface) layer
    public class CreateItemViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }
        
        [Required(AllowEmptyStrings =false, ErrorMessage ="Name cannot be blank")]
        public string Name { get; set; }
       
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
             
        [Range(1, 10000, ErrorMessage ="Range is between 1 and 10000")]
        public double Price { get; set; }

        public string Description { get; set; }

        [Display(Name="Photo")]
        public string PhotoPath { get; set; }

        public int Stock { get; set; }
    }
}
