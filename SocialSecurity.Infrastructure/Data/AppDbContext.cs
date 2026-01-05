using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialSecurity.Domain.Models;
using SocialSecurity.Infrastructure.DataSeedings;
using System.Reflection.Emit;

namespace SocialSecurity.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<FiscalYear> FiscalYears { get; set; }
        public DbSet<FiscalYearPeriod> FiscalYearPeriods { get; set; }
        public DbSet<AuditPlan> AuditPlans { get; set; }
        public DbSet<DepartmentAuditPlan> DepartmentAuditPlans { get; set; }
        public DbSet<AuditTeamMember> DepartmentAuditTeamMembers { get; set; }
        public DbSet<DepartmentDetail> DepartmentDetails { get; set; }
        public DbSet<DepartmentFunction> DepartmentFunctions { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DepartmentAuditPlan>()
       .HasOne(d => d.FiscalYearPeriod)
       .WithMany()
       .HasForeignKey(d => d.FiscalYearPeriodId)
       .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction

            builder.Entity<DepartmentAuditPlan>()
                .HasOne(d => d.AuditPlan)
                .WithMany()
                .HasForeignKey(d => d.AuditPlanId)
                .OnDelete(DeleteBehavior.Cascade); // Keep cascade on one if needed

            DataSeeding.AddDataSeeding(builder);

        }

    }
}