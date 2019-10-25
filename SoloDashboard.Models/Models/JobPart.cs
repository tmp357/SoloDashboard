using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloDashboard.Models.Models
{
    public class JobPart
    {
        [Display(Name = "Job Number")]
        public string ccmasterid { get; set; }
        [Display(Name = "Part")]
        public string ccjobpart { get; set; }
        [Display(Name = "Part Status")]
        public string componentdescription { get; set; }
        [Display(Name = "Description")]
        public string ccpartdesc { get; set; }
        [Display(Name = "Pages")]
        public string ccpages { get; set; }
        [Display(Name = "Final Size")]
        public string finalsize { get; set; }
        [Display(Name = "Flat Size")]
        public string flatsize { get; set; }
    }
}
