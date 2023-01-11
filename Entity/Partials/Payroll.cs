using PaylocityDemo.Model.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Entity
{
    public partial class Payroll
    {

        /// <summary>
        /// Adds the given line item to this payroll object
        /// </summary>
        /// <param name="item">The line item to add</param>
        public void AddLineItem(IPayrollLineItem item)
        {
            AddLineItem(item, 1);
        }

        /// <summary>
        /// Adds the given line item to this payroll object but negates the amount
        /// </summary>
        /// <param name="item">The line item to add</param>
        public void AddDeductionItem(IPayrollLineItem item)
        {
            AddLineItem(item, -1);
        }

        /// <summary>
        /// Adds the given line item to this payroll object
        /// </summary>
        /// <param name="item">The line item to add</param>
        /// <param name="multiplier">multiplies amount times this number</param>
        private void AddLineItem(IPayrollLineItem item, int multiplier)
        {
            PayrollItem.Add(new PayrollItem()
            {
                Description = item.GetPayrollDescription(),
                Amount = multiplier * item.GetPayrollAmount(),
            });
        }
    }
}
