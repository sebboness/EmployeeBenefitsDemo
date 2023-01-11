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
    public class BenefitManager : BaseEntityManager<Benefit>, IBenefitManager
    {

        public BenefitManager(IRepository<Benefit> repository) : base(repository)
        {
        }

        public IQueryable<Benefit> GetAll(int organizationId)
        {
            return Repository.Filter(x => x.OrganizationId == organizationId
                && x.StatusId != (int)Status.Deleted);
        }
    }
}
