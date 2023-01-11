using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PaylocityDemo.Entity
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Benefit> Benefit { get; set; }
        public virtual DbSet<BenefitDiscount> BenefitDiscount { get; set; }
        public virtual DbSet<Dependent> Dependent { get; set; }
        public virtual DbSet<DependentBenefit> DependentBenefit { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeBenefit> EmployeeBenefit { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<Payroll> Payroll { get; set; }
        public virtual DbSet<PayrollItem> PayrollItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-UBP2OSM;Initial Catalog=paylocity-demo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Benefit>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cost)
                    .HasColumnName("cost")
                    .HasColumnType("decimal(19, 4)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrganizationId).HasColumnName("organizationId");

                entity.Property(e => e.StatusId)
                    .HasColumnName("statusId")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Benefit)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Benefit_Organization");
            });

            modelBuilder.Entity<BenefitDiscount>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Expression)
                    .IsRequired()
                    .HasColumnName("expression")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrganizationId).HasColumnName("organizationId");

                entity.Property(e => e.PercentOff)
                    .HasColumnName("percentOff")
                    .HasColumnType("decimal(19, 4)");

                entity.Property(e => e.StatusId)
                    .HasColumnName("statusId")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TypeId).HasColumnName("typeId");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.BenefitDiscount)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BenefitDiscount_Organization");
            });

            modelBuilder.Entity<Dependent>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeId");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(50);

                entity.Property(e => e.Relationship)
                    .IsRequired()
                    .HasColumnName("relationship")
                    .HasMaxLength(50);

                entity.Property(e => e.StatusId)
                    .HasColumnName("statusId")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Dependent)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dependent_Employee");
            });

            modelBuilder.Entity<DependentBenefit>(entity =>
            {
                entity.HasKey(e => new { e.DependentId, e.BenefitId });

                entity.Property(e => e.DependentId).HasColumnName("dependentId");

                entity.Property(e => e.BenefitId).HasColumnName("benefitId");

                entity.HasOne(d => d.Benefit)
                    .WithMany(p => p.DependentBenefit)
                    .HasForeignKey(d => d.BenefitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DependentBenefit_Benefit");

                entity.HasOne(d => d.Dependent)
                    .WithMany(p => p.DependentBenefit)
                    .HasForeignKey(d => d.DependentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DependentBenefit_Dependent");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrganizationId).HasColumnName("organizationId");

                entity.Property(e => e.Salary)
                    .HasColumnName("salary")
                    .HasColumnType("decimal(19, 4)")
                    .HasDefaultValueSql("((52000))");

                entity.Property(e => e.StatusId)
                    .HasColumnName("statusId")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Organization");
            });

            modelBuilder.Entity<EmployeeBenefit>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.BenefitId });

                entity.Property(e => e.EmployeeId).HasColumnName("employeeId");

                entity.Property(e => e.BenefitId).HasColumnName("benefitId");

                entity.HasOne(d => d.Benefit)
                    .WithMany(p => p.EmployeeBenefit)
                    .HasForeignKey(d => d.BenefitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeBenefit_Benefit");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeBenefit)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeBenefit_Employee");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId)
                    .HasColumnName("statusId")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Payroll>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeId");

                entity.Property(e => e.FromDate)
                    .HasColumnName("fromDate")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GrossPay)
                    .HasColumnName("grossPay")
                    .HasColumnType("decimal(19, 2)");

                entity.Property(e => e.NetPay)
                    .HasColumnName("netPay")
                    .HasColumnType("decimal(19, 2)");

                entity.Property(e => e.OrganizationId).HasColumnName("organizationId");

                entity.Property(e => e.StatusId)
                    .HasColumnName("statusId")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.ToDate)
                    .HasColumnName("toDate")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Payroll)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payroll_Employee");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Payroll)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payroll_Organization");
            });

            modelBuilder.Entity<PayrollItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PayrollId).HasColumnName("payrollId");

                entity.HasOne(d => d.Payroll)
                    .WithMany(p => p.PayrollItem)
                    .HasForeignKey(d => d.PayrollId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PayrollItem_Payroll");
            });
        }
    }
}
