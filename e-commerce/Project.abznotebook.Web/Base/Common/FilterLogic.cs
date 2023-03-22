using System.Linq;
using Project.abznotebook.Web.Models;

namespace Project.abznotebook.Web.Base.Common
{
    public static class FilterLogic
    {
        public static ProductListViewModel FilterByModel(ProductListViewModel productList, FilterViewModel model)
        {
            if (!string.IsNullOrEmpty(model.SelectedVendor))
            {
                productList.Products = productList.Products.Where(I => I.Vendor.Contains(model.SelectedVendor));
                productList.FilterTypes.Memories = productList.Products.Select(I => I.MemoryCapacity).Distinct()
                    .OrderBy(I => I).ToList();
                productList.FilterTypes.Processors = productList.Products.Select(I => I.ProcessorVendor).Distinct()
                    .OrderBy(I => I).ToList();
                productList.FilterTypes.Vendors =
                    productList.Products.Select(I => I.Vendor).Distinct().OrderBy(I => I).ToList();
            }

            if (!string.IsNullOrEmpty(model.SelectedMemory))
            {
                productList.Products = productList.Products.Where(I => I.MemoryCapacity == model.SelectedMemory);
                productList.FilterTypes.Processors = productList.Products.Select(I => I.ProcessorVendor).Distinct()
                    .OrderBy(I => I).ToList();
                productList.FilterTypes.Vendors =
                    productList.Products.Select(I => I.Vendor).Distinct().OrderBy(I => I).ToList();
                productList.FilterTypes.Memories =
                    productList.Products.Select(I => I.MemoryCapacity).Distinct().OrderBy(I => I).ToList();
            }

            if (!string.IsNullOrEmpty(model.SelectedProcessor))
            {
                productList.Products = productList.Products.Where(I => I.ProcessorVendor == model.SelectedProcessor);
                productList.FilterTypes.Vendors =
                    productList.Products.Select(I => I.Vendor).Distinct().OrderBy(I => I).ToList();
                productList.FilterTypes.Processors =
                    productList.Products.Select(I => I.ProcessorVendor).Distinct().OrderBy(I => I).ToList();
            }


            return productList;
        }
    }
}