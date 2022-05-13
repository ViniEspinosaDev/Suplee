CREATE TABLE [dbo].[ProdutoImagem]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ProdutoId] UNIQUEIDENTIFIER NOT NULL, 
    [Imagem] NVARCHAR(250) NOT NULL,

    CONSTRAINT [FK_ProdutoImagem_Produto] FOREIGN KEY ([ProdutoId]) REFERENCES [Produto]([Id])
)
