using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static DrugInteractions.Data.Models.DataConstants;

namespace DrugInteractions.Data.Models.Brands
{
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        [MinLength(BrandNameMinLength)]
        [MaxLength(BrandNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(BrandWebSiteMinLength)]
        [MaxLength(BrandWebSiteMaxLength)]
        public string WebSite { get; set; }

        public User Admin { get; set; }

        public string AdminId { get; set; }

        public DateTime DateOfAddition { get; set; }

        public List<Drug> Drugs { get; set; }
    }
}