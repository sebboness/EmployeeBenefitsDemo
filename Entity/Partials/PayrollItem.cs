using PaylocityDemo.Model.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Entity
{
    public partial class PayrollItem : IPayrollLineItem
    {
        public decimal GetPayrollAmount()
        {
            return Amount;
        }

        public string GetPayrollDescription()
        {
            return Description;
        }
    }
}
