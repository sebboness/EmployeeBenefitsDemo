using System;
using System.Collections.Generic;

namespace PaylocityDemo.Entity
{
    public partial class BenefitDiscount
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int OrganizationId { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Expression { get; set; }
        public decimal PercentOff { get; set; }

        public virtual Organization Organization { get; set; }
    }
}
