using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Model.Payroll
{
    public interface IPayrollLineItem
    {
        /// <summary>
        /// Gets the payroll line item description
        /// </summary>
        /// <returns></returns>
        string GetPayrollDescription();

        /// <summary>
        /// Gets the payroll line item amount
        /// </summary>
        /// <returns></returns>
        decimal GetPayrollAmount();
    }
}
