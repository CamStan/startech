IF OBJECT_ID('dbo.UserNameBridges','U') IS NOT NULL
	DROP TABLE [dbo].[UserNameBridges];
GO

IF OBJECT_ID('dbo.MemberCertifications','U') IS NOT NULL
	DROP TABLE [dbo].[MemberCertifications];
GO

IF OBJECT_ID('dbo.Contacts','U') IS NOT NULL
	DROP TABLE [dbo].[Contacts];
GO

IF OBJECT_ID('dbo.ContactInfo','U') IS NOT NULL
	DROP TABLE [dbo].[ContactInfo];
GO

IF OBJECT_ID('dbo.ContactTypes','U') IS NOT NULL
	DROP TABLE [dbo].[ContactTypes];
GO

IF OBJECT_ID('dbo.Certificates','U') IS NOT NULL
	DROP TABLE [dbo].[Certificates];
GO

IF OBJECT_ID('dbo.Members','U') IS NOT NULL
	DROP TABLE [dbo].[Members];
GO

IF OBJECT_ID('dbo.MemberLevels','U') IS NOT NULL
	DROP TABLE [dbo].[MemberLevels];
GO


-- ############# MemberLevels #############
CREATE TABLE [dbo].[MemberLevels]
(
	[ID] INT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [MLevel] NCHAR(255) NOT NULL
);


-- ############# ContactTypes #############
CREATE TABLE [dbo].[ContactTypes]
(
	[ID] INT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [ContactType] NCHAR(50) NOT NULL 
);

-- ############# Certificates #############
CREATE TABLE [dbo].[Certificates]
(
	[ID] INT IDENTITY (1,1) NOT NULL, 
    [Certification] NVARCHAR(255) UNIQUE NOT NULL, 
    CONSTRAINT [PK_Certificates] PRIMARY KEY ([ID])
);

-- ############# Members #############
CREATE TABLE [dbo].[Members]
(
    [ID] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[UserName] NVARCHAR(256) UNIQUE NOT NULL, 
    [Membership_Number] NVARCHAR(50) NULL, 
    [Membership_SignupDate] DATE NULL, 
    [Membership_ExpirationDate] DATE NULL, 
    [MemberLevel] INT NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [MiddleName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [BusinessName] NVARCHAR(256) NULL, 
    [Website] NVARCHAR(256) NULL,
--	CONSTRAINT [FK_dbo.Members_dbo.AspNetUsers] FOREIGN KEY ([UserName]) REFERENCES [dbo].[AspNetUsers]([UserName]),
	CONSTRAINT [FK_dbo.Members_dbo.MemberLevels] FOREIGN KEY ([MemberLevel]) REFERENCES [dbo].[MemberLevels]([ID]),
);

-- ############# ContactInfo #############
CREATE TABLE [dbo].[ContactInfo]
(
	[ID] INT NOT NULL  IDENTITY UNIQUE, 
    [Member_ID] INT NOT NULL, 
    [StreetAddress] NVARCHAR(255) NULL, 
    [City] NVARCHAR(255) NULL,
	[StateName] NVARCHAR(255) NULL,
    [Country] NVARCHAR(50) NULL, 
    [PostalCode] NVARCHAR(50) NULL,
	[PhoneNumber] NVARCHAR(22) NULL,
	[Email] NVARCHAR(50) NULL,
    CONSTRAINT [PK_dbo.ContactInfo] PRIMARY KEY ([Member_ID], [ID]),
);

-- ############# Contacts #############
CREATE TABLE [dbo].[Contacts]
(
	[ID] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[Member_ID] INT NOT NULL,
    [ContactInfo_ID] INT NOT NULL, 
    [ContactType_ID] INT NOT NULL 
	CONSTRAINT [PK_dbo.Contacts] UNIQUE CLUSTERED ([Member_ID], [ContactType_ID]),
	CONSTRAINT [FK_dbo.ContactInfo_dbo.ContactTypes] FOREIGN KEY ([ContactType_ID]) REFERENCES [dbo].[ContactTypes]([ID]),
	CONSTRAINT [FK_dbo.ContactInfo_dbo.Members] FOREIGN KEY ([Member_ID]) REFERENCES [dbo].[Members]([ID]),
	CONSTRAINT [FK_dbo.Contact_dbo.ContactInfo] FOREIGN KEY ([Member_ID],[ContactInfo_ID]) REFERENCES [dbo].[ContactInfo]([Member_ID],[ID])
);

-- ############# MemberCertifications #############
CREATE TABLE [dbo].[MemberCertifications]
(
	[ID] INT IDENTITY (1,1) NOT NULL,
	[Member_ID] INT NOT NULL,
	[Certificate_ID] INT NOT NULL,
	CONSTRAINT [PK_dbo.MemberCertifications] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.MemberCertifications_dbo.Members)] FOREIGN KEY ([Member_ID]) REFERENCES [dbo].[Members]([ID]),
	CONSTRAINT [FK_dbo.MemberCertifications_dbo.Certifcates] FOREIGN KEY ([Certificate_ID]) REFERENCES [dbo].[Certificates] ([ID])
);

-- #############  Username Bridge #################
CREATE TABLE [dbo].[UserNameBridges]
(
	[ID] INT IDENTITY (1,1) NOT NULL,
	[Member_UserName] NVARCHAR(256) UNIQUE NOT NULL,
	[AspNet_UserName] NVARCHAR (256) UNIQUE NOT NULL,
	CONSTRAINT [PK_UserNameBridge] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_dbo.UserNameBridge_dbo.AspNetUsers] FOREIGN KEY ([AspNet_UserName]) REFERENCES [dbo].[AspNetUsers]([UserName]),
	CONSTRAINT [FK_dbo.UserNameBridge_dbo.Members] FOREIGN KEY ([Member_UserName]) REFERENCES [dbo].[Members]([UserName])
);