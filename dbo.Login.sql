USE [Login]
GO

/****** Object: Table [dbo].[Login] Script Date: 30/05/2019 14:31:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Login] (
    [User]     VARCHAR (50) NOT NULL,
    [Password] VARCHAR (50) NOT NULL
);


