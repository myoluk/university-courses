using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Web.Models
{
    public class FilterViewModel
    {
        public List<string> Vendors { get; set; }
        public List<string> Memories { get; set; }
        public List<string> Processors { get; set; }
        public string SelectedVendor { get; set; }
        public string SelectedMemory { get; set; }
        public string SelectedProcessor { get; set; }
    }
}
