using Dashboard.Repository.Interfaces;
using System;
using System.Collections.Generic;
using SoloDashboard.Entities.Models;
using System.Linq.Expressions;
using System.IO;
using System.Linq;
using System.Reflection;
using SoloDashboard.Models.Models;

namespace Dashboard.Repository.Repos
{
    public class MockJobsRepository : IMockJobsRepository
    {
        public IEnumerable<Job> Find(Expression<Func<Job, bool>> predicate)
        {
            var jobs = GetScheduleJobs().AsQueryable();
            var result = jobs.Where(predicate).ToList();

            return result;
        }

        public IEnumerable<Job> GetScheduleJobs()
        {
            string[] readText = File.ReadAllLines(@"C:\Visual Studio 2015\Projects\SoloDashboard\Dashboard.Repository\TestData\expdata1.csv");

            List<Job> scheduledJobs = new List<Job>();
            int counter = 0;

            foreach (var item in readText)
            {
                string[] record = item.Split('\t');
                Job job = null;

                if (counter > 0)
                {
                    
                    job = new Job();
                    job.ccmasterid = record[0] != null ? record[0].Trim() : "NA";
                    job.systermsid = record[1] != null ? record[1].Trim() : "NA";
                    job.armasterid = record[2] != null ? record[2].Trim() : "NA";
                    job.terms = record[3] != null ? record[3].Trim() : "NA";
                    job.arcustname = record[4] != null ? record[4].Trim() : "NA";
                    job.ccdescription = record[5] != null ? record[5].Trim() : "NA";
                    job.quantityordered = record[6] != null ? record[6].Trim() : "NA";
                    job.earlieststartdate = record[7] != null? record[7].Trim() : "NA";
                    job.arcsrname = record[8] != null ? record[8].Trim() : "NA";
                    job.description = record[9] != null ? record[9].Trim() : "NA";
                    job.ccscheduledshipdate = record[10] != null ? record[10].Trim() : "NA";
                    job.arsalesname = record[11] != null ? record[11].Trim() : "NA";
                    job.ccpromisedate = record[12] != null ? Convert.ToDateTime(record[12].ToString()) : DateTime.MinValue;
                    job.approvedDate = record[13] != null ? record[13].Trim() : "NA";
                    job.finishdate = record[14] != null ? record[14].Trim() : "NA";
                    job.remaingtime = record[15] != null ? record[15].Trim() : "NA";
                    job.taskstatus = record[16] != null ? record[16].Trim() : "NA";
                    job.jobparts = GetJobParts(job.ccmasterid.ToString());
                    job.jobplan = GetJobPlan(job.ccmasterid.ToString());

                }

                if (job != null)
                {
                    scheduledJobs.Add(job);
                }
                

                counter++;
            }

            return scheduledJobs;
        }

        public List<JobPart> GetJobParts(string ccmasterid)
        {
            string[] readText = File.ReadAllLines(@"C:\Visual Studio 2015\Projects\SoloDashboard\Dashboard.Repository\TestData\parts.csv");

            List<JobPart> jobpart = new List<JobPart>();
            int counter = 0;

            foreach (var item in readText)
            {
                string[] record = item.Split('\t');
                JobPart part = null;

                if (counter > 0)
                {
                    part = new JobPart();
                    part.ccmasterid = record[0] != null ? record[0].Trim() : "NA";
                    part.ccjobpart = record[1] != null ? record[1].Trim() : "NA";
                    part.componentdescription = record[2] != null ? record[2].Trim() : "NA";
                    part.ccpartdesc = record[3] != null ? record[3].Trim() : "NA";
                    part.ccpages = record[4] != null ? record[4].Trim() : "NA";
                    part.finalsize = record[5] != null ? record[5].Trim() : "NA";
                    part.flatsize = record[6] != null ? record[6].Trim() : "NA";
                }

                if (jobpart != null)
                {
                    jobpart.Add(part);
                }
                counter++;
            }

            return jobpart;
        }

        public List<JobPlan> GetJobPlan(string ccmasterid)
        {
            string[] readText = File.ReadAllLines(@"C:\Visual Studio 2015\Projects\SoloDashboard\Dashboard.Repository\TestData\JobPlan.csv");

            List<JobPlan> jobplan = new List<JobPlan>();
            int counter = 0;

            foreach (var item in readText)
            {
                string[] record = item.Split('\t');
                JobPlan plans = null;

                if (counter > 0)
                {
                    plans = new JobPlan();
                    plans.job = record[0] != null ? record[0].Trim() : "NA";
                    plans.scheduledactivity = record[1] != null ? record[1].Trim() : "NA";
                    plans.jcdescription = record[2] != null ? record[2].Trim() : "NA";
                    plans.part = record[3] != null ? record[3].Trim() : "NA";
                    plans.printflowform = record[4] != null ? record[4].Trim() : "NA";
                    plans.startdate = record[5] != null ? record[5].Trim() : "NA";
                    plans.enddate = record[6] != null ? record[6].Trim() : "NA";
                    plans.name = record[7] != null ? record[7].Trim() : "NA";
                    plans.previoustask = record[8] != null ? record[8].Trim() : "NA";
                    plans.nexttask = record[9] != null ? record[9].Trim() : "NA";
                }

                if (plans != null)
                {
                    jobplan.Add(plans);
                }
                counter++;
            }

            return jobplan;
        }

        public Expression<Func<Job, bool>> GetDynamicQueryWithExpresionTrees(string propertyName, string value)
        {
            //x =>
            var param = Expression.Parameter(typeof(Job), "x");
            //val ("Curry")
            var valExpression = Expression.Constant(value, typeof(string));
            //Field or Property Name
            var column = Expression.PropertyOrField(param, propertyName);
            //x.LastName == "Curry"
            BinaryExpression body = Expression.Equal(column, valExpression);
            //x => x.LastName == "Curry"
            var final = Expression.Lambda<Func<Job, bool>>(body, param);
            //compiles the expression tree to a func delegate
            return  final;    
        }

        private Func<Job, bool> GetDynamicQueryWithExpresionTrees2(string propertyName, string val)
        {
            var param = Expression.Parameter(typeof(Job), "x");
            var member = Expression.Property(param, propertyName);
            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(propertyType);

            if (!converter.CanConvertFrom(typeof(string)))
                throw new NotSupportedException();

            //will give the integer value if the string is integer
            var propertyValue = converter.ConvertFromInvariantString(val);
            var constant = Expression.Constant(propertyValue);

            return null;
        }
    }
}
