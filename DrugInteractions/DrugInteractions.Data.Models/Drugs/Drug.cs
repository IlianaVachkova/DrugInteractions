using System;
using DrugInteractions.Data.Models.Users;
using DrugInteractions.Data.Models.Brands;
using System.Collections.Generic;
using DrugInteractions.Data.Models.IntermediateTables;
using System.ComponentModel.DataAnnotations;

using static DrugInteractions.Data.Models.DataConstants;

namespace DrugInteractions.Data.Models.Drugs
{
    public class Drug
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DrugNameMinLength)]
        [MaxLength(DrugNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(DrugActiveSubstancesMinLength)]
        public string ActiveSubstances { get; set; }

        public string Excipients { get; set; }

        public PharmaceuticalFormType PharmaceuticalForm { get; set; }

        public string Usage { get; set; }

        public string Description { get; set; }

        public User Representative { get; set; }

        public string RepresentativeId { get; set; }

        public DateTime DateOfAddition { get; set; }

        public DrugGroup DrugGroup { get; set; }

        public int DrugGroupId { get; set; }

        public Brand Brand { get; set; }

        public int BrandId { get; set; }

        public List<DrugSideEffect> SideEffects { get; set; } = new List<DrugSideEffect>();
    }
}