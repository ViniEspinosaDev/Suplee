CREATE TABLE [dbo].[ProdutoImagem]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ProdutoId] UNIQUEIDENTIFIER NOT NULL, 
    [NomeImagem] NVARCHAR(250) NOT NULL,
    [UrlImagemOriginal] NVARCHAR(250) NOT NULL, 
    [UrlImagemReduzida] NVARCHAR(250) NOT NULL, 
    [UrlImagemMaior] NVARCHAR(250) NOT NULL, 

    CONSTRAINT [FK_ProdutoImagem_Produto] FOREIGN KEY ([ProdutoId]) REFERENCES [Produto]([Id])
)
