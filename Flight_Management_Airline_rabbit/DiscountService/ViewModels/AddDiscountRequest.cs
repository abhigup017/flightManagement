using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscountService.ViewModels
{
    public class AddDiscountRequest
    {
        public string DiscountCode { get; set; }
        public DateTime DiscountExpiryDate { get; set; }
        public int DiscountValue { get; set; }
    }
}
