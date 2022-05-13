CREATE TABLE [dbo].[ProdutoEfeito]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ProdutoId] UNIQUEIDENTIFIER NOT NULL, 
    [EfeitoId] UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT [FK_ProdutoEfeito_Produto] FOREIGN KEY ([ProdutoId]) REFERENCES [Produto]([Id]),
    CONSTRAINT [FK_ProdutoEfeito_Efeito] FOREIGN KEY ([EfeitoId]) REFERENCES [Efeito]([Id])
)
