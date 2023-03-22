using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Web.Models
{
    public class OrderViewModel
    {
        [BindRequired]
        public int AddressId { get; set; }

        [BindRequired]
        public int ShipperId { get; set; }

        [BindRequired]
        public int PaymentId { get; set; }
        public int? LinesCount { get; set; }

        public List<Address> Addresses { get; set; }

        public SelectList AddressCollection { get; set; }
        public SelectList PaymentCollection { get; set; }
        public SelectList ShipperCollection { get; set; }
    }
}
