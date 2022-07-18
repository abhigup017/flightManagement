using DiscountService.Interface;
using DiscountService.Models;
using DiscountService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscountService.Service
{
    public class DiscountManagementRepository : IDiscountManagementRepository
    {

        private readonly FlightManagementContext _dbContext;

        public DiscountManagementRepository(FlightManagementContext flightManagementContext)
        {
            _dbContext = flightManagementContext;
        }
        
        #region Add Discount
        /// <summary>
        /// Adds a discount coupon
        /// </summary>
        /// <param name="addDiscountRequest"></param>
        /// <returns>A boolean flag indicating if discount coupon is successfully added or not</returns>
        public bool AddDiscount(AddDiscountRequest addDiscountRequest)
        {
            bool isInserted = false;

            try
            {
                if(_dbContext.Discounts.Any(x => x.DiscountCode == addDiscountRequest.DiscountCode))
                {
                    throw new Exception("Discount code already exists");
                }

                Discount discount = new Discount
                {
                    DiscountCode = addDiscountRequest.DiscountCode,
                    DiscountExpiryDate = addDiscountRequest.DiscountExpiryDate,
                    DiscountValue = addDiscountRequest.DiscountValue
                };

                _dbContext.Discounts.Add(discount);
                _dbContext.SaveChanges();
                isInserted = true;

            }
            catch(Exception ex)
            {
                throw ex;
            }

            return isInserted;
        }
        #endregion

        #region Delete Discount
        /// <summary>
        /// Deletes the discount coupon
        /// </summary>
        /// <param name="discountId"></param>
        /// <returns>A boolean flag indicating if discount coupon is successfully deleted or not</returns>
        public bool DeleteDiscount(int discountId)
        {
            bool isDeleted = false;

            try
            {
                var discount = _dbContext.Discounts.Where(x => x.DiscountId == discountId).FirstOrDefault();
                _dbContext.Discounts.Remove(discount);
                _dbContext.SaveChanges();
                isDeleted = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return isDeleted;
        }
        #endregion

        #region Get all Discount coupons
        /// <summary>
        /// Gets all the discount coupons
        /// </summary>
        /// <returns>List of Discount coupons</returns>
        public List<DiscountDetails> GetAllDiscount()
        {
            List<DiscountDetails> discountDetails = new List<DiscountDetails>();

            try
            {
                discountDetails = (from discount in _dbContext.Discounts
                                   select new DiscountDetails
                                   {
                                       DiscountId = discount.DiscountId,
                                       DiscountExpiryDate = discount.DiscountExpiryDate,
                                       DiscountCode = discount.DiscountCode,
                                       DiscountValue = discount.DiscountValue
                                   }).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return discountDetails;
        }
        #endregion

        #region Validate Discount code
        /// <summary>
        /// Vaildates the discount code
        /// </summary>
        /// <param name="discountCode"></param>
        /// <returns>The discount value if its a valid code</returns>
        public int ValidateDiscountCode(string discountCode)
        {
            int discountValue = 0;
            
            try
            {
                var discount = _dbContext.Discounts.Where(x => x.DiscountCode == discountCode).FirstOrDefault();

                if(discount == null || discount.DiscountId <= 0)
                {
                    throw new Exception("Discount code is invalid");
                }
                else if(DateTime.Now.Date > discount.DiscountExpiryDate.Date)
                {
                    throw new Exception("Discount code has expired");
                }

                discountValue = discount.DiscountValue;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return discountValue;
        }
        #endregion
    }
}
