using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloDashboard.Entities.Models
{
    public class Job
    {
        [Display(Name = "Remaining Time")]
        public string remaingtime { get; set; }
        public string sysdescription { get; set; }
        public string armasterid { get; set; }
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
        public string arcsrname { get; set; }
        public string description { get; set; }
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
    }
}
