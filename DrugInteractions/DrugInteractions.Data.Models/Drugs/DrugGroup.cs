using DrugInteractions.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrugInteractions.Data.Models.Drugs
{
    public class DrugGroup
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public User Admin { get; set; }

        public string AdminId { get; set; }

        public DateTime DateOfAddition { get; set; }

        public List<Drug> Drugs { get; set; } = new List<Drug>();
    }
}
