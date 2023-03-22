using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Project.abznotebook.Web.Areas.Admin.Models
{
    public class EditProductViewModel
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Vendor { get; set; }
        public int? CategoryId { get; set; }
#nullable enable
        public string? ProcessorVendor { get; set; }
        public string? ProcessorModel { get; set; }
        public string? ProcessorType { get; set; }
        public string? GraphicsCard { get; set; }
        public string? DiscCapacity { get; set; }
        public string? MemoryCapacity { get; set; }
        public decimal? RealPrice { get; set; }
#nullable disable
        public decimal UnitPrice { get; set; }
        public int UnitInStock { get; set; }
        public bool IsAvailable { get; set; }
        public string Description { get; set; }

        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }


    }
}
