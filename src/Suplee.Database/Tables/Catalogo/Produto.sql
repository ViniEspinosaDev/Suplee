CREATE TABLE [dbo].[Produto]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Nome] NVARCHAR(MAX) NOT NULL, 
    [Descricao] NVARCHAR(MAX) NOT NULL, 
    [Composicao] NVARCHAR(MAX) NOT NULL, 
    [QuantidadeDisponivel] INT NOT NULL, 
    [Preco] DECIMAL(18, 2) NOT NULL, 
    [Altura] DECIMAL(18, 2) NOT NULL, 
    [Largura] DECIMAL(18, 2) NOT NULL, 
    [Profundidade] DECIMAL(18, 2) NOT NULL, 
    [InformacaoNutricionalId] UNIQUEIDENTIFIER NOT NULL, 
    [CategoriaId] UNIQUEIDENTIFIER NOT NULL,
    [DataCadastro] DATETIME NOT NULL DEFAULT GETDATE(), 

    CONSTRAINT [FK_Produto_InformacaoNutricionalId] FOREIGN KEY ([InformacaoNutricionalId]) REFERENCES [InformacaoNutricional]([Id]),
    CONSTRAINT [FK_Produto_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categoria]([Id]),
)
