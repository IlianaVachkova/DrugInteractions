using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Data.Models.Users;
using System;
using System.Collections.Generic;

namespace DrugInteractions.Data.Models.Brands
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string WebSite { get; set; }

        public User Admin { get; set; }

        public string AdminId { get; set; }

        public DateTime DateOfAddition { get; set; }

        public List<Drug> Drugs { get; set; }
    }
}