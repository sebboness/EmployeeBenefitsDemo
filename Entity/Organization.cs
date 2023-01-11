using System;
using System.Collections.Generic;

namespace PaylocityDemo.Entity
{
    public partial class Organization
    {
        public Organization()
        {
            Benefit = new HashSet<Benefit>();
            BenefitDiscount = new HashSet<BenefitDiscount>();
            Employee = new HashSet<Employee>();
            Payroll = new HashSet<Payroll>();
        }

        public int Id { get; set; }
        public int StatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Benefit> Benefit { get; set; }
        public virtual ICollection<BenefitDiscount> BenefitDiscount { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Payroll> Payroll { get; set; }
    }
}
