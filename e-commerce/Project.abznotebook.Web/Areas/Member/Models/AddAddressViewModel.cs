using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.abznotebook.Web.Areas.Member.Models
{
    public class AddAddressViewModel
    {

        [Required(ErrorMessage = "Doldurulması zorunlu alan.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Doldurulması zorunlu alan.")]
        public string AddressLine { get; set; }

        [Required(ErrorMessage = "Doldurulması zorunlu alan.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Doldurulması zorunlu alan.")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "Doldurulması zorunlu alan.")]
        public string District { get; set; }

        [Required(ErrorMessage = "Doldurulması zorunlu alan.")]
        public string PostalCode { get; set; }
    }
}
