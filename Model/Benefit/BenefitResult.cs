using AutoMapper;
using PaylocityDemo.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Model.Benefit
{
    public class BenefitResult : IHaveCustomMappings
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Entity.Benefit, BenefitResult>();
        }
    }
}
