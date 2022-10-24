using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.ViewModels
{
    public class ItemViewModel
    {
 
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string PhotoPath { get; set; }

        public int Stock { get; set; }
    }
}
