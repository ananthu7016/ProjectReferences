---This is an updated version of database for the LoanManagementSystemV3

--First we need to create a database 
CREATE DATABASE LMS_V4_db;

USE LMS_V4_db;



CREATE TABLE Roles
(
    RoleId INT IDENTITY PRIMARY KEY,
    RoleName VARCHAR(25),
    IsActive BIT
);


CREATE TABLE Users
(
    UserId INT IDENTITY PRIMARY KEY,
    FullName VARCHAR(35),
    UserName VARCHAR(30),
    Password VARCHAR(30) UNIQUE,
    RoleId INT REFERENCES Roles(RoleId),
    CreatedDateTime DATETIME,
    IsActive BIT 
);

CREATE TABLE Customers 
(
    CustomerId INT IDENTITY PRIMARY KEY,
    FullName VARCHAR(25),
    Occupation VARCHAR(35),
    Address VARCHAR(70),
    Phone VARCHAR(10),
    Aadhar VARCHAR(12),
    Dob DATE,
    UserName VARCHAR(30) NULL,
    Password VARCHAR(30) NULL,
    CustEmploymentStatus BIT,
    RegisteredDateTime DATETIME,
    IsActive BIT
);

CREATE TABLE LoanTypes
(
    LoanTypeId INT IDENTITY PRIMARY KEY,
    LoanTypeName VARCHAR(40),
    LoanDescription VARCHAR(300),
    LoanMinimumAmount DECIMAL(18,2),
    LoanMaximumAmount DECIMAL(18,2),
    LoanInterestRate DECIMAL(10,2),
    ProcessingFee DECIMAL(10,2),
    TaxPercentage DECIMAL(10,2),
    IsEmploymentStatusRequired BIT,
    LoanTerm INT,
    CreatedDateTime DATETIME,
    IsActive BIT
);

CREATE TABLE LoanDetails
(
    DetailId INT IDENTITY PRIMARY KEY,
    LoanTypeId INT REFERENCES LoanTypes(LoanTypeId),
    CustId INT REFERENCES Customers(CustomerId),
    LoanAmount DECIMAL(18,2),
    LoanRequestDateTime DATETIME,
    LoanSanctionDateTime DATETIME,
    LoanPurpose VARCHAR(400),
    UserId INT REFERENCES Users(UserId),
    IsActive BIT
);

CREATE TABLE LoanRequests
(
    RequestId INT IDENTITY PRIMARY KEY,
    LoanTypeId INT REFERENCES LoanTypes(LoanTypeId),
    CustId INT REFERENCES Customers(CustomerId),
    LoanRequestDateTime DATETIME,
    LoanPurpose VARCHAR(400),
    RequestedAmount DECIMAL(18,2),
    RequestDetails VARCHAR(50),
    IsDocumentUploaded BIT,
    IsActive BIT
);

CREATE TABLE LoanVerificationSummary
(
SummaryId INT IDENTITY PRIMARY KEY,
RequestId INT REFERENCES LoanRequests(RequestId),
Summary VARCHAR(200),
StatusChangedDateTime DATETIME
);


CREATE TABLE LoanVerifications 
(
    VerificationId INT IDENTITY PRIMARY KEY,
    RequestId INT REFERENCES LoanRequests(RequestId),
    UserId INT REFERENCES Users(UserId),
    VerificationReview VARCHAR(500),
    VerificationStatus BIT,
 );

CREATE TABLE Logs
(
    LogId INT IDENTITY PRIMARY KEY,
    EventType VARCHAR(30),
    TimeStamp DATETIME,
    LogDescription VARCHAR(200),
    LogStatus BIT
);

SELECT * FROM LoanVerificationSummary
SELECT * FROM LoanVerifications


//----------------------------------------------------
INSERT INTO Roles
VALUES('Admin',1),
('Manager',1),
('Officer',1)


SELECT * FROM Users
SELECT * FROM Customers

SELECT * FROM LoanTypes

SELECT * FROM LoanRequests

SELECT * FROM LoanVerificationSummary

SELECT * FROM LoanVerifications
