using System;
using System.Collections.Generic;

namespace PaylocityDemo.Entity
{
    public partial class DependentBenefit
    {
        public int DependentId { get; set; }
        public int BenefitId { get; set; }

        public virtual Benefit Benefit { get; set; }
        public virtual Dependent Dependent { get; set; }
    }
}
