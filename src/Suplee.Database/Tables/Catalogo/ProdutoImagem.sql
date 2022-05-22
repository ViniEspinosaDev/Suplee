CREATE TABLE [dbo].[ProdutoImagem]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ProdutoId] UNIQUEIDENTIFIER NOT NULL, 
    [NomeImagem] NVARCHAR(250) NOT NULL,
    [Url] NVARCHAR(250) NOT NULL, 

    CONSTRAINT [FK_ProdutoImagem_Produto] FOREIGN KEY ([ProdutoId]) REFERENCES [Produto]([Id])
)
