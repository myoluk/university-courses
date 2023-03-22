using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Web.Areas.Member.Models
{
    public class UserOrderDetailViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerFullName { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }


        public int ShipperId { get; set; }
        public DateTime ShipDate { get; set; }
        public bool IsShipped { get; set; }
        public string ShipperName { get; set; }
        public string ShipTrackNumber { get; set; }

        public int ProductId { get; set; }
        //public int NumberOfPieces { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
