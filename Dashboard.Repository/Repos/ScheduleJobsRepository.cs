using System;
using SoloDashboard.Entities.Models;
using System.Collections.Generic;
using SoloDashboard.Data.ConnectionManager;
using Npgsql;
using System.Data;
using Dashboard.Repository.Interfaces;
using System.Linq.Expressions;
using System.Linq;
using SoloDashboard.Models.Models;

namespace Dashboard.Repository.Repos
{
    public class ScheduleJobsRepository : Repository<Job> , IScheduleJobsRepository
    {
        public IEnumerable<Job> GetScheduleJobs()
        {
            string selectCommand = @"SELECT 
                                       job.ccmasterid,
                                       job.armasterid, 
                                        (select note from jobnote where job = job.ccmasterid and department = '001' limit 1) as csrnotes,
                                       job.sytermsid,
                                       (select sytdescription from terms where sytermsid = job.sytermsid ) as terms,
                                       customer.arcustname,
                                       job.ccdescription,
                                       job.quantityordered,
                                       job.earlieststartdate,
                                       csr.arcsrname,
                                       jobtype.description,
                                       job.ccscheduledshipdate,
                                       salesperson.arsalesname,
                                       job.ccpromisedate,
                                       (SELECT s.name 
                                    from jobplan j
                                       inner join schedulestatus s on s.id = j.status
                                    where activitycode = '12705' and job = job.ccmasterid) as Approved,
                                       (select enddate 
                                    from jobplan j
                                    where enddate is not null and j.job =  job.ccmasterid
                                    order by enddate desc limit 1) as finishdate,
                                       (SELECT sum(coalesce(NULLIF(cast(jobplan.scheduledhours as INTEGER),0))) 
                                    FROM  activitycode activitycode INNER JOIN jobplan 
                                       jobplan ON activitycode.jcmasterid=jobplan.activitycode INNER JOIN activitycode activitycode_1 ON 
                                       jobplan.scheduledactivity=activitycode_1.jcmasterid 
                                    WHERE jobplan.job = job.ccmasterid AND ((NOT ( (jobplan.status = 8)))) ) as remaingtime,
                                       (SELECT 
                                       CASE 
                                       when COUNT(jobplan.scheduledhours) > 0 then 'Remaining Task'
                                       when count(jobplan.scheduledhours) = 0 then 'No Remaining Task'
                                       end as task
                                    FROM  activitycode activitycode INNER JOIN jobplan 
                                       jobplan ON activitycode.jcmasterid=jobplan.activitycode INNER 
                                       JOIN activitycode activitycode_1 ON 
                                       jobplan.scheduledactivity=activitycode_1.jcmasterid 
                                    WHERE jobplan.job = job.ccmasterid AND ((NOT ( (jobplan.status = 8)))) ) as taskstatus
                                    FROM  jobstatus jobstatus 
                                       INNER JOIN job job ON jobstatus.sysstatusid=job.ccstatus 
                                       INNER JOIN csr csr ON job.arcsrid=csr.arcsrid 
                                       INNER JOIN jobtype jobtype ON job.jobtype=jobtype.id 
                                       INNER JOIN salesperson salesperson ON job.arsalesid=salesperson.arsalesid  
                                       INNER JOIN customer customer ON job.armasterid=customer.armasterid 
                                    where 
                                       job.ccstatus in ('$','1','9','T') and job.armasterid <> 'TRACF'
                                    ORDER BY Approved, taskstatus desc,job.ccscheduledshipdate asc";

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

                            scheduledJob.ccmasterid = dRow["ccmasterid"].ToString();
                            scheduledJob.armasterid = dRow["armasterid"].ToString();
                            scheduledJob.csrnotes = dRow["csrnotes"].ToString();
                            scheduledJob.terms = dRow["terms"].ToString();
                            scheduledJob.arcustname = dRow["arcustname"].ToString();
                            scheduledJob.ccdescription = dRow["ccdescription"].ToString();
                            scheduledJob.quantityordered = dRow["quantityordered"].ToString();
                            scheduledJob.earlieststartdate = dRow["earlieststartdate"].ToString();
                            scheduledJob.arcsrname = dRow["arcsrname"].ToString();
                            scheduledJob.description = dRow["description"].ToString();
                            scheduledJob.ccscheduledshipdate = dRow["ccscheduledshipdate"].ToString();
                            scheduledJob.arsalesname = dRow["arsalesname"].ToString();
                            scheduledJob.ccpromisedate = DateTime.Parse(dRow["ccpromisedate"].ToString());

                            scheduledJob.jobparts = GetJobParts(scheduledJob.ccmasterid);

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

        public new IEnumerable<Job> Find(Expression<Func<Job, bool>> predicate)
        {
            var jobs = GetScheduleJobs().AsQueryable();
            var result = jobs.Where(predicate).ToList();

            return result;
        }

        public List<JobPart> GetJobParts(string ccmasterid)
        {
            string selectCommand = $@"SELECT ccmasterid,ccjobpart,componentdescription,ccpartdesc, ccpages ,
                                        CONCAT(ccfinalsizew ,' x ',ccfinalsizeh) AS finalsize,concat(ccflatsizew,' x ',ccflatsizeh) AS flatsize
                                        FROM jobpart WHERE ccmasterid = '{ccmasterid}' 
                                        ORDER BY ccjobpart asc";

            try
            {
                using (NpgsqlDataAdapter data = new NpgsqlDataAdapter(selectCommand, ConnectionManager.GetMyNpgConnection()))
                {
                    DataTable dt = new DataTable();
                    data.SelectCommand.CommandType = CommandType.Text;
                    var jobparts = new List<JobPart>();


                    data.Fill(dt);

                    if (dt.Rows.Count > 0) // this is the record returned from epace
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dRow = dt.Rows[i];
                            var jobpart = new JobPart();

                            jobpart.ccmasterid = dRow["ccmasterid"].ToString();
                            jobpart.ccjobpart = dRow["ccjobpart"].ToString();
                            jobpart.componentdescription = dRow["componentdescription"].ToString();
                            jobpart.ccpartdesc = dRow["ccpartdesc"].ToString();
                            jobpart.ccpages = dRow["ccpages"].ToString();
                            jobpart.finalsize = dRow["finalsize"].ToString();
                            jobpart.flatsize = dRow["flatsize"].ToString();                            

                            jobparts.Add(jobpart);
                        }
                        return jobparts;
                    }
                    return jobparts;
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

        public List<JobPlan> GetJobPlan(string ccmasterid)
        {
            string selectCommand = $@"SELECT 
                                          job, 
                                          --jp.activitycode,
                                          jp.scheduledactivity , 
                                          at.jcdescription,
                                          jp.part,
                                          jp.printflowform,
                                          jp.startdate, 
                                          jp.enddate,
                                          s.name,jp.previoustask, jp.nexttask 
                                          FROM jobplan jp 
                                          INNER JOIN activitycode at ON at.jcmasterid = jp.activitycode
                                          INNER JOIN schedulestatus s ON s.id = jp.status
                                          WHERE job = '{ccmasterid}'
                                          ORDER BY jp.activitycode";

            try
            {
                using (NpgsqlDataAdapter data = new NpgsqlDataAdapter(selectCommand, ConnectionManager.GetMyNpgConnection()))
                {
                    DataTable dt = new DataTable();
                    data.SelectCommand.CommandType = CommandType.Text;
                    var tasks = new List<JobPlan>();


                    data.Fill(dt);

                    if (dt.Rows.Count > 0) // this is the record returned from epace
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dRow = dt.Rows[i];
                            var task = new JobPlan();

                            task.job = dRow["job"].ToString();
                            task.scheduledactivity = dRow["scheduledactivity"].ToString();
                            task.jcdescription = dRow["jcdescription"].ToString();
                            task.part = dRow["part"].ToString();
                            task.printflowform = dRow["printflowform"].ToString();
                            task.startdate = dRow["startdate"].ToString();
                            task.enddate = dRow["enddate"].ToString();
                            task.name = dRow["name"].ToString();
                            task.previoustask = dRow["previoustask"].ToString();
                            task.nexttask = dRow["nexttask"].ToString();

                            tasks.Add(task);
                        }
                        return tasks;
                    }
                    return tasks;
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
