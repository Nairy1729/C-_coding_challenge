-- 1. Provide a SQL script that initializes the database for the Job Board scenario “CareerHub”.  
CREATE DATABASE CareerHub

USE CareerHub

--2. Create tables for Companies, Jobs, Applicants and Applications.  
--3. Define appropriate primary keys, foreign keys, and constraints.  
--4. Ensure the script handles potential errors, such as if the database or tables already exist. 

-- Create the Companies table
CREATE TABLE Companies (
    CompanyID INT PRIMARY KEY IDENTITY(1,1),
    CompanyName VARCHAR(255) NOT NULL,
    Location VARCHAR(255)
)

-- Create the Jobs table
CREATE TABLE Jobs (
    JobID INT PRIMARY KEY IDENTITY(1,1),
    CompanyID INT FOREIGN KEY REFERENCES Companies(CompanyID),
    JobTitle VARCHAR(255) NOT NULL,
    JobDescription TEXT,
    JobLocation VARCHAR(255),
    Salary DECIMAL(18,2),
    JobType VARCHAR(50),
    PostedDate DATETIME DEFAULT GETDATE()
)

-- Create the Applicants table
CREATE TABLE Applicants (
    ApplicantID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Phone VARCHAR(20),
    Resume TEXT
)

-- Create the Applications table
CREATE TABLE Applications (
    ApplicationID INT PRIMARY KEY IDENTITY(1,1),
    JobID INT FOREIGN KEY REFERENCES Jobs(JobID),
    ApplicantID INT FOREIGN KEY REFERENCES Applicants(ApplicantID),
    ApplicationDate DATETIME DEFAULT GETDATE(),
    CoverLetter TEXT
)

-- Inserting data into the table
-- Insert sample data into Companies table
INSERT INTO Companies (CompanyName, Location)
VALUES
('Tata Consultancy Services', 'Mumbai'),
('Infosys', 'Bengaluru'),
('Wipro', 'Bengaluru'),
('HCL Technologies', 'Noida'),
('Tech Mahindra', 'Pune'),
('Reliance Industries', 'Mumbai'),
('Larsen & Toubro', 'Mumbai'),
('Bharti Airtel', 'New Delhi'),
('HDFC Bank', 'Mumbai'),
('ICICI Bank', 'Mumbai')

select * from Companies

-- Insert sample data into Jobs table
INSERT INTO Jobs (CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate)
VALUES
(1, 'Software Developer', 'Develop and maintain software applications.', 'Mumbai', 800000, 'Full-time', '2024-09-01'),
(1, 'Data Analyst', 'Analyze large datasets to extract actionable insights.', 'Mumbai', 700000, 'Full-time', '2024-09-10'),
(2, 'System Engineer', 'Work on the development and support of IT systems.', 'Bengaluru', 600000, 'Full-time', '2024-09-12'),
(3, 'Project Manager', 'Oversee IT projects from start to finish.', 'Bengaluru', 1200000, 'Full-time', '2024-09-15'),
(4, 'Network Engineer', 'Maintain and optimize network infrastructure.', 'Noida', 900000, 'Full-time', '2024-09-20'),
(5, 'Cloud Architect', 'Design and manage cloud infrastructure.', 'Pune', 1500000, 'Full-time', '2024-09-22'),
(6, 'Marketing Manager', 'Lead the marketing team and strategies.', 'Mumbai', 1100000, 'Full-time', '2024-09-25'),
(7, 'Civil Engineer', 'Plan and oversee construction projects.', 'Mumbai', 850000, 'Full-time', '2024-09-28'),
(8, 'Sales Executive', 'Handle sales and client relationships.', 'New Delhi', 500000, 'Full-time', '2024-09-30'),
(9, 'Banking Analyst', 'Analyze financial transactions and trends.', 'Mumbai', 950000, 'Full-time', '2024-09-05')

-- Insert sample data into Applicants table
INSERT INTO Applicants (FirstName, LastName, Email, Phone, Resume)
VALUES
('Amit', 'Sharma', 'amit.sharma@gmail.com', '9876543210', 'Experienced software developer.'),
('Priya', 'Verma', 'priya.verma@gmail.com', '9823456710', 'Expert in data analysis and visualization.'),
('Rohit', 'Singh', 'rohit.singh@gmail.com', '9912345678', 'Certified network engineer with 5 years of experience.'),
('Nisha', 'Patel', 'nisha.patel@gmail.com', '9123456789', 'Civil engineer with expertise in infrastructure development.'),
('Vikas', 'Kumar', 'vikas.kumar@gmail.com', '9874561230', 'Marketing manager with a track record of successful campaigns.'),
('Anjali', 'Mehta', 'anjali.mehta@gmail.com', '9987456123', 'Cloud architect with 7 years of experience in AWS.'),
('Rahul', 'Joshi', 'rahul.joshi@gmail.com', '9876523410', 'Banking analyst with expertise in risk management.'),
('Sonia', 'Kapoor', 'sonia.kapoor@gmail.com', '9987563412', 'Experienced project manager in IT services.'),
('Manoj', 'Bajpai', 'manoj.bajpai@gmail.com', '9872341234', 'Sales executive with strong communication skills.'),
('Meera', 'Gupta', 'meera.gupta@gmail.com', '9812341234', 'System engineer with expertise in IT systems.')

-- Insert sample data into Applications table
INSERT INTO Applications (JobID, ApplicantID, ApplicationDate, CoverLetter)
VALUES
(1, 1, '2024-09-02', 'I am an experienced software developer looking to join your team.'),
(2, 2, '2024-09-12', 'I have expertise in data analysis and am excited about this role.'),
(4, 3, '2024-09-15', 'I have 5 years of experience in network management.'),
(8, 9, '2024-09-30', 'I have strong sales experience and am eager to contribute.'),
(5, 6, '2024-09-25', 'As a cloud architect, I have managed large-scale cloud projects.'),
(9, 7, '2024-09-05', 'I have deep experience in financial analysis and banking trends.'),
(3, 8, '2024-09-16', 'With 10 years of experience in IT project management, I am well-suited for this role.'),
(6, 5, '2024-09-26', 'I have a proven record of leading successful marketing strategies.'),
(7, 4, '2024-09-28', 'My civil engineering skills will be beneficial to your projects.'),
(2, 10, '2024-09-13', 'I have extensive experience in systems engineering and support.')