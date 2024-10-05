# Job Board Application

## Overview

The Job Board Application is a console-based system designed to facilitate the process of job searching and recruitment. This application allows job seekers to create profiles, apply for job listings, and view their applications. Companies can post job listings, while the system also maintains a database of applicants, companies, and job listings. 

The application follows object-oriented programming principles and incorporates robust exception handling to ensure a smooth user experience.

## Features

- **Job Listings:** Companies can post job listings with details like job title, description, location, salary, and type.
- **Company Profiles:** Companies can create and manage their profiles.
- **Applicant Profiles:** Applicants can create their profiles with personal details and resumes.
- **Job Applications:** Applicants can apply for specific job listings and submit cover letters.
- **Exception Handling:** The application implements custom exceptions to handle:
  - Invalid email formats
  - Negative salary calculations
  - File upload errors
  - Application deadline checks
  - Database connection issues

## Directory Structure

## Classes

### JobListing

- **Attributes:**
  - `JobID` (int): Unique identifier for the job.
  - `CompanyID` (int): Reference to the company offering the job.
  - `JobTitle` (string): Title of the job.
  - `JobDescription` (string): Description of the job.
  - `JobLocation` (string): Location of the job.
  - `Salary` (decimal): Salary offered for the job.
  - `JobType` (string): Type of job (e.g., Full-time, Part-time).
  - `PostedDate` (DateTime): Date when the job was posted.

- **Methods:**
  - `Apply(int applicantID, string coverLetter)`: Allows applicants to apply for the job.
  - `GetApplicants()`: Retrieves a list of applicants for the job.

### Company

- **Attributes:**
  - `CompanyID` (int): Unique identifier for the company.
  - `CompanyName` (string): Name of the company.
  - `Location` (string): Location of the company.

- **Methods:**
  - `PostJob(string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType)`: Posts a new job listing.
  - `GetJobs()`: Retrieves a list of job listings posted by the company.

### Applicant

- **Attributes:**
  - `ApplicantID` (int): Unique identifier for the applicant.
  - `FirstName` (string): First name of the applicant.
  - `LastName` (string): Last name of the applicant.
  - `Email` (string): Email address of the applicant.
  - `Phone` (string): Phone number of the applicant.
  - `Resume` (string): Reference to the applicant's resume.

- **Methods:**
  - `CreateProfile(string email, string firstName, string lastName, string phone)`: Creates a profile for the applicant.
  - `ApplyForJob(int jobID, string coverLetter)`: Allows the applicant to apply for a specific job.

### JobApplication

- **Attributes:**
  - `ApplicationID` (int): Unique identifier for the job application.
  - `JobID` (int): Reference to the job listing.
  - `ApplicantID` (int): Reference to the applicant.
  - `ApplicationDate` (DateTime): Date when the application was submitted.
  - `CoverLetter` (string): Cover letter submitted with the application.

### DatabaseManager

- **Methods:**
  - `InitializeDatabase()`: Initializes the database schema and tables.
  - `InsertJobListing(JobListing job)`: Inserts a new job listing into the database.
  - `InsertCompany(Company company)`: Inserts a new company into the database.
  - `InsertApplicant(Applicant applicant)`: Inserts a new applicant into the database.
  - `InsertJobApplication(JobApplication application)`: Inserts a new job application into the database.
  - `GetJobListings()`: Retrieves a list of all job listings.
  - `GetCompanies()`: Retrieves a list of all companies.
  - `GetApplicants()`: Retrieves a list of all applicants.
  - `GetApplicationsForJob(int jobID)`: Retrieves applications for a specific job listing.

## Exception Handling

The application includes various user-defined exceptions to handle:

- Invalid email format during registration.
- Negative salary values when calculating averages.
- File upload issues (size, format, not found).
- Application submissions after deadlines.
- Database connectivity errors.

## Database Connectivity

The application utilizes a connection to a database to store and retrieve information. It features:

- **Job Listing Retrieval:** Connects to the database to fetch job listings.
- **Applicant Profile Creation:** Allows applicants to insert their information into the database.
- **Job Application Submission:** Facilitates job applications by inserting details into the database.
- **Company Job Posting:** Enables companies to add job listings to the database.
- **Salary Range Query:** Allows searching for job listings within a specific salary range.



