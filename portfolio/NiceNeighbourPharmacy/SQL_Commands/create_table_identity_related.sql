-- Drop table "Customers"
Drop Table If Exists [dbo].[Customers];

-- Create table "Customers"
Create Table [dbo].[Customers] (
	[Id] Int Identity (1,1) Not Null,
	[FirstName] Nvarchar (Max) Not Null,
	[LastName] Nvarchar (Max) Not Null,
	[UserId] Nvarchar (Max) Not Null
	Primary Key (Id)
);

-- Drop table "Pharmacists"
Drop Table If Exists [dbo].[Pharmacists];

-- Create table "Pharmacists"
Create Table [dbo].[Pharmacists] (
	[Id] Int Identity (1,1) Not Null,
	[FirstName] Nvarchar (Max) Not Null,
	[LastName] Nvarchar (Max) Not Null,
	[UserId] Nvarchar (Max) Not Null
	Primary Key (Id)
);

-- Drop table "Admins"
Drop Table If Exists [dbo].[Admins];

-- Create table "Admins"
Create Table [dbo].[Admins] (
	[Id] Int Identity (1,1) Not Null,
	[FirstName] Nvarchar (Max) Not Null,
	[LastName] Nvarchar (Max) Not Null,
	[UserId] Nvarchar (Max) Not Null
	Primary Key (Id)
);