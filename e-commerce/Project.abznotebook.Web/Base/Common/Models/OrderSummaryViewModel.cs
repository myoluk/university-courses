using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.abznotebook.Web.Base.Common.Models
{
    public class OrderSummaryViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public string ShipStatus { get; set; }
        public string AllowStatus { get; set; }
        public string CustomerFullName { get; set; }
        public string PaymentMethod { get; set; }

        public List<string> ProductImages { get; set; }
        public int ProductCount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
