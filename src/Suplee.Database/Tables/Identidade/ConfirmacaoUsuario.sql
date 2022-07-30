CREATE TABLE [dbo].[ConfirmacaoUsuario]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UsuarioId] UNIQUEIDENTIFIER NOT NULL, 
    [CodigoConfirmacao] NVARCHAR(10) NOT NULL, 
    [DataConfirmacao] DATETIME NULL
)
