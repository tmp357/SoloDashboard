using SoloDashboard.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Dashboard.Repository.Interfaces
{
    public interface IMockJobsRepository 
    {
        IEnumerable<Job> GetScheduleJobs();
        IEnumerable<Job> Find(Expression<Func<Job, bool>> predicate);
        Expression<Func<Job, bool>> GetDynamicQueryWithExpresionTrees(string propertyName, string val);
    }
}
