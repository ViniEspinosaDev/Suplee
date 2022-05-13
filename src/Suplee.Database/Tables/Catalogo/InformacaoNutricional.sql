CREATE TABLE [dbo].[InformacaoNutricional]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Cabecalho] NVARCHAR(250) NOT NULL, 
    [Legenda] NVARCHAR(500) NOT NULL
)
