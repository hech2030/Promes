
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/06/2020 22:06:57
-- Generated from EDMX file: C:\Users\Ahmed\Desktop\Projects\Polar\Solar-Thermal.git\trunk\DataAcess\SolarModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SolarThermal];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
IF OBJECT_ID(N'[dbo].[ENTREE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ENTREE];
GO
IF OBJECT_ID(N'[dbo].[SORTIE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SORTIE];
GO
IF OBJECT_ID(N'[dbo].[LIGNE_COMMANDE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LIGNE_COMMANDE];
GO
IF OBJECT_ID(N'[dbo].[COMMANDE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[COMMANDE];
GO
IF OBJECT_ID(N'[dbo].[RECEPTION]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RECEPTION];
GO
IF OBJECT_ID(N'[dbo].[__EFMigrationsHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__EFMigrationsHistory];
GO
IF OBJECT_ID(N'[dbo].[ARTICLE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ARTICLE];
GO
IF OBJECT_ID(N'[dbo].[CATEGORIE_ART]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CATEGORIE_ART];
GO
IF OBJECT_ID(N'[dbo].[FOURNISSEUR]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FOURNISSEUR];
GO
IF OBJECT_ID(N'[dbo].[MAGASIN]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MAGASIN];
GO


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------






-- Creating table 'ARTICLE'
CREATE TABLE [dbo].[ARTICLE] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [designation] nvarchar(max)  NULL,
    [unit] nvarchar(max)  NULL,
    [quantite] bigint  NULL,
    [prix] bigint  NULL,
    [newAttr] bigint  NULL,
    [emplacement] nvarchar(max)  NULL,
    [CATEGORIE_ARTId] bigint  NOT NULL,
    [FOURNISSEURId] bigint  NOT NULL,
    [MAGASINId] bigint  NOT NULL
);
GO

-- Creating table 'CATEGORIE_ART'
CREATE TABLE [dbo].[CATEGORIE_ART] (
    [Id] bigint IDENTITY(1,1)  NOT NULL,
    [nomCate] nvarchar(max)  NULL,
    [description] nvarchar(max)  NULL
);
GO

-- Creating table 'COMMANDE'
CREATE TABLE [dbo].[COMMANDE] (
    [Id] bigint IDENTITY(1,1)  NOT NULL,
    [numCommande] bigint  NULL,
    [dateCOMMANDE] datetime  NULL,
    [etat] nvarchar(max)  NULL,
    [RECEPTIONId] bigint  NOT NULL,
    [FOURNISSEURId] bigint  NOT NULL
);
GO

-- Creating table 'ENTREE'
CREATE TABLE [dbo].[ENTREE] (
    [Id] bigint IDENTITY(1,1)  NOT NULL,
    [numEntree] bigint  NULL,
    [quantite] bigint  NULL,
    [dateEntree] datetime  NULL,
    [prixDentree] bigint  NULL,
    [ARTICLEId] bigint  NOT NULL
);
GO

-- Creating table 'FOURNISSEUR'
CREATE TABLE [dbo].[FOURNISSEUR] (
    [Id] bigint IDENTITY(1,1)  NOT NULL,
    [numF] bigint  NOT NULL,
    [NomF] nvarchar(max)  NULL,
    [adresse] nvarchar(max)  NULL,
    [codP] int  NULL,
    [ville] nvarchar(max)  NULL,
    [payes] nvarchar(max)  NULL,
    [tele] bigint  NULL,
    [fax] bigint  NULL,
    [email] nvarchar(max)  NULL
);
GO

-- Creating table 'LIGNE_COMMANDE'
CREATE TABLE [dbo].[LIGNE_COMMANDE] (
    [Id] bigint IDENTITY(1,1)  NOT NULL,
    [index] bigint  NULL,
    [quantite] bigint  NULL,
    [prix] bigint  NULL,
    [montant] float  NULL,
    [ARTICLEId] bigint  NOT NULL,
    [COMMANDEId] bigint  NOT NULL
);
GO

-- Creating table 'MAGASIN'
CREATE TABLE [dbo].[MAGASIN] (
    [Id] bigint IDENTITY(1,1)  NOT NULL,
    [nomMagasin] nvarchar(max)  NULL
);
GO

-- Creating table 'RECEPTION'
CREATE TABLE [dbo].[RECEPTION] (
    [Id] bigint IDENTITY(1,1)  NOT NULL,
    [numReception] bigint  NULL,
    [dateReception] datetime  NULL,
    [quantiteLivree] bigint  NULL
);
GO

-- Creating table 'SORTIE'
CREATE TABLE [dbo].[SORTIE] (
    [Id] bigint IDENTITY(1,1)  NOT NULL,
    [numSortie] bigint  NULL,
    [quantite] bigint  NULL,
    [dateSortie] datetime  NULL,
    [prixDSortie] bigint  NULL,
    [ARTICLEId] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------




-- Creating primary key on [Id] in table 'ARTICLE'
ALTER TABLE [dbo].[ARTICLE]
ADD CONSTRAINT [PK_ARTICLE]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CATEGORIE_ART'
ALTER TABLE [dbo].[CATEGORIE_ART]
ADD CONSTRAINT [PK_CATEGORIE_ART]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'COMMANDE'
ALTER TABLE [dbo].[COMMANDE]
ADD CONSTRAINT [PK_COMMANDE]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ENTREE'
ALTER TABLE [dbo].[ENTREE]
ADD CONSTRAINT [PK_ENTREE]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FOURNISSEUR'
ALTER TABLE [dbo].[FOURNISSEUR]
ADD CONSTRAINT [PK_FOURNISSEUR]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LIGNE_COMMANDE'
ALTER TABLE [dbo].[LIGNE_COMMANDE]
ADD CONSTRAINT [PK_LIGNE_COMMANDE]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MAGASIN'
ALTER TABLE [dbo].[MAGASIN]
ADD CONSTRAINT [PK_MAGASIN]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RECEPTION'
ALTER TABLE [dbo].[RECEPTION]
ADD CONSTRAINT [PK_RECEPTION]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SORTIE'
ALTER TABLE [dbo].[SORTIE]
ADD CONSTRAINT [PK_SORTIE]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CATEGORIE_ARTId] in table 'ARTICLE'
ALTER TABLE [dbo].[ARTICLE]
ADD CONSTRAINT [FK_ARTICLECATEGORIE_ART]
    FOREIGN KEY ([CATEGORIE_ARTId])
    REFERENCES [dbo].[CATEGORIE_ART]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ARTICLECATEGORIE_ART'
CREATE INDEX [IX_FK_ARTICLECATEGORIE_ART]
ON [dbo].[ARTICLE]
    ([CATEGORIE_ARTId]);
GO

-- Creating foreign key on [ARTICLEId] in table 'ENTREE'
ALTER TABLE [dbo].[ENTREE]
ADD CONSTRAINT [FK_ARTICLEENTREE]
    FOREIGN KEY ([ARTICLEId])
    REFERENCES [dbo].[ARTICLE]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ARTICLEENTREE'
CREATE INDEX [IX_FK_ARTICLEENTREE]
ON [dbo].[ENTREE]
    ([ARTICLEId]);
GO

-- Creating foreign key on [ARTICLEId] in table 'SORTIE'
ALTER TABLE [dbo].[SORTIE]
ADD CONSTRAINT [FK_ARTICLESORTIE]
    FOREIGN KEY ([ARTICLEId])
    REFERENCES [dbo].[ARTICLE]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ARTICLESORTIE'
CREATE INDEX [IX_FK_ARTICLESORTIE]
ON [dbo].[SORTIE]
    ([ARTICLEId]);
GO

-- Creating foreign key on [ARTICLEId] in table 'LIGNE_COMMANDE'
ALTER TABLE [dbo].[LIGNE_COMMANDE]
ADD CONSTRAINT [FK_ARTICLELIGNE_COMMANDE]
    FOREIGN KEY ([ARTICLEId])
    REFERENCES [dbo].[ARTICLE]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ARTICLELIGNE_COMMANDE'
CREATE INDEX [IX_FK_ARTICLELIGNE_COMMANDE]
ON [dbo].[LIGNE_COMMANDE]
    ([ARTICLEId]);
GO

-- Creating foreign key on [COMMANDEId] in table 'LIGNE_COMMANDE'
ALTER TABLE [dbo].[LIGNE_COMMANDE]
ADD CONSTRAINT [FK_COMMANDELIGNE_COMMANDE]
    FOREIGN KEY ([COMMANDEId])
    REFERENCES [dbo].[COMMANDE]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_COMMANDELIGNE_COMMANDE'
CREATE INDEX [IX_FK_COMMANDELIGNE_COMMANDE]
ON [dbo].[LIGNE_COMMANDE]
    ([COMMANDEId]);
GO

-- Creating foreign key on [RECEPTIONId] in table 'COMMANDE'
ALTER TABLE [dbo].[COMMANDE]
ADD CONSTRAINT [FK_RECEPTIONCOMMANDE]
    FOREIGN KEY ([RECEPTIONId])
    REFERENCES [dbo].[RECEPTION]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RECEPTIONCOMMANDE'
CREATE INDEX [IX_FK_RECEPTIONCOMMANDE]
ON [dbo].[COMMANDE]
    ([RECEPTIONId]);
GO

-- Creating foreign key on [FOURNISSEURId] in table 'COMMANDE'
ALTER TABLE [dbo].[COMMANDE]
ADD CONSTRAINT [FK_FOURNISSEURCOMMANDE]
    FOREIGN KEY ([FOURNISSEURId])
    REFERENCES [dbo].[FOURNISSEUR]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FOURNISSEURCOMMANDE'
CREATE INDEX [IX_FK_FOURNISSEURCOMMANDE]
ON [dbo].[COMMANDE]
    ([FOURNISSEURId]);
GO

-- Creating foreign key on [FOURNISSEURId] in table 'ARTICLE'
ALTER TABLE [dbo].[ARTICLE]
ADD CONSTRAINT [FK_FOURNISSEURARTICLE]
    FOREIGN KEY ([FOURNISSEURId])
    REFERENCES [dbo].[FOURNISSEUR]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FOURNISSEURARTICLE'
CREATE INDEX [IX_FK_FOURNISSEURARTICLE]
ON [dbo].[ARTICLE]
    ([FOURNISSEURId]);
GO

-- Creating foreign key on [MAGASINId] in table 'ARTICLE'
ALTER TABLE [dbo].[ARTICLE]
ADD CONSTRAINT [FK_MAGASINARTICLE]
    FOREIGN KEY ([MAGASINId])
    REFERENCES [dbo].[MAGASIN]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MAGASINARTICLE'
CREATE INDEX [IX_FK_MAGASINARTICLE]
ON [dbo].[ARTICLE]
    ([MAGASINId]);
GO
