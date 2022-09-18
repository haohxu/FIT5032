CREATE TABLE [dbo].[Students] (
	[Id] INT IDENTITY (1, 1) NOT NULL, 
	[FirstName] NVARCHAR (max) NOT NULL, 
	[LastName] NVARCHAR (max) NOT NULL, 
	[UserId] NVARCHAR (max) NOT NULL
	PRIMARY KEY (Id)
);
GO

CREATE TABLE [dbo].[Units] (
	[Id] INT IDENTITY (1, 1) NOT NULL, 
	[Name] NVARCHAR (max) NOT NULL, 
	[Description] NVARCHAR (max) NOT NULL, 
	[StudentId] INT NOT NULL
	PRIMARY KEY (Id),
	FOREIGN KEY (StudentId) REFERENCES Students (Id)
);
GO