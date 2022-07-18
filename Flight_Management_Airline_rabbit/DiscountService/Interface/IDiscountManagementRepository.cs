using DiscountService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscountService.Interface
{
    public interface IDiscountManagementRepository
    {
        bool AddDiscount(AddDiscountRequest addDiscountRequest);
        bool DeleteDiscount(int discountId);
        int ValidateDiscountCode(string discountCode);
        List<DiscountDetails> GetAllDiscount();

    }
}
