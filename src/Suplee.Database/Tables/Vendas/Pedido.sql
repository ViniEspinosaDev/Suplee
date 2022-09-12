CREATE TABLE [dbo].[Pedido]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UsuarioId] UNIQUEIDENTIFIER NOT NULL,
    [Codigo] NVARCHAR(7) NOT NULL, 
    [Status] SMALLINT NOT NULL, 
    [ValorTotal] DECIMAL(18, 2) NOT NULL, 
    [DataCadastro] DATETIME NOT NULL,

    CONSTRAINT [FK_Pedido_Usuario] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario]([Id]),
)
