
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/16/2022 13:30:18
-- Generated from EDMX file: C:\Files\Monash\2022S2\FIT5032\source\FIT5032\portfolio\NiceNeighbourPharmacy\NiceNeighbourPharmacy\Models\NNP_Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Database1];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Medicines'
CREATE TABLE [dbo].[Medicines] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [price] nvarchar(max)  NOT NULL,
    [num_of_stock] nvarchar(max)  NOT NULL,
    [catagory] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [collect_time] nvarchar(max)  NOT NULL,
    [total_price] nvarchar(max)  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [CustomerId] int  NOT NULL
);
GO

-- Creating table 'Order_Medicine'
CREATE TABLE [dbo].[Order_Medicine] (
    [Id] nvarchar(max)  NOT NULL,
    [price] nvarchar(max)  NOT NULL,
    [quantity] nvarchar(max)  NOT NULL,
    [Order_Id] int  NOT NULL,
    [Medicine_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Medicines'
ALTER TABLE [dbo].[Medicines]
ADD CONSTRAINT [PK_Medicines]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Order_Medicine'
ALTER TABLE [dbo].[Order_Medicine]
ADD CONSTRAINT [PK_Order_Medicine]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CustomerId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_CustomerOrder]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerOrder'
CREATE INDEX [IX_FK_CustomerOrder]
ON [dbo].[Orders]
    ([CustomerId]);
GO

-- Creating foreign key on [Order_Id] in table 'Order_Medicine'
ALTER TABLE [dbo].[Order_Medicine]
ADD CONSTRAINT [FK_Order_MedicineOrder]
    FOREIGN KEY ([Order_Id])
    REFERENCES [dbo].[Orders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_MedicineOrder'
CREATE INDEX [IX_FK_Order_MedicineOrder]
ON [dbo].[Order_Medicine]
    ([Order_Id]);
GO

-- Creating foreign key on [Medicine_Id] in table 'Order_Medicine'
ALTER TABLE [dbo].[Order_Medicine]
ADD CONSTRAINT [FK_Order_MedicineMedicine]
    FOREIGN KEY ([Medicine_Id])
    REFERENCES [dbo].[Medicines]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_MedicineMedicine'
CREATE INDEX [IX_FK_Order_MedicineMedicine]
ON [dbo].[Order_Medicine]
    ([Medicine_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------