using System;
using SoloDashboard.Entities.Models;
using System.Collections.Generic;
using SoloDashboard.Data.ConnectionManager;
using Npgsql;
using System.Data;
using Dashboard.Repository.Interfaces;

namespace Dashboard.Repository.Repos
{
    public class ScheduleJobsRepository : Repository<Job> , IScheduleJobsRepository
    {
        public IEnumerable<Job> GetScheduleJobs()
        {
            string selectCommand = @"SELECT jobstatus.sysdescription, job.armasterid,
                                        (select note from jobnote where job = job.ccmasterid and department = '001' limit 1) as csrnotes,
                                           (select s.name from jobplan j inner join schedulestatus s on s.id = j.status where activitycode = '12705' and job = job.ccmasterid) as Approved,
                                           (SELECT sum(coalesce(NULLIF(cast(jobplan.scheduledhours as INTEGER),0))) FROM  activitycode activitycode INNER JOIN jobplan
                                           jobplan ON activitycode.jcmasterid=jobplan.activitycode INNER JOIN activitycode activitycode_1 ON
                                           jobplan.scheduledactivity=activitycode_1.jcmasterid
                                        WHERE jobplan.job = job.ccmasterid AND ((NOT ( (jobplan.status = 8)))) ) as remaingtime,
                                           (SELECT
                                           CASE
                                           when COUNT(jobplan.scheduledhours) > 0 then 'Remaining Task'
                                           when count(jobplan.scheduledhours) = 0 then 'No Remaining Task'
                                           end as task
                                        FROM  activitycode activitycode INNER JOIN jobplan
                                           jobplan ON activitycode.jcmasterid=jobplan.activitycode   INNER
                                           JOIN activitycode activitycode_1 ON
                                           jobplan.scheduledactivity=activitycode_1.jcmasterid
                                        WHERE jobplan.job = job.ccmasterid AND ((NOT ( (jobplan.status = 8)))) ) as taskstatus,   job.ccdatesetup,
                                           job.ccscheduledshipdate,
                                           job.earlieststartdate,
                                           job.qtyshipped,job.arsalesid,job.jobordertype,job.ccmasterid,job.ccpromisedate,job.ccdescription,job.quantityordered,job.ccdescription2,job.ccstatus,csr.arcsrname,jobtype.description,salesperson.arsalesname,customer.arcustname
                                        FROM jobstatus jobstatus 
                                        INNER JOIN job job ON jobstatus.sysstatusid=job.ccstatus )  
                                        INNER JOIN csr csr ON job.arcsrid=csr.arcsrid )  
                                        INNER JOIN jobtype jobtype ON job.jobtype=jobtype.id )  
                                        INNER JOIN salesperson salesperson ON job.arsalesid=salesperson.arsalesid )  
                                        INNER JOIN customer customer ON job.armasterid=customer.armasterid
                                        where job.armasterid <> 'TRACF' and job.ccstatus in ('$','1','9','T')";

            try
            {
                using (NpgsqlDataAdapter data = new NpgsqlDataAdapter(selectCommand, ConnectionManager.GetMyNpgConnection()))
                {
                    DataTable dt = new DataTable();
                    data.SelectCommand.CommandType = CommandType.Text;
                    var jobsList = new List<Job>();


                    data.Fill(dt);

                    if (dt.Rows.Count > 0) // this is the record returned from epace
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dRow = dt.Rows[i];
                            var scheduledJob = new Job();

                            scheduledJob.sysdescription = dRow["sysdescription"].ToString().Trim();
                            scheduledJob.armasterid = dRow["armasterid"].ToString();
                            scheduledJob.csrnotes = dRow["csrnotes"].ToString();

                            jobsList.Add(scheduledJob);
                        }

                        return jobsList;
                    }

                    return jobsList;
                }
            }
            catch (NpgsqlException ex)
            {
                throw new Exception("Npgsql exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception: " + ex.Message);
            }
            finally
            {
                ConnectionManager.CloseNpgSqlConnection();
            }
        }
        
    }
}
