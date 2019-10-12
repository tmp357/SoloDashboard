using SoloDashboard.Entities.Models;
using System.Collections.Generic;

namespace Dashboard.Repository.Interfaces
{
    public interface IMockJobsRepository 
    {
        IEnumerable<Job> GetScheduleJobs();
    }
}
