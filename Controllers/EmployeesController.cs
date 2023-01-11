using Microsoft.AspNetCore.Mvc;
using PaylocityDemo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Controllers
{

    [Produces("application/json")]
    [Route("employees")]
    public class EmployeesController : BaseController
    {
        public EmployeesController(AppDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// [Not Implemented] Creates an employee record.
        /// POST model will include basic info including organization ID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateEmployee()
        {
            await Task.Run(() => { /* TODO */ });
            return new JsonResult("Not implemented");
        }

        /// <summary>
        /// [Not Implemented] Deletes an employee record for the given ID
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await Task.Run(() => { /* TODO */ });
            return new JsonResult("Not implemented");
        }

        /// <summary>
        /// [Not Implemented] Returns an employee record for the given ID
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            await Task.Run(() => { /* TODO */ });
            return new JsonResult("Not implemented");
        }

        /// <summary>
        /// [Not Implemented] Updates an existing employee record for the given ID
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id)
        {
            await Task.Run(() => { /* TODO */ });
            return new JsonResult("Not implemented");
        }

        /// <summary>
        /// [Not Implemented] Creates a dependent record for the given employee ID
        /// </summary>
        /// <param name="employeeId">The employee ID</param>
        /// <returns></returns>
        [HttpPost("{employeeId}/dependents")]
        public async Task<IActionResult> CreateDependent(int employeeId)
        {
            await Task.Run(() => { /* TODO */ });
            return new JsonResult("Not implemented");
        }

        /// <summary>
        /// [Not Implemented] Deletes a dependent for the given employee ID and dependent ID
        /// </summary>
        /// <param name="employeeId">The employee ID</param>
        /// <param name="id">The dependent ID</param>
        /// <returns></returns>
        [HttpDelete("{employeeId}/dependents/{id}")]
        public async Task<IActionResult> DeleteDependent(int employeeId, int id)
        {
            await Task.Run(() => { /* TODO */ });
            return new JsonResult("Not implemented");
        }

        /// <summary>
        /// [Not Implemented] Gets a dependent for the given employee ID and dependent ID
        /// </summary>
        /// <param name="employeeId">The employee ID</param>
        /// <param name="id">The dependent ID</param>
        /// <returns></returns>
        [HttpGet("{employeeId}/dependents/{id}")]
        public async Task<IActionResult> GetDependents(int employeeId, int id)
        {
            await Task.Run(() => { /* TODO */ });
            return new JsonResult("Not implemented");
        }

        /// <summary>
        /// [Not Implemented] Updates a dependent for the given employee ID and dependent ID
        /// </summary>
        /// <param name="employeeId">The employee ID</param>
        /// <param name="id">The dependent ID</param>
        /// <returns></returns>
        [HttpPut("{employeeId}/dependents/{id}")]
        public async Task<IActionResult> UpdateDependent(int employeeId, int id)
        {
            await Task.Run(() => { /* TODO */ });
            return new JsonResult("Not implemented");
        }

        /// <summary>
        /// [Not Implemented] Adds a benefit to the given employee.
        /// Checks if a benefit of same ID is already associated to employee.
        /// </summary>
        /// <param name="employeeId">The employee ID</param>
        /// <param name="benefitId">The benefit ID</param>
        /// <returns></returns>
        [HttpPost("{employeeId}/benefits/{benefitId}")]
        public async Task<IActionResult> AddEmployeeBenefit(int employeeId, int benefitId)
        {
            await Task.Run(() => { /* TODO */ });
            return new JsonResult("Not implemented");
        }

        /// <summary>
        /// [Not Implemented] Removes a benefit from the given employee.
        /// </summary>
        /// <param name="employeeId">The employee ID</param>
        /// <param name="benefitId">The benefit ID</param>
        /// <returns></returns>
        [HttpDelete("{employeeId}/benefits/{benefitId}")]
        public async Task<IActionResult> RemoveEmployeeBenefit(int employeeId, int benefitId)
        {
            await Task.Run(() => { /* TODO */ });
            return new JsonResult("Not implemented");
        }

        /// <summary>
        /// [Not Implemented] Adds a benefit to the given dependent for the given employee.
        /// Checks if a benefit of same ID is already associated to dependent.
        /// </summary>
        /// <param name="employeeId">The employee ID</param>
        /// <param name="dependentId">The dependent ID</param>
        /// <param name="benefitId">The benefit ID</param>
        /// <returns></returns>
        [HttpPost("{employeeId}/dependents/{dependentId}/benefits/{benefitId}")]
        public async Task<IActionResult> AddDependentBenefit(int employeeId, int dependentId, int benefitId)
        {
            await Task.Run(() => { /* TODO */ });
            return new JsonResult("Not implemented");
        }

        /// <summary>
        /// [Not Implemented] Removes a benefit from the given dependent for the given employee.
        /// Checks if a benefit of same ID is already associated to dependent.
        /// </summary>
        /// <param name="employeeId">The employee ID</param>
        /// <param name="dependentId">The dependent ID</param>
        /// <param name="benefitId">The benefit ID</param>
        /// <returns></returns>
        [HttpDelete("{employeeId}/dependents/{dependentId}/benefits/{benefitId}")]
        public async Task<IActionResult> RemoveDependentBenefit(int employeeId, int dependentId, int benefitId)
        {
            await Task.Run(() => { /* TODO */ });
            return new JsonResult("Not implemented");
        }
    }
}
