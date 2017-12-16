using DrugInteractions.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrugInteractions.Data.Models.SideEffects
{
    public class SideEffectGroup
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public User Admin { get; set; }

        public string AdminId { get; set; }

        public DateTime DateOfAddition { get; set; }

        public List<SideEffect> SideEffects { get; set; } = new List<SideEffect>();
    }
}