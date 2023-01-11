using Microsoft.EntityFrameworkCore;
using PaylocityDemo.Data;
using PaylocityDemo.Entity;
using PaylocityDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Manager
{
    public class EmployeeManager : BaseEntityManager<Employee>, IEmployeeManager
    {

        public EmployeeManager(IRepository<Employee> repository) : base(repository)
        {
        }

        public IQueryable<Employee> GetForPayrollCalculation(int employeeId)
        {
            return Repository.Filter(
                    x => x.Id == employeeId
                    && x.StatusId != (int)Status.Deleted)
                .Include(x => x.Dependent)
                    .ThenInclude(x => x.DependentBenefit)
                        .ThenInclude(x => x.Benefit)
                .Include(x => x.EmployeeBenefit)
                    .ThenInclude(x => x.Benefit);
        }
    }
}
