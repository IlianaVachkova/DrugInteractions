using DrugInteractions.Data.Models.IntermediateTables;
using DrugInteractions.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrugInteractions.Data.Models.SideEffects
{
    public class SideEffect
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public User Admin { get; set; }

        public string AdminId { get; set; }

        public DateTime DateOfAddition { get; set; }

        public SideEffectGroup SideEffectGroup { get; set; }

        public int SideEffectGroupId { get; set; }

        public List<DrugSideEffect> Drugs { get; set; } = new List<DrugSideEffect>();
    }
}
