using SoloDashboard.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoloDashboard.Entities.Models
{
    public class Job
    {
        private string _jobCompletionStatus = "";

        public Job()
        {
            jobparts = new List<JobPart>();
            jobplan = new List<JobPlan>();
        }

        [Display(Name = "Remaining Time")]
        public string remaingtime { get; set; }
        public string sysdescription { get; set; }
        public string armasterid { get; set; }
        [Display(Name = "Csr Notes")]
        public string csrnotes { get; set; }
        public string taskstatus { get; set; }
        [Display(Name = "Entry Date")]
        public Nullable<DateTime> ccdatesetup { get; set; }
        [Display(Name = "Schedule Ship Date")]
        public string ccscheduledshipdate { get; set; }
        [Display(Name = "Earliest Start Date")]
        public string earlieststartdate { get; set; }
        [Display(Name = "Quantity Shipped")]
        public string qtyshipped { get; set; }
        public string arsalesid { get; set; }
        [Display(Name = "Job Type")]
        public string jobordertype { get; set; }
        [Display(Name = "Job Number")]
        public string ccmasterid { get; set; }
        [Display(Name = "Job Due Date")]
        public Nullable<DateTime> ccpromisedate { get; set; }
        [Display(Name = "Job Description")]
        public string ccdescription { get; set; }
        [Display(Name = "Quantity Ordered")]
        public string quantityordered { get; set; }
        public string ccdescription2 { get; set; }
        [Display(Name = "Status")]
        public string ccstatus { get; set; }
        [Display(Name = "Csr Name")]
        public string arcsrname { get; set; }
        public string description { get; set; }
        [Display(Name = "Sales Person")]
        public string arsalesname { get; set; }
        [Display(Name = "Customer Name")]
        public string arcustname { get; set; }

        public string logo
        {
            get
            {
                return "~/Logos/"+ armasterid.Trim() +".png";
            }           

        }

        public string terms { get; set; }
        public string PrintFlowCompletionDate { get; set; }
        public string systermsid { get; set; }
        [Display(Name = "Proof Approved Date")]
        public string approvedDate { get; set; }
        public string finishdate { get; set; }

        public string jobCompletionStatus
        {
            get
            {
                if (DateTime.Now <= ccpromisedate)
                {
                    _jobCompletionStatus = "bg-success";
                }
                else if (ccpromisedate != null && ccpromisedate.Value.ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    _jobCompletionStatus = "bg-info";
                }
                else if (DateTime.Now >= ccpromisedate)
                {
                    _jobCompletionStatus = "bg-warning";
                }
                else
                {
                    _jobCompletionStatus = "bg-light";
                }

                return _jobCompletionStatus;
            }           
        }

        public List<JobPart> jobparts { get; set; }
        public List<JobPlan> jobplan { get; set; }


    }
}
