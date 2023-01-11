using PaylocityDemo.Entity;
using PaylocityDemo.Model.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Manager
{
    public interface IPayrollManager
    {
        /// <summary>
        /// Returns a single payroll draft for the given payroll ID
        /// </summary>
        /// <param name="payrollId"></param>
        /// <returns></returns>
        IQueryable<Payroll> GetDraft(int payrollId);

        /// <summary>
        /// Returns a single payroll draft for the given employee ID and pay period dates
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        IQueryable<Payroll> GetDraft(int employeeId, DateTime fromDate, DateTime toDate);
        
        /// <summary>
        /// Returns all payroll drafts for an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        IQueryable<Payroll> GetDrafts(int organizationId);

        /// <summary>
        /// Saves a payroll draft for the given employee and the given pay period model with any benefits, dependents, and applied discounts
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="orgDiscounts"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Payroll SaveDraft(Employee employee, List<BenefitDiscount> orgDiscounts, PayrollDraftBindingModel model);
    }
}