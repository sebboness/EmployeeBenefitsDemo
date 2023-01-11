using Microsoft.EntityFrameworkCore;
using PaylocityDemo.Data;
using PaylocityDemo.Entity;
using PaylocityDemo.Model;
using PaylocityDemo.Model.Benefit;
using PaylocityDemo.Model.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Manager
{
    public class PayrollManager : BaseEntityManager<Payroll>, IPayrollManager
    {
        // For simplicity, define a standard payroll tax rate of 25%
        public static decimal StandardTaxRate = 0.25m;

        public PayrollManager(IRepository<Payroll> repository) : base(repository)
        {
        }

        public IQueryable<Payroll> GetDraft(int payrollId)
        {
            return Repository.Filter(
                    x => x.Id == payrollId
                    && x.StatusId == (int)Status.Draft)
                .Include(x => x.PayrollItem);
        }

        public IQueryable<Payroll> GetDraft(int employeeId, DateTime fromDate, DateTime toDate)
        {
            return Repository.Filter(
                    x => x.EmployeeId == employeeId
                    && x.FromDate == fromDate.Date
                    && x.ToDate == toDate.Date
                    && x.StatusId == (int)Status.Draft)
                .Include(x => x.PayrollItem);
        }

        public IQueryable<Payroll> GetDrafts(int organizationId)
        {
            return Repository.Filter(
                    x => x.OrganizationId == organizationId
                    && x.StatusId == (int)Status.Draft)
                .Include(x => x.PayrollItem);
        }

        public Payroll SaveDraft(Employee employee, List<BenefitDiscount> orgDiscounts, PayrollDraftBindingModel model)
        {
            Payroll payroll = new Payroll();
            payroll.StatusId = (int)Status.Draft;
            payroll.OrganizationId = employee.OrganizationId;
            payroll.Employee = employee;
            payroll.FromDate = model.FromDate;
            payroll.ToDate = model.ToDate;
            payroll.CreatedOn = DateTime.Now.ToUniversalTime();

            // Prepare relevant payroll items such as benefits and discounts
            PreparePayrollItems(payroll, employee, orgDiscounts);

            // Save the payroll to the database
            Repository.Create(payroll);

            return payroll;
        }

        /// <summary>
        /// Prepares a payroll object with line items from the given employee.
        /// Calculates an employee's net pay based on their selected dependents, benefits, discounts, and tax
        /// </summary>
        /// <param name="payroll">The payroll object for which to prepare line items</param>
        /// <param name="employee">The employee record from which to get selected benefit information etc.</param>
        protected void PreparePayrollItems(Payroll payroll, Employee employee, List<BenefitDiscount> orgDiscounts)
        {
            payroll.GrossPay = employee.Salary;
            payroll.NetPay = employee.Salary;

            // Add employee's base salary as the starting line item
            payroll.AddLineItem(employee);

            // Add employee's benefits if any and deduct from base salary
            foreach (EmployeeBenefit employeeBenefit in employee.EmployeeBenefit)
            {
                payroll.AddDeductionItem(employeeBenefit);
                payroll.NetPay -= employeeBenefit.GetPayrollAmount();

                // Apply any discounts if employee qualifies
                var discounts = orgDiscounts.FindAll(x => x.TypeId == (int)BenefitDiscountType.Employee);
                var calculator = BenefitDiscountCalculatorFactory.GetCalculator(BenefitDiscountType.Employee);
                foreach (BenefitDiscount discount in discounts)
                {
                    if (calculator.Qualifies(employee, discount))
                    {
                        IPayrollLineItem discountItem = calculator.CalculateDiscountItem(employeeBenefit.Benefit, discount);
                        payroll.AddLineItem(discountItem);
                        payroll.NetPay += discountItem.GetPayrollAmount();
                    }
                }
            }

            // Add employee's depedents' benefits if any and deduct from base salary
            foreach (Dependent dependent in employee.Dependent)
            {
                foreach (DependentBenefit dependentBenefit in dependent.DependentBenefit)
                {
                    payroll.AddDeductionItem(dependentBenefit);
                    payroll.NetPay -= dependentBenefit.GetPayrollAmount();

                    // Apply any discounts if dependent qualifies
                    var discounts = orgDiscounts.FindAll(x => x.TypeId == (int)BenefitDiscountType.Dependent);
                    var calculator = BenefitDiscountCalculatorFactory.GetCalculator(BenefitDiscountType.Dependent);
                    foreach (BenefitDiscount discount in discounts)
                    {
                        if (calculator.Qualifies(dependent, discount))
                        {
                            IPayrollLineItem discountItem = calculator.CalculateDiscountItem(dependentBenefit.Benefit, discount);
                            payroll.AddLineItem(discountItem);
                            payroll.NetPay += discountItem.GetPayrollAmount();
                        }
                    }
                }
            }

            // Deduct tax from current base salary after benefits and depedent benefits and any discounts applied
            PayrollItem taxItem = CalculateTaxLineItem(payroll.NetPay, StandardTaxRate);
            payroll.AddDeductionItem(taxItem);
            payroll.NetPay -= taxItem.Amount;
        }

        /// <summary>
        /// Calculates and returns a payroll line item for taxes after deductions
        /// </summary>
        /// <param name="grossAfterDeductions">The gross amount after all deductions (i.e. benefits)</param>
        /// <param name="taxRate">The tax rate</param>
        /// <returns></returns>
        protected PayrollItem CalculateTaxLineItem(decimal grossAfterDeductions, decimal taxRate)
        {
            decimal tax = grossAfterDeductions * taxRate;
            return new PayrollItem
            {
                Amount = tax,
                Description = "Taxes (after deductions)",
            };
        }
    }
}
