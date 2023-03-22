using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Web.Base.Common.Models
{
    public class BillViewModel
    {
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
