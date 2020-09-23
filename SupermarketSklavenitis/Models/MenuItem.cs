﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketSklavenitis.Models
{
    public class MenuItem
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sweeteners { get; set; }
        public enum ESweet { NA=0, NoSweeteners=1, Sugar=2, Stevia=3, OtherSweeteners=4 }
        public string Image { get; set; }

        [Display(Name="Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "SubCategory")]
        public int SubCategoryId { get; set; }

        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }


        [Range(1,int.MaxValue, ErrorMessage = "Price should be greater than ${1}")]
        public double Price { get; set; }
    }
}
