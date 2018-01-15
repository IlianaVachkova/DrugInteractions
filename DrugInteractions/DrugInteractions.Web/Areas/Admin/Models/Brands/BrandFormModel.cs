using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.Brands;
using DrugInteractions.Data.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;

using static DrugInteractions.Data.Models.DataConstants;

namespace DrugInteractions.Web.Areas.Admin.Models.Brands
{
    public class BrandFormModel : IMapFrom<Brand>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(BrandNameMinLength)]
        [MaxLength(BrandNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(BrandWebSiteMinLength)]
        [MaxLength(BrandWebSiteMaxLength)]
        [Url]
        //*supported formats: google.com, www.google.com, https://www.google.com ,https://www.google.com */
        //[RegularExpression(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$", ErrorMessage ="Invalid format for a website.")]
        public string WebSite { get; set; }

        public User Admin { get; set; }

        public string AdminId { get; set; }

        public DateTime DateOfAddition { get; set; }

        public string StatusMessage { get; set; }
    }
}
