using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Model.Benefit
{
    public class BenefitDiscountCalculatorFactory
    {
        public static BenefitDiscountCalculator GetCalculator(BenefitDiscountType discountType)
        {
            switch (discountType)
            {
                case BenefitDiscountType.Dependent:
                case BenefitDiscountType.Employee:
                    return new BenefitDiscountCalculator();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
