using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Web.Base.Common.Models
{
    public class OrderDetailViewModel : OrderSummaryViewModel
    {
        public List<OrderDetail> OrderDetails { get; set; }
        public List<BillViewModel> Bill { get; set; }

        public int AddressId { get; set; }
        public string AddressLine { get; set; }
        public string AddressNeighborhood { get; set; }
        public string AddressDistrict { get; set; }
        public string AddressCity { get; set; }
        public string AddressPostalCode { get; set; }

        public string ShipperCompanyName { get; set; }
        public string ShipperPhone { get; set; }

    }
}
