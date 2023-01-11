using PaylocityDemo.Entity;
using System.Linq;

namespace PaylocityDemo.Manager
{
    public interface IEmployeeManager
    {
        IQueryable<Employee> GetForPayrollCalculation(int employeeId);
    }
}