using PaylocityDemo.Model.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Entity
{
    public partial class EmployeeBenefit : IPayrollLineItem
    {
        public decimal GetPayrollAmount()
        {
            return Benefit.Cost;
        }

        public string GetPayrollDescription()
        {
            return $"Benefits employee";
        }
    }
}
