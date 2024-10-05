using CareerHub.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.Interfaces
{
    public interface IDatabaseManager
    {

        void InsertJobListing(Job job);

        void InsertCompany(Company company);

        void InsertApplicant(Applicant applicant);

        void InsertJobApplication(Application application);

        List<Job> GetJobListings();

        List<Company> GetCompanies();

        List<Applicant> GetApplicants();

        List<Application> GetApplicationsForJob(int jobID);
    }
}
