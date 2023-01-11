using Microsoft.EntityFrameworkCore;
using PaylocityDemo.Data;
using PaylocityDemo.Entity;
using PaylocityDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Manager
{
    public class BenefitDiscountManager : BaseEntityManager<BenefitDiscount>, IBenefitDiscountManager
    {

        public BenefitDiscountManager(IRepository<BenefitDiscount> repository) : base(repository)
        {
        }

        public IQueryable<BenefitDiscount> GetAll(int organizationId)
        {
            return Repository.Filter(x => x.OrganizationId == organizationId
                && x.StatusId != (int)Status.Deleted);
        }
    }
}
