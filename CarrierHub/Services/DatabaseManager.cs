using CareerHub.Interfaces;
using CareerHub.Modals;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.util;

namespace CareerHub.Services
{
    public class DatabaseManager : IDatabaseManager
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        String filepath = "C:\\Users\\nairy\\source\\repos\\C#_coding_challenge\\CarrierHub\\util\\dbconfig.json";

        public DatabaseManager()
        {
            sqlConnection = DBConnection.GetConnection(filepath);
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }

        public List<Job> GetJobListings()
        {
            List<Job> jobListings = new List<Job>();

            try
            {
                string query = "SELECT JobID, CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate FROM Jobs";

                cmd.CommandText = query;

                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Job job = new Job
                        {
                            JobID = reader.GetInt32(0),
                            CompanyID = reader.GetInt32(1),
                            JobTitle = reader.GetString(2),
                            JobDescription = reader.IsDBNull(3) ? null : reader.GetString(3),
                            JobLocation = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Salary = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5),
                            JobType = reader.IsDBNull(6) ? null : reader.GetString(6),
                            PostedDate = reader.GetDateTime(7)
                        };

                        jobListings.Add(job);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving job listings: " + ex.Message);
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }

            return jobListings;
        }

        //public void InitializeDatabase()
        //{
        //    // Logic to initialize the database schema will go here
        //}

        // Method to insert a new job listing into the "Jobs" table
        public void InsertJobListing(Job job)
        {
            try
            {
                string query = "INSERT INTO Jobs (CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate) " +
                               "VALUES (@CompanyID, @JobTitle, @JobDescription, @JobLocation, @Salary, @JobType, @PostedDate)";

                cmd.CommandText = query;

                cmd.Parameters.Clear(); 
                cmd.Parameters.AddWithValue("@CompanyID", job.CompanyID);
                cmd.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                cmd.Parameters.AddWithValue("@JobDescription", job.JobDescription ?? (object)DBNull.Value); 
                cmd.Parameters.AddWithValue("@JobLocation", job.JobLocation ?? (object)DBNull.Value); 
                cmd.Parameters.AddWithValue("@Salary", job.Salary);
                cmd.Parameters.AddWithValue("@JobType", job.JobType ?? (object)DBNull.Value); 
                cmd.Parameters.AddWithValue("@PostedDate", job.PostedDate);

                
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Job listing inserted successfully.");
                    //Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to insert job listing.");
                    //Console.ResetColor();
                }
                Console.ResetColor();
                Console.WriteLine(new string('-', 50));

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while inserting the job listing: " + ex.Message);
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }


        public void InsertCompany(Company company)
        {
            try
            {
                string query = "INSERT INTO Companies (CompanyName, Location) VALUES (@CompanyName, @Location)";

                cmd.CommandText = query;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                cmd.Parameters.AddWithValue("@Location", company.Location ?? (object)DBNull.Value);

                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green; 
                    Console.WriteLine("Company inserted successfully.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to insert company.");
                }

                Console.ResetColor(); 
                Console.WriteLine(new string('-', 50)); 

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting company: " + ex.Message);
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }


        public void InsertApplicant(Applicant applicant)
        {
            try
            {
                string query = "INSERT INTO Applicants (FirstName, LastName, Email, Phone, Resume) " +
                               "VALUES (@FirstName, @LastName, @Email, @Phone, @Resume)";

                cmd.CommandText = query;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@FirstName", applicant.FirstName);
                cmd.Parameters.AddWithValue("@LastName", applicant.LastName);
                cmd.Parameters.AddWithValue("@Email", applicant.Email);
                cmd.Parameters.AddWithValue("@Phone", applicant.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Resume", applicant.Resume ?? (object)DBNull.Value);

                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Set color to green for success
                    Console.WriteLine("Applicant inserted successfully.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Set color to red for failure
                    Console.WriteLine("Failed to insert applicant.");
                }

                Console.ResetColor(); // Reset the console color to default
                Console.WriteLine(new string('-', 50)); // Print a line after execution

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting applicant: " + ex.Message);
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        public void InsertJobApplication(Application application)
        {
            try
            {
                string query = "INSERT INTO Applications (JobID, ApplicantID, ApplicationDate, CoverLetter) " +
                               "VALUES (@JobID, @ApplicantID, @ApplicationDate, @CoverLetter)";

                cmd.CommandText = query;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@JobID", application.JobID);
                cmd.Parameters.AddWithValue("@ApplicantID", application.ApplicantID);
                cmd.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
                cmd.Parameters.AddWithValue("@CoverLetter", application.CoverLetter ?? (object)DBNull.Value);

                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                

                

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Job application inserted successfully.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; 
                    Console.WriteLine("Failed to insert job application.");
                }

                Console.ResetColor(); 
                Console.WriteLine(new string('-', 50)); 


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting job application: " + ex.Message);
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }



        public List<Company> GetCompanies()
        {
            List<Company> companies = new List<Company>();

            try
            {
                string query = "SELECT * FROM Companies";
                cmd.CommandText = query;

                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Company company = new Company
                        {
                            CompanyID = reader.GetInt32(0),
                            CompanyName = reader.GetString(1),
                            Location = reader.IsDBNull(2) ? null : reader.GetString(2)
                        };
                        companies.Add(company);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error retrieving companies: " + ex.Message);
                Console.ResetColor();
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }

            return companies;
        }


        public List<Applicant> GetApplicants()
        {
            List<Applicant> applicants = new List<Applicant>();

            try
            {
                string query = "SELECT * FROM Applicants";
                cmd.CommandText = query;

                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Applicant applicant = new Applicant
                        {
                            ApplicantID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3),
                            Phone = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Resume = reader.IsDBNull(5) ? null : reader.GetString(5)
                        };
                        applicants.Add(applicant);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error retrieving applicants: " + ex.Message);
                Console.ResetColor();
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }

            return applicants;
        }


        public List<Application> GetApplicationsForJob(int jobID)
        {
            List<Application> applications = new List<Application>();

            try
            {
                string query = @"SELECT a.ApplicationID, a.JobID, a.ApplicantID, a.ApplicationDate, a.CoverLetter, 
                                app.FirstName, app.LastName, j.JobTitle 
                         FROM Applications a
                         JOIN Applicants app ON a.ApplicantID = app.ApplicantID
                         JOIN Jobs j ON a.JobID = j.JobID
                         WHERE a.JobID = @JobID";

                cmd.CommandText = query;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@JobID", jobID);

                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Application application = new Application
                        {
                            ApplicationID = reader.GetInt32(0),
                            JobID = reader.GetInt32(1),
                            ApplicantID = reader.GetInt32(2),
                            ApplicationDate = reader.GetDateTime(3),
                            CoverLetter = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Applicant = $"{reader.GetString(5)} {reader.GetString(6)}", 
                            Job = reader.GetString(7)
                        };

                        applications.Add(application);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error retrieving applications for job: " + ex.Message);
                Console.ResetColor();
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }

            return applications;
        }

        public List<Job> GetJobListingsBySalaryRange(decimal minSalary, decimal maxSalary)
        {
            List<Job> jobListings = new List<Job>();

            try
            {
                string query = "SELECT j.JobID, j.JobTitle, j.Salary, c.CompanyName " +
                               "FROM Jobs j " +
                               "JOIN Companies c ON j.CompanyID = c.CompanyID " +
                               "WHERE j.Salary BETWEEN @MinSalary AND @MaxSalary";

                cmd.CommandText = query;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@MinSalary", minSalary);
                cmd.Parameters.AddWithValue("@MaxSalary", maxSalary);

                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Job job = new Job
                        {
                            JobID = reader.GetInt32(0),
                            JobTitle = reader.GetString(1),
                            Salary = reader.GetDecimal(2),
                            CompanyName = reader.GetString(3)
                        };

                        jobListings.Add(job);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occurred while retrieving job listings: " + ex.Message);
                Console.ResetColor();
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }

            return jobListings;
        }



    }
}
