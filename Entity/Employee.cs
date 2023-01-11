using System;
using System.Collections.Generic;

namespace PaylocityDemo.Entity
{
    public partial class Employee
    {
        public Employee()
        {
            Dependent = new HashSet<Dependent>();
            EmployeeBenefit = new HashSet<EmployeeBenefit>();
            Payroll = new HashSet<Payroll>();
        }

        public int Id { get; set; }
        public int StatusId { get; set; }
        public int OrganizationId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public decimal Salary { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual ICollection<Dependent> Dependent { get; set; }
        public virtual ICollection<EmployeeBenefit> EmployeeBenefit { get; set; }
        public virtual ICollection<Payroll> Payroll { get; set; }
    }
}
