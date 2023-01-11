using System;
using System.Collections.Generic;

namespace PaylocityDemo.Entity
{
    public partial class EmployeeBenefit
    {
        public int EmployeeId { get; set; }
        public int BenefitId { get; set; }

        public virtual Benefit Benefit { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
