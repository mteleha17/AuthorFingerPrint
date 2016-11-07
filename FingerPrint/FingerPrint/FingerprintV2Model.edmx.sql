
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/07/2016 16:12:51
-- Generated from EDMX file: C:\Users\Michael\Source\Repos\AuthorFingerPrint\FingerPrint\FingerPrint\FingerprintV2Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FingerprintV2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


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
    [CountsID] int  NOT NULL,
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
    [Name] nchar(100)  NOT NULL
);
GO

-- Creating table 'Group_Group'
CREATE TABLE [dbo].[Group_Group] (
    [GG_ID] int  NOT NULL,
    [ParentID] int  NOT NULL,
    [ChildID] int  NOT NULL,
    [GroupGroupID] int  NOT NULL,
    [GroupGroupID1] int  NOT NULL
);
GO

-- Creating table 'Texts'
CREATE TABLE [dbo].[Texts] (
    [TextID] int IDENTITY(1,1) NOT NULL,
    [Name] nchar(100)  NOT NULL,
    [Author] nchar(100)  NULL,
    [QuoteInd] bit  NOT NULL,
    [CountsWithQuotesID] int  NOT NULL,
    [CountsWithoutQuotesID] int  NOT NULL,
    [Count_CountsID] int  NOT NULL,
    [Count1_CountsID] int  NOT NULL
);
GO

-- Creating table 'Text_Group'
CREATE TABLE [dbo].[Text_Group] (
    [TextID] int  NOT NULL,
    [GroupID] int  NOT NULL,
    [TextTextID] int  NOT NULL,
    [Group_GroupID] int  NOT NULL
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

-- Creating primary key on [TextID] in table 'Text_Group'
ALTER TABLE [dbo].[Text_Group]
ADD CONSTRAINT [PK_Text_Group]
    PRIMARY KEY CLUSTERED ([TextID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Count_CountsID] in table 'Texts'
ALTER TABLE [dbo].[Texts]
ADD CONSTRAINT [FK_TextCount]
    FOREIGN KEY ([Count_CountsID])
    REFERENCES [dbo].[Counts]
        ([CountsID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TextCount'
CREATE INDEX [IX_FK_TextCount]
ON [dbo].[Texts]
    ([Count_CountsID]);
GO

-- Creating foreign key on [TextTextID] in table 'Text_Group'
ALTER TABLE [dbo].[Text_Group]
ADD CONSTRAINT [FK_TextText_Group]
    FOREIGN KEY ([TextTextID])
    REFERENCES [dbo].[Texts]
        ([TextID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TextText_Group'
CREATE INDEX [IX_FK_TextText_Group]
ON [dbo].[Text_Group]
    ([TextTextID]);
GO

-- Creating foreign key on [Group_GroupID] in table 'Text_Group'
ALTER TABLE [dbo].[Text_Group]
ADD CONSTRAINT [FK_Text_GroupGroup]
    FOREIGN KEY ([Group_GroupID])
    REFERENCES [dbo].[Groups]
        ([GroupID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Text_GroupGroup'
CREATE INDEX [IX_FK_Text_GroupGroup]
ON [dbo].[Text_Group]
    ([Group_GroupID]);
GO

-- Creating foreign key on [GroupGroupID] in table 'Group_Group'
ALTER TABLE [dbo].[Group_Group]
ADD CONSTRAINT [FK_GroupGroup_Group]
    FOREIGN KEY ([GroupGroupID])
    REFERENCES [dbo].[Groups]
        ([GroupID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupGroup_Group'
CREATE INDEX [IX_FK_GroupGroup_Group]
ON [dbo].[Group_Group]
    ([GroupGroupID]);
GO

-- Creating foreign key on [GroupGroupID1] in table 'Group_Group'
ALTER TABLE [dbo].[Group_Group]
ADD CONSTRAINT [FK_GroupGroup_Group1]
    FOREIGN KEY ([GroupGroupID1])
    REFERENCES [dbo].[Groups]
        ([GroupID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupGroup_Group1'
CREATE INDEX [IX_FK_GroupGroup_Group1]
ON [dbo].[Group_Group]
    ([GroupGroupID1]);
GO

-- Creating foreign key on [Count1_CountsID] in table 'Texts'
ALTER TABLE [dbo].[Texts]
ADD CONSTRAINT [FK_TextCount1]
    FOREIGN KEY ([Count1_CountsID])
    REFERENCES [dbo].[Counts]
        ([CountsID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TextCount1'
CREATE INDEX [IX_FK_TextCount1]
ON [dbo].[Texts]
    ([Count1_CountsID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------