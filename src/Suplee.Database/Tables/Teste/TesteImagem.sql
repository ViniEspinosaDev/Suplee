CREATE TABLE [dbo].[TesteImagem]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [NomeMaximizada] NVARCHAR(50) NOT NULL,
    [NomeReduzida] NVARCHAR(50) NOT NULL,
    [UrlMaximizada] NVARCHAR(100) NOT NULL, 
    [UrlReduzida] NVARCHAR(100) NOT NULL
)
