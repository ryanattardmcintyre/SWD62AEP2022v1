using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    //Domain: will be a project referenced by all the other layers and will
    //        hold the data containers i.e. Models

    //Note: Code first Approach

    //EFCore is the module that will be used to automatically shape/model the database
    public class Item
    {
        [Key] //these attributes are for databse generation
        public int Id { get; set; }
        
        [StringLength(100)]
        [Required]
        public string Name { get; set; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string PhotoPath { get; set; }
    }
}
