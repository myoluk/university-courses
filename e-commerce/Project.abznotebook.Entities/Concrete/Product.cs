using System;
using System.Collections.Generic;
using System.Text;
using Project.abznotebook.Entities.Interfaces;

namespace Project.abznotebook.Entities.Concrete
{

    public class Product : ITable
    {
        public int Id { get; set; } // CommonFeature
        public string SKU { get; set; } // CommonFeature
        public string Name { get; set; } // CommonFeature
        public decimal? RealPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; } // CommonFeature
        public string Vendor { get; set; } // CommonFeature
        public string ProcessorVendor { get; set; }
        public string ProcessorType { get; set; }
        public string GraphicsCard { get; set; }
        public string MemoryCapacity { get; set; }
        public string DiscCapacity { get; set; }
        public string Image1 { get; set; } // CommonFeature
        public string Image2 { get; set; } // CommonFeature
        public string Image3 { get; set; } // CommonFeature
        public int UnitInStock { get; set; }
        public bool IsAvailable { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public List<OrderDetail> OrderDetails { get; set; }


        public int? CategoryId { get; set; }
        public Category Category { get; set; }

    }

}
