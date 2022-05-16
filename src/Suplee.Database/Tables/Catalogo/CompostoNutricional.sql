CREATE TABLE [dbo].[CompostoNutricional]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [InformacaoNutricionalId] UNIQUEIDENTIFIER NOT NULL, 
    [Composto] NVARCHAR(250) NOT NULL, 
    [Porcao] NVARCHAR(250) NOT NULL, 
    [ValorDiario] NVARCHAR(250) NOT NULL,
    [Ordem] INT NOT NULL, 

    CONSTRAINT [FK_CompostoNutricional_InformacaoNutricional] FOREIGN KEY ([InformacaoNutricionalId]) REFERENCES [InformacaoNutricional]([Id])
)
