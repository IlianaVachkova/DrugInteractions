using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.Brands;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace DrugInteractions.Services.Models
{
    public class BrandChartServiceModel : IMapFrom<Brand>, IHaveCustomMapping
    {
        public string Name { get; set; }

        public int DrugsCount { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Brand, BrandChartServiceModel>()
                .ForMember(d => d.DrugsCount, cfg => cfg.MapFrom(d => d.Drugs.Count));

        }
    }
}
