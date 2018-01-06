using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.Brands;
using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Data.Models.Users;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static DrugInteractions.Data.Models.DataConstants;

namespace DrugInteractions.Web.Areas.Repr.Models.Drugs
{
    public class DrugFormModel : IMapFrom<Drug>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DrugNameMinLength)]
        [MaxLength(DrugNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(DrugActiveSubstancesMinLength)]
        [Display(Name= "Active Substances")]
        public string ActiveSubstances { get; set; }

        public string Excipients { get; set; }

        [Display(Name= "Pharmaceutical Form")]
        public PharmaceuticalFormType PharmaceuticalForm { get; set; }

        public string Usage { get; set; }

        [Required]
        public string Description { get; set; }

        public User Representative { get; set; }

        public string RepresentativeId { get; set; }

        public DateTime DateOfAddition { get; set; }

        public DrugGroup DrugGroup { get; set; }

        [Display(Name = "Drug Group")]
        public int DrugGroupId { get; set; }

        public Brand Brand { get; set; }

        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; }

        public IEnumerable<SelectListItem> DrugGroups { get; set; }
    }
}
