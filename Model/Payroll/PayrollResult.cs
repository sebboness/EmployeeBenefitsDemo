using AutoMapper;
using PaylocityDemo.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Model.Payroll
{
    public class PayrollResult : IHaveCustomMappings
    {
        public PayrollResult()
        {
            Items = new List<PayrollItemResult>();
        }

        public int Id { get; set; }
        public int StatusId { get; set; }
        public int OrganizationId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }

        public virtual List<PayrollItemResult> Items { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Entity.Payroll, PayrollResult>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Items, opt => opt.MapFrom(s => s.PayrollItem));
        }
    }

    public class PayrollItemResult : IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Entity.PayrollItem, PayrollItemResult>()
                .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Amount))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
        }
    }
}
