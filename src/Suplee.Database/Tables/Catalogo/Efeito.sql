﻿CREATE TABLE [dbo].[Efeito]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Nome] NVARCHAR(250) NOT NULL, 
    [Descricao] NVARCHAR(500) NOT NULL, 
    [Icone] NVARCHAR(250) NOT NULL
)
