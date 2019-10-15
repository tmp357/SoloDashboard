using System;
using Dashboard.Repository.Interfaces;

namespace SoloDashboard.Repository.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IScheduleJobsRepository DbContextJobs { get; }
        IMockJobsRepository DbContextJobsTestData { get; }
    }
}
