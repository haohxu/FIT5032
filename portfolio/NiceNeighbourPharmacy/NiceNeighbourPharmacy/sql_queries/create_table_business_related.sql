-- Drop tables, one by one
Drop Table If Exists [dbo].[ShoppingCartRecords];

Drop Table If Exists [dbo].[Ratings];

Drop Table If Exists [dbo].[OrdersMedicines];

Drop Table If Exists [dbo].[Medicines];

Drop Table If Exists [dbo].[Orders];

-- Creating tables MUST be executed 
-- after "create_table_identity_related.sql" creating tables executed

-- Create table "Orders"
Create Table [dbo].[Orders] (
	[Id] Int Identity (1, 1) Not Null, 
	[TotalPrice] Numeric (7, 2) Not Null, -- or use "MONEY" datatype
	[Status] Nvarchar (Max) Not Null, 
	[CollectDateTime] Date Not Null, 
		[CustomerId] Int Not Null,
		[PharmacistId] Int

	Primary Key (Id),
	Foreign Key (CustomerId) References Customers (Id),
	Foreign Key (PharmacistId) References Pharmacists (Id)
);

-- Create table "Medicines"
Create Table [dbo].[Medicines] (
	[Id] Int Identity (1, 1) Not Null, 
	[Name] Nvarchar (Max) Not Null, 
	[Description] Nvarchar (Max) Not Null, 
	[Category] Nvarchar (Max) Not Null, 
	[Price] Decimal (7, 2) Not Null, 
	[NumberOfStock] Int Not Null

	Primary Key (Id)
);

-- Create table "OrdersMedicines"
Create Table [dbo].[OrdersMedicines] (
	[Id] Int Identity (1, 1) Not Null, 
	[Quantity] Int Not Null, 
	[TotalPrice] Int Not Null, 
		[OrderId] Int Not Null, 
		[MedicineId] Int Not Null

	Primary Key (Id), 
	Foreign Key (OrderId) References Orders (Id),
	Foreign Key (MedicineId) References Medicines (Id)
);

-- Create table "Ratings"
Create Table [dbo].[Ratings] (
	[Id] Int Identity (1, 1) Not Null, 
	[RatingScore] Decimal (3, 2), 
	[RatingComment] Nvarchar (Max),
	[RatingStatus] Int Not Null, 
		[OrderMedicineId] Int Not Null

	Primary Key (Id), 
	Foreign Key (OrderMedicineId) References OrdersMedicines (Id)
);

-- Create table "ShoppingCartRecords"
Create Table [dbo].[ShoppingCartRecords] (
	[Id] Int Identity (1, 1) Not Null, 
	[Quantity] Int Not Null, 
		[MedicineId] Int Not Null,
		[CustomerId] Int Not Null

	Primary Key (Id), 
	Foreign Key (MedicineId) References Medicines (Id),
	Foreign Key (CustomerId) References Customers (Id)
);