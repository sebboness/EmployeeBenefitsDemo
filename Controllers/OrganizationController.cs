using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaylocityDemo.Entity;
using PaylocityDemo.Manager;
using PaylocityDemo.Mapping;
using PaylocityDemo.Model.Benefit;
using PaylocityDemo.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Controllers
{
    [Produces("application/json")]
    [Route("organization")]
    public class OrganizationController : BaseController
    {
        private readonly IBenefitManager benefitManager;
        private readonly IBenefitDiscountManager benefitDiscountManager;

        public OrganizationController(IBenefitManager benefitManager, IBenefitDiscountManager benefitDiscountManager, AppDbContext dbContext) : base(dbContext)
        {
            this.benefitManager = benefitManager;
            this.benefitDiscountManager = benefitDiscountManager;
        }

        /// <summary>
        /// Returns an organization's types of benefits
        /// </summary>
        /// <param name="organizationId">The organization ID</param>
        /// <returns></returns>
        [HttpGet("{organizationId}/benefits")]
        public async Task<IActionResult> GetBenefits(int organizationId)
        {
            List<Benefit> benefits = await benefitManager.GetAll(organizationId).ToListAsync();
            List<BenefitResult> result = AutoMappings.Current.Mapper.Map<List<Benefit>, List<BenefitResult>>(benefits);
            return new ApiResult()
                    .WithData(result)
                    .ToJsonResult();
        }

        /// <summary>
        /// Returns an organization's types of discounts
        /// </summary>
        /// <param name="organizationId">The organization ID</param>
        /// <returns></returns>
        [HttpGet("{organizationId}/discounts")]
        public async Task<IActionResult> GetDiscounts(int organizationId)
        {
            List<BenefitDiscount> discounts = await benefitDiscountManager.GetAll(organizationId).ToListAsync();
            List<BenefitDiscountResult> result = AutoMappings.Current.Mapper.Map<List<BenefitDiscount>, List<BenefitDiscountResult>>(discounts);
            return new ApiResult()
                    .WithData(result)
                    .ToJsonResult();
        }
    }
}
