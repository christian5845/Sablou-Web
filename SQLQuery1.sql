CREATE TABLE [dbo].[Bruger] (
    [Id]            INT            NOT NULL,
    [Navn]          NVARCHAR (50)  NOT NULL,
    [Email]         NVARCHAR (50)  NOT NULL,
    [Adresse]       NVARCHAR (50)  NOT NULL,
    [Telefonnummer] INT            NULL,
    [Password]      NVARCHAR (200) NOT NULL,
    [Rolle]         NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);