
SET QUOTED_IDENTIFIER OFF;
GO
USE [FIT5032_MyCodeFirst];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_Enrolment_dbo_Course_CourseID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Enrolment] DROP CONSTRAINT [FK_dbo_Enrolment_dbo_Course_CourseID];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Enrolment_dbo_Student_StudentID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Enrolment] DROP CONSTRAINT [FK_dbo_Enrolment_dbo_Student_StudentID];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Course]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Course];
GO
IF OBJECT_ID(N'[dbo].[Enrolment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Enrolment];
GO
IF OBJECT_ID(N'[dbo].[Student]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Student];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Courses'
CREATE TABLE [dbo].[Courses] (
    [CourseID] int  NOT NULL,
    [Title] nvarchar(max)  NULL,
    [Credits] int  NOT NULL
);
GO

-- Creating table 'Enrolments'
CREATE TABLE [dbo].[Enrolments] (
    [EnrolmentID] int IDENTITY(1,1) NOT NULL,
    [CourseID] int  NOT NULL,
    [StudentID] int  NOT NULL
);
GO

-- Creating table 'Students'
CREATE TABLE [dbo].[Students] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [Address] nvarchar(max)  NULL,
    [DOB] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CourseID] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [PK_Courses]
    PRIMARY KEY CLUSTERED ([CourseID] ASC);
GO

-- Creating primary key on [EnrolmentID] in table 'Enrolments'
ALTER TABLE [dbo].[Enrolments]
ADD CONSTRAINT [PK_Enrolments]
    PRIMARY KEY CLUSTERED ([EnrolmentID] ASC);
GO

-- Creating primary key on [ID] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [PK_Students]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CourseID] in table 'Enrolments'
ALTER TABLE [dbo].[Enrolments]
ADD CONSTRAINT [FK_dbo_Enrolment_dbo_Course_CourseID]
    FOREIGN KEY ([CourseID])
    REFERENCES [dbo].[Courses]
        ([CourseID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Enrolment_dbo_Course_CourseID'
CREATE INDEX [IX_FK_dbo_Enrolment_dbo_Course_CourseID]
ON [dbo].[Enrolments]
    ([CourseID]);
GO

-- Creating foreign key on [StudentID] in table 'Enrolments'
ALTER TABLE [dbo].[Enrolments]
ADD CONSTRAINT [FK_dbo_Enrolment_dbo_Student_StudentID]
    FOREIGN KEY ([StudentID])
    REFERENCES [dbo].[Students]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Enrolment_dbo_Student_StudentID'
CREATE INDEX [IX_FK_dbo_Enrolment_dbo_Student_StudentID]
ON [dbo].[Enrolments]
    ([StudentID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------