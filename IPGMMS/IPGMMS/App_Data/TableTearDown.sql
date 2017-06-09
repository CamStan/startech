IF OBJECT_ID('AspNetUserRoles','U') IS NOT NULL
	DROP TABLE [dbo].[AspNetUserRoles];
GO

IF OBJECT_ID('dbo.AspNetRoles','U') IS NOT NULL
	DROP TABLE [dbo].[AspNetRoles];
GO

IF OBJECT_ID('dbo.AspNetUserClaims','U') IS NOT NULL
	DROP TABLE [dbo].[AspNetUserClaims];
GO

IF OBJECT_ID('dbo.AspNetUserLogins','U') IS NOT NULL
	DROP TABLE [dbo].[AspNetUserLogins];
GO

IF OBJECT_ID('dbo.AspNetUsers','U') IS NOT NULL
	DROP TABLE [dbo].[AspNetUsers];
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