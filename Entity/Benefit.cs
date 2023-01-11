using System;
using System.Collections.Generic;

namespace PaylocityDemo.Entity
{
    public partial class Benefit
    {
        public Benefit()
        {
            DependentBenefit = new HashSet<DependentBenefit>();
            EmployeeBenefit = new HashSet<EmployeeBenefit>();
        }

        public int Id { get; set; }
        public int StatusId { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual ICollection<DependentBenefit> DependentBenefit { get; set; }
        public virtual ICollection<EmployeeBenefit> EmployeeBenefit { get; set; }
    }
}
