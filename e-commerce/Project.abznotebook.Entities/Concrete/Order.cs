using System;
using System.Collections.Generic;
using System.Text;
using Project.abznotebook.Entities.Interfaces;

namespace Project.abznotebook.Entities.Concrete
{
    public class Order : ITable
    {
        public int Id { get; set; }
        public Guid OrderNumber { get; set; } = Guid.NewGuid();
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public bool IsShipped { get; set; } = false;
        public bool IsAllowed { get; set; } = false;
        public List<OrderDetail> OrderDetails { get; set; }
        
        public int CustomerId { get; set; }
        public AppUser Customer { get; set; }

        public int ShipperId { get; set; }
        public Shipper Shipper { get; set; }

        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
