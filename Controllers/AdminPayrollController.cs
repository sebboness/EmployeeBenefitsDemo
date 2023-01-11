using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaylocityDemo.Entity;
using PaylocityDemo.Manager;
using PaylocityDemo.Mapping;
using PaylocityDemo.Model.Payroll;
using PaylocityDemo.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Controllers
{
    [Produces("application/json")]
    [Route("admin/payroll")]
    public class AdminPayrollController : BaseController
    {
        private readonly IBenefitDiscountManager benefitDiscountManager;
        private readonly IEmployeeManager employeeManager;
        private readonly IPayrollManager payrollManager;

        public AdminPayrollController(
            IBenefitDiscountManager benefitDiscountManager,
            IEmployeeManager employeeManager,
            IPayrollManager payrollManager,
            AppDbContext dbContext) : base(dbContext)
        {
            this.benefitDiscountManager = benefitDiscountManager;
            this.employeeManager = employeeManager;
            this.payrollManager = payrollManager;
        }

        /// <summary>
        /// Creates a new payroll draft for an employee for the provided pay period dates (fromDate, toDate)
        /// </summary>
        /// <param name="organizationId">The organization ID</param>
        /// <param name="employeeId">The employee ID</param>
        /// <param name="model">Model that accepts fromDate and toDate</param>
        /// <returns></returns>
        [HttpPost("{organizationId}/draft/{employeeId}")]
        public async Task<IActionResult> Draft(int organizationId, int employeeId, [FromBody]PayrollDraftBindingModel model)
        {
            // TODO Perform custom model validation such as checking that fromDate <= toDate

            if (!ModelState.IsValid)
                return new ApiResult()
                    .WithErrors(ModelState)
                    .ToJsonResult();

            var draft = await payrollManager.GetDraft(employeeId, model.FromDate, model.ToDate)
                .FirstOrDefaultAsync();

            // If a draft already exists for employee for provided dates, return an error.
            // If a draft needs to be re-drafted, the admin should first delete the draft and then call this endpoint again
            if (draft != null)
            {
                ModelState.AddModelError(nameof(employeeId), "Payroll draft already exists for employee and given pay period");
                return new ApiResult()
                    .WithErrors(ModelState)
                    .ToJsonResult();
            }

            // Get employee record that includes related dependants, benefits, and discount objects
            var employee = await employeeManager.GetForPayrollCalculation(employeeId)
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                ModelState.AddModelError(nameof(employeeId), $"Employee record not found for ${employeeId}");
                return new ApiResult()
                    .WithErrors(ModelState)
                    .ToJsonResult();
            }

            // Get all company discounts
            var orgDiscounts = await benefitDiscountManager.GetAll(employee.OrganizationId).ToListAsync();

            // Create payroll draft object (saves to db with status of "draft")
            Payroll payroll = await Task.Run(() => payrollManager.SaveDraft(employee, orgDiscounts, model));
            PayrollResult result = AutoMappings.Current.Mapper.Map<Payroll, PayrollResult>(payroll);
            return new ApiResult()
                    .WithData(result)
                    .ToJsonResult();
        }


        /// <summary>
        /// [Not Implemented] Deletes a payroll draft (soft delete)
        /// </summary>
        /// <param name="organizationId">The organization ID</param>
        /// <param name="payrollId">The payroll ID</param>
        /// <returns></returns>
        [HttpDelete("{organizationId}/draft/{payrollId}")]
        public async Task<IActionResult> DeleteDraft(int organizationId, int payrollId)
        {
            await Task.Run(() => { /* TODO */ });
            return new JsonResult("Not implemented");
        }

        /// <summary>
        /// Returns a payroll draft by the given ID
        /// </summary>
        /// <param name="organizationId">The organization ID</param>
        /// <param name="payrollId">The payroll ID</param>
        /// <returns></returns>
        [HttpGet("{organizationId}/draft/{payrollId}")]
        public async Task<IActionResult> GetDraft(int organizationId, int payrollId)
        {
            Payroll payroll = await payrollManager.GetDraft(payrollId)
                .FirstOrDefaultAsync();

            if (payroll == null)
            {
                ModelState.AddModelError(nameof(payrollId), $"No payroll draft record found for dates");
                return new ApiResult()
                    .WithErrors(ModelState)
                    .ToJsonResult();
            }

            PayrollResult result = AutoMappings.Current.Mapper.Map<Payroll, PayrollResult>(payroll);
            return new ApiResult()
                    .WithData(result)
                    .ToJsonResult();
        }

        /// <summary>
        /// Returns all payroll drafts for the given organizationID
        /// </summary>
        /// <param name="organizationId">The organization ID</param>
        /// <param name="payrollId">The payroll ID</param>
        /// <returns></returns>
        [HttpGet("{organizationId}/draft")]
        public async Task<IActionResult> GetDrafts(int organizationId)
        {
            List<Payroll> payrolls = await payrollManager.GetDrafts(organizationId)
                .ToListAsync();

            List<PayrollResult> result = AutoMappings.Current.Mapper.Map<List<Payroll>, List<PayrollResult>>(payrolls);
            return new ApiResult()
                    .WithData(result)
                    .ToJsonResult();
        }

        /// <summary>
        /// [Not Implemented] Submits the given payroll (changes its status from "Draft" to "Active", meaning it's paid out)
        /// </summary>
        /// <param name="organizationId">The organization ID</param>
        /// <param name="payrollId">The payroll ID</param>
        /// <returns></returns>
        [HttpPost("{organizationId}/submit/{payrollId}")]
        public async Task<IActionResult> Submit(int organizationId, int payrollId)
        {
            await Task.Run(() => { /* TODO */ });
            return new JsonResult("Not implemented");
        }
    }
}
