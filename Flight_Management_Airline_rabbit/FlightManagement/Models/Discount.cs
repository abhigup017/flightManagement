using System;
using System.Collections.Generic;

#nullable disable

namespace AirlineService.Models
{
    public partial class Discount
    {
        public int DiscountId { get; set; }
        public string DiscountCode { get; set; }
        public DateTime DiscountExpiryDate { get; set; }
        public int DiscountValue { get; set; }
    }
}
