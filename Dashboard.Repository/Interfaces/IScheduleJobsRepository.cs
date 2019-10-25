using SoloDashboard.Entities.Models;
using SoloDashboard.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Repository.Interfaces
{
    public interface IScheduleJobsRepository : IRepository<Job>
    {
        IEnumerable<Job> GetScheduleJobs();
        List<JobPart> GetJobParts(string ccmasterid);
        List<JobPlan> GetJobPlan(string ccmasterid);


    }
}
