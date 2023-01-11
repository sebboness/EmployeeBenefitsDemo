using PaylocityDemo.Model.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Entity
{
    public partial class Employee : IPayrollLineItem
    {
        public decimal GetPayrollAmount()
        {
            return Salary;
        }

        public string GetPayrollDescription()
        {
            return $"Employee {FirstName} {LastName} base salary";
        }
    }
}
