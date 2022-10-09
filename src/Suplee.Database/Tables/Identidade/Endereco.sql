CREATE TABLE [dbo].[Endereco]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UsuarioId] UNIQUEIDENTIFIER NOT NULL,
    [NomeDestinatario] NVARCHAR(150) NOT NULL, 
    [CEP] NVARCHAR(15) NOT NULL, 
    [Estado] NVARCHAR(2) NOT NULL, 
    [Cidade] NVARCHAR(50) NOT NULL, 
    [Bairro] NVARCHAR(50) NOT NULL, 
    [Rua] NVARCHAR(120) NOT NULL, 
    [Numero] NVARCHAR(10) NOT NULL, 
    [Complemento] NVARCHAR(100) NULL, 
    [TipoLocal] INT NOT NULL, 
    [Telefone] NVARCHAR(15) NULL, 
    [InformacaoAdicional] NVARCHAR(255) NULL, 
    [EnderecoPadrao] BIT NOT NULL DEFAULT 0, 

    CONSTRAINT [FK_Endereco_Usuario] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario]([Id]),
)
