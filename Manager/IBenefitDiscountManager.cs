using PaylocityDemo.Entity;
using System.Linq;

namespace PaylocityDemo.Manager
{
    public interface IBenefitDiscountManager
    {
        IQueryable<BenefitDiscount> GetAll(int organizationId);
    }
}