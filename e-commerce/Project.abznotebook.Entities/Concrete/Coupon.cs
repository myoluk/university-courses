using System;
using Project.abznotebook.Entities.Interfaces;

namespace Project.abznotebook.Entities.Concrete
{
    public class Coupon : ITable
    {
        public int Id { get; set; }
        public string Code { get; set; } //Coupon Code
        public DateTime EndDate { get; set; }
        public int DiscountPercentage { get; set; }
        public bool IsActive { get; set; }

    }
}