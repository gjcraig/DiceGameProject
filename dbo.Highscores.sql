USE [Dice]
GO

/****** Object: Table [dbo].[Highscores] Script Date: 30/05/2019 14:29:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Highscores] (
    [Name]  VARCHAR (50) NOT NULL,
    [Score] INT          NOT NULL
);


