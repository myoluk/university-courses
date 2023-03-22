using System;
using System.Collections.Generic;
using System.Text;
using Project.abznotebook.Entities.Interfaces;

namespace Project.abznotebook.Entities.Concrete
{
    public class Category : ITable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}
