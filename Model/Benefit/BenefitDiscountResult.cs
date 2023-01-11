using AutoMapper;
using PaylocityDemo.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Model.Benefit
{
    public class BenefitDiscountResult : IHaveCustomMappings
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Expression { get; set; }
        public decimal PercentOff { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Entity.BenefitDiscount, BenefitDiscountResult>();
        }
    }
}
