using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Web.Areas.Admin.Models
{
    public class AddProductViewModel
    {
        public string SKU { get; set; } = Guid.NewGuid().ToString();
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
        public bool IsAvailable { get; set; } = true;
        public string Description { get; set; }
        public IFormFile Image1 { get; set; }
        public List<IFormFile> Images { get; set; }

    }
}
