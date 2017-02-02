IF OBJECT_ID('dbo.Story','U') IS NOT NULL
	DROP TABLE [dbo].[Story];
GO

IF OBJECT_ID('dbo.UserKey','U') IS NOT NULL
	DROP TABLE [dbo].[UserKey];
GO

IF OBJECT_ID('dbo.User','U') IS NOT NULL
	DROP TABLE [dbo].[User];
GO

-- ########### User ###########
CREATE TABLE [dbo].[User]
(
	[ID] INT IDENTITY (1,1) NOT NULL,
	[FName] NVARCHAR (50) NOT NULL,
	[LName] NVARCHAR (50) NOT NULL,
	[UserName] NVARCHAR (50) NOT NULL,
	CONSTRAINT [PK_dbo.Pirates] PRIMARY KEY CLUSTERED ([ID] ASC)
);

-- ########### Story ###########
CREATE TABLE [dbo].[Story]
(
	[ID] INT IDENTITY (1,1) NOT NULL,
	[UserID] INT NOT NULL,
	[Title] NVARCHAR (200) NOT NULL,
	[Body] TEXT NOT NULL,
	[Summary] TEXT,
	[PostDate] DATE NOT NULL,
	CONSTRAINT [PK_dbo.Story] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.Story_dbo.User_ID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


-- ########### UserKey ###########
CREATE TABLE [dbo].[UserKey]
(
	[ID] INT IDENTITY (1,1) NOT NULL,
	[UserID] INT NOT NULL,
	[UKey] INT NOT NULL,
	CONSTRAINT [PK_dbo.UserKey] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.UserKey_dbo.User_ID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);