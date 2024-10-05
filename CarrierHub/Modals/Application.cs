using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.Modals
{
    public class Application
    {
        public int ApplicationID { get; set; }
        public int JobID { get; set; }
        public int ApplicantID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string CoverLetter { get; set; }

        public String Job { get; set; }
        public String Applicant { get; set; }
    }

}
