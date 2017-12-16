using DrugInteractions.Data.Models.Brands;
using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Data.Models.SideEffects;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrugInteractions.Data.Models.Users
{
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public string Facebook { get; set; }

        public string LinkedIn { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime DateOfAddition { get; set; }

        public List<Drug> Drugs { get; set; } = new List<Drug>();

        public List<DrugGroup> DrugGroups { get; set; } = new List<DrugGroup>();

        public List<SideEffect> SideEffects { get; set; } = new List<SideEffect>();

        public List<SideEffectGroup> SideEffectGroups { get; set; } = new List<SideEffectGroup>();

        public List<Brand> Brands { get; set; } = new List<Brand>();
    }
}
