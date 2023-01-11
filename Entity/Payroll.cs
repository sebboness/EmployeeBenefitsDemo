using System;
using System.Collections.Generic;

namespace PaylocityDemo.Entity
{
    public partial class Payroll
    {
        public Payroll()
        {
            PayrollItem = new HashSet<PayrollItem>();
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

        public virtual Employee Employee { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<PayrollItem> PayrollItem { get; set; }
    }
}
