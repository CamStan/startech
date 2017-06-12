-- ############# AspNetRoles #############
CREATE TABLE [dbo].[AspNetRoles]
(
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);

-- ############# AspNetUsers #############
CREATE TABLE [dbo].[AspNetUsers]
(
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]([UserName] ASC);

-- ############# AspNetUserClaims #############
CREATE TABLE [dbo].[AspNetUserClaims]
(
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (128) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]([UserId] ASC);

-- ############# AspNetUserLogins #############
CREATE TABLE [dbo].[AspNetUserLogins]
(
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]([UserId] ASC);

-- ############# AspNetUserRoles #############
CREATE TABLE [dbo].[AspNetUserRoles]
(
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]([UserId] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]([RoleId] ASC);

-- ############# MemberLevels #############
CREATE TABLE [dbo].[MemberLevels]
(
	[ID] INT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [MLevel] NVARCHAR(255) NOT NULL
);


-- ############# ContactTypes #############
CREATE TABLE [dbo].[ContactTypes]
(
	[ID] INT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [ContactType] NVARCHAR(50) NOT NULL 
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
    [Membership_Number] NVARCHAR(50) NULL, 
    [Membership_SignupDate] DATE NULL, 
    [Membership_ExpirationDate] DATE NULL, 
    [MemberLevel] INT NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [MiddleName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [BusinessName] NVARCHAR(256) NULL, 
    [Website] NVARCHAR(256) NULL,
	[Identity_ID] NVARCHAR(128) NULL,
	CONSTRAINT [FK_dbo.Members_dbo.MemberLevels] FOREIGN KEY ([MemberLevel]) REFERENCES [dbo].[MemberLevels]([ID]),
);

-- ############# ContactInfo #############
CREATE TABLE [dbo].[ContactInfo]
(
	[ID] INT IDENTITY (1,1) NOT NULL UNIQUE,
    [StreetAddress] NVARCHAR(255) NULL, 
    [City] NVARCHAR(255) NULL,
	[StateName] NVARCHAR(255) NULL,
    [Country] NVARCHAR(50) NULL, 
    [PostalCode] NVARCHAR(50) NULL,
	[PhoneNumber] NVARCHAR(22) NULL,
	[Email] NVARCHAR(50) NULL,
    CONSTRAINT [PK_dbo.ContactInfo] PRIMARY KEY ([ID]),
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
	CONSTRAINT [FK_dbo.Contact_dbo.ContactInfo] FOREIGN KEY ([ContactInfo_ID]) REFERENCES [dbo].[ContactInfo]([ID])
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