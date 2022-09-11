CREATE TABLE [dbo].[PedidoProduto]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PedidoId] UNIQUEIDENTIFIER NOT NULL, 
    [ProdutoId] UNIQUEIDENTIFIER NOT NULL, 
    [NomeProduto] NVARCHAR(MAX) NOT NULL, 
    [Quantidade] INT NOT NULL, 
    [ValorUnitario] DECIMAL(18, 2) NOT NULL,

    CONSTRAINT [FK_PedidoProduto_Produto] FOREIGN KEY ([ProdutoId]) REFERENCES [Produto]([Id]),
    CONSTRAINT [FK_PedidoProduto_Pedido] FOREIGN KEY ([PedidoId]) REFERENCES [Pedido]([Id]),
)
