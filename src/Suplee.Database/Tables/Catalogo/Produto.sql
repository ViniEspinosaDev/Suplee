CREATE TABLE [dbo].[Produto]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Nome] NVARCHAR(MAX) NOT NULL, 
    [Descricao] NVARCHAR(MAX) NOT NULL, 
    [Composicao] NVARCHAR(MAX) NOT NULL, 
    [QuantidadeDisponivel] INT NOT NULL, 
    [Preco] DECIMAL NOT NULL, 
    [Altura] DECIMAL NOT NULL, 
    [Largura] DECIMAL NOT NULL, 
    [Profundidade] DECIMAL NOT NULL, 
    [InformacaoNutricionalId] UNIQUEIDENTIFIER NOT NULL, 
    [CategoriaId] UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT [FK_Produto_InformacaoNutricionalId] FOREIGN KEY ([InformacaoNutricionalId]) REFERENCES [InformacaoNutricional]([Id]),
    CONSTRAINT [FK_Produto_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categoria]([Id]),
)
