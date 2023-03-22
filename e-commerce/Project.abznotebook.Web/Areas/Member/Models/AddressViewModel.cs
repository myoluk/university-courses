using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Project.abznotebook.Web.Areas.Member.Models
{
    public class AddressViewModel
    {
        public string Title { get; set; }

        public string AddressLine { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Neighborhood { get; set; }

        public string PostalCode { get; set; }

    }
}
