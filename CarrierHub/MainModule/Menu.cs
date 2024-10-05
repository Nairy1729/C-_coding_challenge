using CareerHub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Modals;
using CareerHub.Exceptions;

namespace CareerHub.MainModule
{

    public class Menu
    {
        private readonly DatabaseManager databaseManager = new DatabaseManager();

        public void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine();
            Console.WriteLine("\n*** Your Gateway to Career Success!  ***");
            Console.WriteLine();
            Console.ResetColor();
            while (true)
            {
                
                Console.WriteLine("1. Insert a new company");
                Console.WriteLine("2. Insert a new job listing");
                Console.WriteLine("3. Insert a new applicant");
                Console.WriteLine("4. Apply for a job");
                Console.WriteLine("5. View all companies");
                Console.WriteLine("6. View all job listings");
                Console.WriteLine("7. View all applicants");
                Console.WriteLine("8. View applications for a specific job");
                Console.WriteLine("9. Get Job in salary range");
                Console.WriteLine("10. Exit");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Enter your choice (1-10): ");
                Console.ResetColor();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        InsertCompany();
                        break;
                    case "2":
                        InsertJobListing();
                        break;
                    case "3":
                        InsertApplicant();
                        break;
                    case "4":
                        ApplyForJob();
                        break;
                    case "5":
                        ViewAllCompanies();
                        break;
                    case "6":
                        ViewAllJobListings();
                        break;
                    case "7":
                        ViewAllApplicants();
                        break;
                    case "8":
                        ViewApplicationsForJob();
                        break;
                    case "9":
                        SearchJobsBySalaryRange();
                        break;
                    case "10":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice! Please select a valid option.");
                        break;
                }
                Console.WriteLine("\nPress Enter to return to the menu...");
                Console.ReadLine();
            }
        }

         private void SearchJobsBySalaryRange()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Please enter the minimum salary(INR): ");
            decimal minSalary = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Please enter the maximum salary(INR): ");
            decimal maxSalary = Convert.ToDecimal(Console.ReadLine());
            Console.ResetColor();

            List<Job> jobs = databaseManager.GetJobListingsBySalaryRange(minSalary, maxSalary);

            

            if (jobs.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Jobs available in the salary range INR {minSalary} - INR {maxSalary}:");
                foreach (var job in jobs)
                {
                    Console.WriteLine($"Job Title: {job.JobTitle}, Company: {job.CompanyName}, Salary: INR {job.Salary}");
                }
                Console.ResetColor ();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No jobs found in the specified salary range.");
                Console.ResetColor();
            }
        }


        private void InsertCompany()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Enter Company Name: ");
            string companyName = Console.ReadLine();

            Console.Write("Enter Company Location (optional): ");
            string location = Console.ReadLine();
            Console.ResetColor();

            Company company = new Company
            {
                CompanyName = companyName,
                Location = string.IsNullOrEmpty(location) ? null : location
            };
            

            databaseManager.InsertCompany(company);
        }

        private void InsertJobListing()
        {
            try
            {
                ViewAllCompanies();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Enter Company ID: ");
                int companyId = int.Parse(Console.ReadLine());

                Console.Write("Enter Job Title: ");
                string jobTitle = Console.ReadLine();

                Console.Write("Enter Job Description (optional): ");
                string jobDescription = Console.ReadLine();

                Console.Write("Enter Job Location (optional): ");
                string jobLocation = Console.ReadLine();

                Console.Write("Enter Salary (INR): ");
                decimal salary;
                if (!decimal.TryParse(Console.ReadLine(), out salary) || salary < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    throw new InvalidSalaryException("Salary cannot be a negative value.");
                    Console.ResetColor();

                }

                Console.Write("Enter Job Type (optional): ");
                string jobType = Console.ReadLine();

                Job job = new Job
                {
                    CompanyID = companyId,
                    JobTitle = jobTitle,
                    JobDescription = string.IsNullOrEmpty(jobDescription) ? null : jobDescription,
                    JobLocation = string.IsNullOrEmpty(jobLocation) ? null : jobLocation,
                    Salary = salary,
                    JobType = string.IsNullOrEmpty(jobType) ? null : jobType,
                    PostedDate = DateTime.Now
                };

                databaseManager.InsertJobListing(job);

                // Console.WriteLine("Job listing inserted successfully!");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: Invalid input format. Please enter a valid number for Company ID or Salary. Details: {ex.Message}");
            }
            catch (InvalidSalaryException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }




        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }


        private void InsertApplicant()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Enter First Name: ");
                string firstName = Console.ReadLine();

                Console.Write("Enter Last Name: ");
                string lastName = Console.ReadLine();

                Console.Write("Enter Email: ");
                string email = Console.ReadLine();

                // Validate the email format
                if (!IsValidEmail(email))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    throw new InvalidEmailFormatException("The provided email format is invalid.");
                    Console.ResetColor();
                }

                Console.Write("Enter Phone (optional): ");
                string phone = Console.ReadLine();

                Console.Write("Enter Resume (optional): ");
                string resume = Console.ReadLine();

                Applicant applicant = new Applicant
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = string.IsNullOrEmpty(phone) ? null : phone,
                    Resume = string.IsNullOrEmpty(resume) ? null : resume
                };

                
                databaseManager.InsertApplicant(applicant);

                //Console.WriteLine("Applicant registered successfully!");
            }
            catch (InvalidEmailFormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(); 
            }
        }


        private void ApplyForJob()
        {
            ViewAllJobListings();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Enter Job ID: ");
            int jobId = int.Parse(Console.ReadLine());

            Console.Write("Enter Applicant ID: ");
            int applicantId = int.Parse(Console.ReadLine());

            Console.Write("Enter Cover Letter (optional): ");
            string coverLetter = Console.ReadLine();

            Application application = new Application
            {
                JobID = jobId,
                ApplicantID = applicantId,
                ApplicationDate = DateTime.Now,
                CoverLetter = string.IsNullOrEmpty(coverLetter) ? null : coverLetter
            };

            databaseManager.InsertJobApplication(application);
        }

        private void ViewAllCompanies()
        {
            List<Company> companies = databaseManager.GetCompanies();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n*** List of Companies ***");
            Console.ResetColor();
            foreach (var company in companies)
            {
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine($"Company ID: {company.CompanyID}, Name: {company.CompanyName}, Location: {company.Location}");
            }
        }

        private void ViewAllJobListings()
        {
            List<Job> jobListings = databaseManager.GetJobListings();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n*** List of Job Listings ***");
            foreach (var job in jobListings)
            {
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine($"Job ID: {job.JobID}, Title: {job.JobTitle}, Salary: {job.Salary}, Location: {job.JobLocation}");
            }
        }

        private void ViewAllApplicants()
        {
            List<Applicant> applicants = databaseManager.GetApplicants();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n*** List of Applicants ***");
            foreach (var applicant in applicants)
            {
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine($"Applicant ID: {applicant.ApplicantID}, Name: {applicant.FirstName} {applicant.LastName}, Email: {applicant.Email}");
            }
        }

        private void ViewApplicationsForJob()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Enter Job ID: ");
            int jobId = int.Parse(Console.ReadLine());

            List<Application> applications = databaseManager.GetApplicationsForJob(jobId);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n*** List of Applications for Job ID: {jobId} ***");
            foreach (var application in applications)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Application ID: {application.ApplicationID}, Name: {application.Applicant}, Job: {application.Job} ,Date: {application.ApplicationDate}");
            }
        }

        //private void InitializeDatabase()
        //{
        //    jobService.InitializeDatabase();
        //}
    }

}
