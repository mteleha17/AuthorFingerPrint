
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/21/2016 15:52:04
-- Generated from EDMX file: C:\Users\cfriess17\Source\Repos\AuthorFingerPrint\FingerPrint\FingerPrint\FingerprintV3Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FingerprintV3];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Counts__CountsID__32E0915F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Counts] DROP CONSTRAINT [FK__Counts__CountsID__32E0915F];
GO
IF OBJECT_ID(N'[dbo].[FK__Group_Gro__Child__403A8C7D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Group_Group] DROP CONSTRAINT [FK__Group_Gro__Child__403A8C7D];
GO
IF OBJECT_ID(N'[dbo].[FK__Group_Gro__Paren__412EB0B6]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Group_Group] DROP CONSTRAINT [FK__Group_Gro__Paren__412EB0B6];
GO
IF OBJECT_ID(N'[dbo].[FK__Text_Grou__Group__3A4CA8FD]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Text_Group] DROP CONSTRAINT [FK__Text_Grou__Group__3A4CA8FD];
GO
IF OBJECT_ID(N'[dbo].[FK__Text_Grou__TextI__2739D489]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Text_Group] DROP CONSTRAINT [FK__Text_Grou__TextI__2739D489];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Counts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Counts];
GO
IF OBJECT_ID(N'[dbo].[Group]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Group];
GO
IF OBJECT_ID(N'[dbo].[Group_Group]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Group_Group];
GO
IF OBJECT_ID(N'[dbo].[Text]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Text];
GO
IF OBJECT_ID(N'[dbo].[Text_Group]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Text_Group];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Counts'
CREATE TABLE [dbo].[Counts] (
    [CountsID] int IDENTITY(1,1) NOT NULL,
    [one] int  NOT NULL,
    [two] int  NOT NULL,
    [three] int  NOT NULL,
    [four] int  NOT NULL,
    [five] int  NOT NULL,
    [six] int  NOT NULL,
    [seven] int  NOT NULL,
    [eight] int  NOT NULL,
    [nine] int  NOT NULL,
    [ten] int  NOT NULL,
    [eleven] int  NOT NULL,
    [twelve] int  NOT NULL,
    [thirteen] int  NOT NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [GroupID] int IDENTITY(1,1) NOT NULL,
    [Name] nchar(10)  NULL
);
GO

-- Creating table 'Group_Group'
CREATE TABLE [dbo].[Group_Group] (
    [GG_ID] int  NOT NULL,
    [ParentID] int  NOT NULL,
    [ChildID] int  NOT NULL
);
GO

-- Creating table 'Texts'
CREATE TABLE [dbo].[Texts] (
    [TextID] int IDENTITY(1,1) NOT NULL,
    [Name] nchar(100)  NULL,
    [Author] nchar(100)  NULL,
    [QuoteInd] bit  NULL,
    [CountsWithQuotesID] int  NULL,
    [CountsWithoutQuotesID] int  NULL
);
GO

-- Creating table 'Text_Group1'
CREATE TABLE [dbo].[Text_Group1] (
    [Groups_GroupID] int  NOT NULL,
    [Texts_TextID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CountsID] in table 'Counts'
ALTER TABLE [dbo].[Counts]
ADD CONSTRAINT [PK_Counts]
    PRIMARY KEY CLUSTERED ([CountsID] ASC);
GO

-- Creating primary key on [GroupID] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([GroupID] ASC);
GO

-- Creating primary key on [GG_ID] in table 'Group_Group'
ALTER TABLE [dbo].[Group_Group]
ADD CONSTRAINT [PK_Group_Group]
    PRIMARY KEY CLUSTERED ([GG_ID] ASC);
GO

-- Creating primary key on [TextID] in table 'Texts'
ALTER TABLE [dbo].[Texts]
ADD CONSTRAINT [PK_Texts]
    PRIMARY KEY CLUSTERED ([TextID] ASC);
GO

-- Creating primary key on [Groups_GroupID], [Texts_TextID] in table 'Text_Group1'
ALTER TABLE [dbo].[Text_Group1]
ADD CONSTRAINT [PK_Text_Group1]
    PRIMARY KEY CLUSTERED ([Groups_GroupID], [Texts_TextID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CountsID] in table 'Counts'
ALTER TABLE [dbo].[Counts]
ADD CONSTRAINT [FK__Counts__CountsID__32E0915F]
    FOREIGN KEY ([CountsID])
    REFERENCES [dbo].[Texts]
        ([TextID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ChildID] in table 'Group_Group'
ALTER TABLE [dbo].[Group_Group]
ADD CONSTRAINT [FK__Group_Gro__Child__403A8C7D]
    FOREIGN KEY ([ChildID])
    REFERENCES [dbo].[Groups]
        ([GroupID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Group_Gro__Child__403A8C7D'
CREATE INDEX [IX_FK__Group_Gro__Child__403A8C7D]
ON [dbo].[Group_Group]
    ([ChildID]);
GO

-- Creating foreign key on [ParentID] in table 'Group_Group'
ALTER TABLE [dbo].[Group_Group]
ADD CONSTRAINT [FK__Group_Gro__Paren__412EB0B6]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[Groups]
        ([GroupID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Group_Gro__Paren__412EB0B6'
CREATE INDEX [IX_FK__Group_Gro__Paren__412EB0B6]
ON [dbo].[Group_Group]
    ([ParentID]);
GO

-- Creating foreign key on [Groups_GroupID] in table 'Text_Group1'
ALTER TABLE [dbo].[Text_Group1]
ADD CONSTRAINT [FK_Text_Group1_Group]
    FOREIGN KEY ([Groups_GroupID])
    REFERENCES [dbo].[Groups]
        ([GroupID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Texts_TextID] in table 'Text_Group1'
ALTER TABLE [dbo].[Text_Group1]
ADD CONSTRAINT [FK_Text_Group1_Text]
    FOREIGN KEY ([Texts_TextID])
    REFERENCES [dbo].[Texts]
        ([TextID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Text_Group1_Text'
CREATE INDEX [IX_FK_Text_Group1_Text]
ON [dbo].[Text_Group1]
    ([Texts_TextID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------