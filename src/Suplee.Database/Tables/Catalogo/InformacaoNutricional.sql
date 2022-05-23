CREATE TABLE [dbo].[InformacaoNutricional]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Cabecalho] NVARCHAR(250) NOT NULL, 
    [Legenda] NVARCHAR(MAX) NOT NULL
)
