using System;
using System.Collections.Generic;

namespace PaylocityDemo.Entity
{
    public partial class Dependent
    {
        public Dependent()
        {
            DependentBenefit = new HashSet<DependentBenefit>();
        }

        public int Id { get; set; }
        public int StatusId { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<DependentBenefit> DependentBenefit { get; set; }
    }
}
