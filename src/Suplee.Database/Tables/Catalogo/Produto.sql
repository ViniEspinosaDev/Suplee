CREATE TABLE [dbo].[Produto]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Nome] NVARCHAR(250) NOT NULL, 
    [Descricao] NVARCHAR(500) NOT NULL, 
    [Composicao] NVARCHAR(250) NOT NULL, 
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
