using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace SoloDashboard.Models.Models
{
    public class JobPlan
    {
        [Display(Name = "Job Number")]
        public string job { get; set; }
        [Display(Name = "Schedule Activity Code")]
        public string scheduledactivity { get; set; }
        [Display(Name = "Description")]
        public string jcdescription { get; set; }
        [Display(Name = "Part")]
        public string part { get; set; }
        [Display(Name = "PrintFlow Part")]
        public string printflowform { get; set; }
        [Display(Name = "Start Date")]
        public string startdate { get; set; }
        [Display(Name = "End Date")]
        public string enddate { get; set; }
        [Display(Name = "Task")]
        public string name { get; set; }
        [Display(Name = "Previous Task")]
        public string previoustask { get; set; }
        [Display(Name = "Next Task")]
        public string nexttask { get; set; }
    }
}
