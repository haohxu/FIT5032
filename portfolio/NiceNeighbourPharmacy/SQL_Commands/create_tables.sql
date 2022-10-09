-- Drop tables, in order
Drop Table If Exists [dbo].[Locations];

Drop Table If Exists [dbo].[Files];

Drop Table If Exists [dbo].[Events];

Drop Table If Exists [dbo].[Ratings];

Drop Table If Exists [dbo].[TrolleyItems];

Drop Table If Exists [dbo].[OrderDetails];

Drop Table If Exists [dbo].[Medicines];

Drop Table If Exists [dbo].[Orders];


-- Creating tables MUST be executed after User tables created

-- Create table "Orders"
Create Table [dbo].[Orders] (
	[Id] Int Identity (1, 1) Not Null, 
	[TotalPrice] Numeric (7, 2) Not Null, -- or use "MONEY" datatype
	[Status] Nvarchar (Max), 
	[CollectDateTime] Date, 
		[CustomerId] Nvarchar (128) Not Null,
		[PharmacistId] Nvarchar (128)

	Primary Key (Id),
	Foreign Key (CustomerId) References AspNetUsers (Id),
	Foreign Key (PharmacistId) References AspNetUsers (Id)
);

-- Create table "Medicines"
Create Table [dbo].[Medicines] (
	[Id] Int Identity (1, 1) Not Null, 
	[Name] Nvarchar (Max) Not Null, 
	[Description] Nvarchar (Max), 
	[Category] Nvarchar (Max), 
	[Price] Decimal (7, 2), 
	[NumberOfStock] Int,
	[AvgRatings] Decimal (7, 2),
	[Notes] Nvarchar (Max)

	Primary Key (Id)
);

-- Create table "OrderDetails"
Create Table [dbo].[OrderDetails] (
	[Id] Int Identity (1, 1) Not Null, 
	[Quantity] Int, 
	[Price] Int, 
		[OrderId] Int Not Null, 
		[MedicineId] Int Not Null

	Primary Key (Id), 
	Foreign Key (OrderId) References Orders (Id),
	Foreign Key (MedicineId) References Medicines (Id)
);

-- Create table "Ratings"
Create Table [dbo].[Ratings] (
	[Id] Int Identity (1, 1) Not Null, 
	[RatingScore] Decimal (7, 2), 
	[RatingComment] Nvarchar (Max),
	[RatingStatus] Nvarchar (Max), 
		[OrderDetailId] Int Not Null

	Primary Key (Id), 
	Foreign Key (OrderDetailId) References OrderDetails (Id)
);

-- Create table "TrolleyItems"
Create Table [dbo].[TrolleyItems] (
	[Id] Int Identity (1, 1) Not Null, 
	[Quantity] Int,
	[Price] Decimal (7, 2),
		[MedicineId] Int Not Null,
		[CustomerId] Nvarchar (128) Not Null

	Primary Key (Id), 
	Foreign Key (MedicineId) References Medicines (Id),
	Foreign Key (CustomerId) References AspNetUsers (Id)
);

-- Create table "Events"
Create Table [dbo].[Events] (
	[Id] Int Identity (1, 1) Not Null,
	[Title] Nvarchar (Max),
	[Description] Nvarchar (Max),
	[TimeStart] DateTime,
	[TimeEnd] DateTime,
		[OrderId] Int Not Null

	Primary Key (Id),
	Foreign Key (OrderId) References Orders (Id)
);

-- Create table "Files"
Create Table [dbo].[Files] (
	[Id] Int Identity (1, 1) Not Null, 
	[Name] Nvarchar (Max),
	[Path] Nvarchar (Max),
	[Category] Nvarchar (Max),
		[UserId] Nvarchar (128) Not Null

	Primary Key (Id),
	Foreign Key (UserId) References AspNetUsers (Id)
);

-- Create table "Locations"
Create Table [dbo].[Locations] (
	[Id] Int Identity (1, 1) Not Null, 
	[Name] Nvarchar (Max),
	[Description] Nvarchar (Max),
	[Address] Nvarchar (Max),
	[Latitude] Decimal (11, 8) Not Null, 
	[Longitude] Decimal (11, 8) Not Null,
		[UserId] Nvarchar (128) Not Null

	Primary Key (Id),
	Foreign Key (UserId) References AspNetUsers (Id)
);