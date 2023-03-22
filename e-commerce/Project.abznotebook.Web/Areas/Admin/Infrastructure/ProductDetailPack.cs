using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.abznotebook.Web.Areas.Admin.Infrastructure
{
    public static class ProductDetailPack
    {
        public static List<string> MemoryList = new List<string>()
        {
            new string("2 GB"),
            new string("4 GB"),
            new string("6 GB"),
            new string("8 GB"),
            new string("12 GB"),
            new string("16 GB"),
            new string("32 GB"),
            new string("64 GB"),
            new string("128 GB"),
            new string("256 GB")
        };

        public static List<string> VendorList = new List<string>()
        {
            new string("Acer"),
            new string("Apple"),
            new string("Asus"),
            new string("Casper"),
            new string("Dell"),
            new string("HP"),
            new string("Monster"),
            new string("Lenovo"),
            new string("MSI")
        };

        public static List<string> ProcessorVendorList = new List<string>()
        {
            new string("Intel"),
            new string("AMD"),
        };

        public static List<string> DiscCapacityList = new List<string>()
        {
            new string("128 GB"),
            new string("256 GB"),
            new string("512 GB"),
            new string("1 TB"),
            new string("2 TB"),
            new string("3 TB"),
            new string("4 TB"),
            new string("5 TB"),
            new string("10 TB")
        };
    }
}
