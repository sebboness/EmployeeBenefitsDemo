using System;
using System.Collections.Generic;

namespace PaylocityDemo.Entity
{
    public partial class PayrollItem
    {
        public int Id { get; set; }
        public int PayrollId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public virtual Payroll Payroll { get; set; }
    }
}
