using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.abznotebook.Business.Interfaces;
using Project.abznotebook.Entities.Concrete;

namespace Project.abznotebook.Web.Areas.Admin.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountOfProduct { get; set; }

        //public List<Product> Products { get; set; }
    }
}
