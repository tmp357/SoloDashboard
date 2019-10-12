using Dashboard.Repository.Interfaces;
using System;
using System.Collections.Generic;
using SoloDashboard.Entities.Models;
using System.Linq.Expressions;
using System.IO;
using System.Linq;

namespace Dashboard.Repository.Repos
{
    public class MockJobsRepository : IMockJobsRepository
    {
        public IEnumerable<Job> GetScheduleJobs()
        {
            string[] readText = File.ReadAllLines(@"C:\Visual Studio 2015\Projects\SoloDashboard\Dashboard.Repository\TestData\data-1570538698798r3.tab");

            List<Job> scheduledJobs = new List<Job>();
            int counter = 0;

            foreach (var item in readText)
            {
                string[] record = item.Split('\t');
                Job job = null;

                if (counter > 0)
                {
                    job = new Job();
                    job.sysdescription = record[0] != null? record[0].Trim() : "NA";
                    job.armasterid = record[1] != null ? record[1].Trim() : "NA";
                    job.csrnotes = record[2] != null ? record[2].Trim() : "NA";
                    job.Approved = record[3] != null ? record[3].Trim() : "NA";
                    job.remaingtime = record[4] != null ? record[4].Trim() : "NA";
                    job.taskstatus = record[5] != null ? record[5].Trim() : "NA";
                    job.ccdatesetup = record[6] != null ? record[6].Trim() : "NA";
                    job.ccscheduledshipdate = record[7] != null ? record[7].Trim() : "NA";
                    job.earlieststartdate = record[8] != null ? record[8].Trim() : "NA";
                    job.qtyshipped = record[9] != null ? record[9].Trim() : "NA";
                    job.arsalesid = record[10] != null ? record[10].Trim() : "NA";
                    job.jobordertype = record[11] != null ? record[11].Trim() : "NA";
                    job.ccmasterid = record[12] != null ? record[12].Trim() : "NA";
                    job.ccpromisedate = record[13] != null ? record[13].Trim() : "NA";
                    job.ccdescription = record[14] != null ? record[14].Trim() : "NA";
                    job.quantityordered = record[15] != null ? record[15].Trim() : "NA";
                    job.ccdescription2 = record[16] != null ? record[16].Trim() : "NA";
                    job.ccstatus = record[17] != null ? record[17].Trim() : "NA";
                    job.arcsrname = record[18] != null ? record[18].Trim() : "NA";
                    job.description = record[19] != null ? record[19].Trim() : "NA";
                    job.arsalesname = record[20] != null ? record[20].Trim() : "NA";
                    job.arcustname = record[21] != null ? record[21].Trim() : "NA";
                }                

                scheduledJobs.Add(job);

                counter++;
            }

            return scheduledJobs;
        }
    }
}
