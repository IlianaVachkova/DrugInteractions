using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.Drugs;
using AutoMapper;

namespace DrugInteractions.Services.Models
{
    public class DrugChartServiceModel : IMapFrom<Drug>, IHaveCustomMapping
    {
        public string Name { get; set; }

        public int SideEffectsCont { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Drug, DrugChartServiceModel>()
                .ForMember(d=>d.SideEffectsCont, cfg=>cfg.MapFrom(d=>d.SideEffects.Count));
        }
    }
}
