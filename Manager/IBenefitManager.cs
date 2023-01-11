using PaylocityDemo.Entity;
using System.Linq;

namespace PaylocityDemo.Manager
{
    public interface IBenefitManager
    {
        IQueryable<Benefit> GetAll(int organizationId);
    }
}